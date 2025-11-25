using FitGain.Entity;

namespace FitGain.Data.Abstract;

public interface ITagRepository
{
    IQueryable<Tag> Tags { get; }
    void CreatePost(Tag tag);
}