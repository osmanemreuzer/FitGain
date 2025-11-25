using FitGain.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitGain.ViewComponents;

public class NewPosts : ViewComponent
{
    private readonly IPostRepository _postRepository;
    public NewPosts(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View( await _postRepository
                        .Posts
                        .OrderByDescending(p => p.PublishedOn)
                        .Take(5)
                        .ToListAsync()
        );
    }
}