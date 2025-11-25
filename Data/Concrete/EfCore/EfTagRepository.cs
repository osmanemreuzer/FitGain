using FitGain.Data.Abstract;
using FitGain.Data.Concrete.EfCore;
using FitGain.Entity;

namespace FitGain.Data.Concrete;

public class EfTagRepository : ITagRepository
{
    private readonly GymContext _context;
    public EfTagRepository(GymContext context)
    {
        _context = context;
    }
    public IQueryable<Tag> Tags => _context.Tags;

    public void CreatePost(Tag tag)
    {
        _context.Tags.Add(tag);
        _context.SaveChanges();
    }
}