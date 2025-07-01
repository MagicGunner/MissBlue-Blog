using Backend.Domain;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Domain.Entities;

namespace Backend.Modules.Blog.Domain.IRepository;

public interface ITagRepository : IBaseRepositories<Tag> {
}