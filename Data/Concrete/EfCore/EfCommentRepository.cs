using FitGain.Data.Abstract;
using FitGain.Data.Concrete.EfCore;
using FitGain.Entity;

namespace FitGain.Data.Concrete;

public class EfCommentRepository : ICommentRepository
{
    private readonly GymContext _context;
    public EfCommentRepository(GymContext context)
    {
        _context = context;
    }

    public IQueryable<Comment> Comments => _context.Comments;

    public void CreateComment(Comment comment)
    {
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }
}