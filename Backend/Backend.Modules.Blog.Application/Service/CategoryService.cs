﻿using AutoMapper;
using Backend.Application.Service;
using Backend.Common.Results;
using Backend.Contracts;
using Backend.Contracts.IService;
using Backend.Domain;
using Backend.Domain.IRepository;
using Backend.Modules.Blog.Contracts.DTO;
using Backend.Modules.Blog.Contracts.IService;
using Backend.Modules.Blog.Contracts.VO;
using Backend.Modules.Blog.Domain.Entities;
using Backend.Modules.Blog.Domain.IRepository;
using SqlSugar;

namespace Backend.Modules.Blog.Application.Service;

public class CategoryService(IMapper mapper, IBaseRepositories<Category> baseRepositories, ICategoryRepository categoryRepository)
    : BaseServices<Category>(mapper, baseRepositories), ICategoryService {
    private readonly IMapper _mapper = mapper;

    public async Task<List<CategoryVO>> ListAll() {
        var categoryVos = await Query<CategoryVO>();
        var dic = await categoryRepository.GetCountOfCategoryDic(categoryVos.Select(c => c.Id).ToList());
        return categoryVos.Select(c => {
                                      c.ArticleCount = dic[c.Id];
                                      return c;
                                  })
                          .ToList();
    }

    public async Task<bool>        Add(CategoryDto    categoryDto) => await categoryRepository.InsertOrUpdate(_mapper.Map<Category>(categoryDto));
    public async Task<bool>        Update(CategoryDto categoryDto) => await categoryRepository.InsertOrUpdate(_mapper.Map<Category>(categoryDto));
    public async Task<CategoryVO?> GetById(long       id)          => (await Query<CategoryVO>(i => i.Id == id)).FirstOrDefault();

    public async Task<List<CategoryVO>> SearchCategory(SearchCategoryDTO searchCategoryDto) {
        var categories = await categoryRepository.SearchCategory(searchCategoryDto.CategoryName, searchCategoryDto.StartTime, searchCategoryDto.EndTime);

        var countDic = await categoryRepository.GetCountOfCategoryDic(categories.Select(c => c.Id).ToList());

        return categories.Select(c => {
                                     var vo = _mapper.Map<CategoryVO>(c);
                                     if (countDic.TryGetValue(c.Id, out var count)) vo.ArticleCount = count;
                                     return vo;
                                 })
                         .ToList();
    }
}