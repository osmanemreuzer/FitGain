using System.Security.Claims;
using System.Threading.Tasks;
using FitGain.Data.Abstract;
using FitGain.Data.Concrete.EfCore;
using FitGain.Entity;
using FitGain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitGain.Controllers;

public class PostsController : Controller
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ICommentRepository _commentRepository;
    public PostsController(IPostRepository postRepository,ITagRepository tagRepository,ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
        _commentRepository = commentRepository;
    }
    public  async Task<IActionResult> Index(string tag)
    {
        var claims = User.Claims;
        var posts = _postRepository.Posts.Where(i => i.IsActive);

        if(!string.IsNullOrEmpty(tag))
        {
            posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
        }
         var postList = await posts.ToListAsync();
         return View(postList);
    }

    public async Task<IActionResult> Details(string url)
    {
        return View(await _postRepository
                    .Posts
                    .Include(x => x.User)
                    .Include(x => x.Tags)
                    .Include(x => x.Comments)
                    .ThenInclude(x => x.User)
                    .FirstOrDefaultAsync(p => p.Url == url));
    }

    public IActionResult AddComment(int PostId, string Text, string Url)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var entity = new Comment
        {
            Text = Text,
            PublishedOn = DateTime.Now,
            PostId = PostId,
            UserId = int.Parse(userId ?? "")
        };
        _commentRepository.CreateComment(entity);

        return Redirect("/posts/details/" + Url);
    }
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [Authorize]
    public IActionResult Create(PostCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
            {
                ModelState.AddModelError("", "Please log in to continue.");
                return View(model);
            }

            _postRepository.CreatePost(
                new Post
                {
                    Title = model.Title ?? "",
                    Content = model.Content ?? "",
                    Description = model.Description ?? "",
                    Url = model.Url ?? "",
                    UserId = parsedUserId,
                    PublishedOn = DateTime.Now,
                    Image = "bench.jpg",
                    IsActive = false
                }
            );

            return RedirectToAction("Index");
        }
        return View(model);
    }
    [Authorize]
    public async Task<IActionResult> List()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
        var role = User.FindFirstValue(ClaimTypes.Role);

        var posts = _postRepository.Posts;

        if (string.IsNullOrEmpty(role))
        {
            posts = posts.Where(i => i.UserId == userId);
        }
        return View(await posts.ToListAsync());
    }
    [Authorize]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var post = _postRepository.Posts.Include(i => i.Tags).FirstOrDefault(i => i.PostId == id);
        if (post == null)
        {
            return NotFound();
        }

        ViewBag.Tags = _tagRepository.Tags.ToList();

        return View(new PostCreateViewModel
        {
            PostId = post.PostId,
            Title = post.Title,
            Description = post.Description,
            Content = post.Content,
            Url = post.Url,
            IsActive = post.IsActive,
            Tags = post.Tags
        });
    }
    [Authorize]
    [HttpPost]
    public IActionResult Edit(PostCreateViewModel model, int[] tagIds)
    {
        if (ModelState.IsValid)
        {
            var entityToUpdate = new Post
            {
                PostId = model.PostId,
                Title = model.Title,
                Description = model.Description ?? "",
                Content = model.Content ?? "",
                Url = model.Url ?? ""
            };

            if (User.FindFirstValue(ClaimTypes.Role) == "admin")
            {
                entityToUpdate.IsActive = model.IsActive;
            }
            _postRepository.EditPost(entityToUpdate, tagIds);
            return RedirectToAction("List");
        }
        ViewBag.Tags = _tagRepository.Tags.ToList();
        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        if (!User.IsInRole("admin"))
    {
        TempData["Message"] = "This action is not authorized!";
        TempData["MessageType"] = "danger";
        return RedirectToAction("List");
    }
        var post = await _postRepository.Posts.FirstOrDefaultAsync(p => p.PostId == id);
        
        if (post == null)
        {
            return NotFound();
        }
        
        return View(post);  
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        if (!User.IsInRole("admin"))
    {
        TempData["Message"] = "This action is not authorized.!";
        TempData["MessageType"] = "danger";
        return RedirectToAction("List");
    }
        
        var post = await _postRepository.Posts.FirstOrDefaultAsync(p => p.PostId == id);
        
        if (post == null)
        {
            return NotFound();
        }
        
        _postRepository.DeletePost(post);
        
        return RedirectToAction("List");
    }
}