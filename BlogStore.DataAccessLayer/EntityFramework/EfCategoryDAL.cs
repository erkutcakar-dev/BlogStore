using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Dtos;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.DataAccessLayer.EntityFramework
{
    public class EfCategoryDAL : GenericRepository<Category>, ICategoryDal
    {
        private readonly BlogContext _context;
        public EfCategoryDAL(BlogContext context) : base(context)
        {
            _context = context;
        }


        public List<CategoryWithArticleCountDto> GetCategoriesWithArticlesCount()
        {
            var result = _context.Categories.Select(c => new CategoryWithArticleCountDto
            {
                CategoryName = c.CategoryName,
                ArticleCount = _context.Articles.Count(a => a.CategoryId == c.CategoryId)
            })
                 .ToList();
            return result;
        }
    }
    
}
