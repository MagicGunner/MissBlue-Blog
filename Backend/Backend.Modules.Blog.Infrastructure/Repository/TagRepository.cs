using System.Linq.Expressions;
using Backend.Infrastructure;
using Backend.Infrastructure.Repository;
using Backend.Infrastructure.UnitOfWorks;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;
using SqlSugar;

namespace Backend.Modules.Blog.Infrastructure.Repository;

public class TagRepository(IUnitOfWorkManage unitOfWorkManage) : BaseRepositories<Tag>(unitOfWorkManage), ITagRepository;