using FitGain.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitGain.ViewComponents;

public class TagsMenu : ViewComponent
{
    private readonly ITagRepository _tagRepository;
    public TagsMenu(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View( await _tagRepository.Tags.ToListAsync());
    }
}