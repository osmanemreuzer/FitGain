using FitGain.Entity;

namespace FitGain.Data.Abstract;

public interface ICommentRepository
{
    IQueryable<Comment> Comments { get; }
    void CreateComment(Comment comment);
}