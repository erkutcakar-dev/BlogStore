using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.EntityFramework
{
    public class EfArticleDal : GenericRepository<Article>, IArticleDal
    {
        private readonly BlogContext _blogContext;
        public EfArticleDal(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public List<AppUser> GetAllAuthorsWithArticles()
        {
            return _blogContext.Users
                .Where(u => u.Articles.Any())
                .Include(u => u.Articles)
                .ToList();
        }

        public AppUser GetAppUserByArticleId(int id)
        {
            string userId = _blogContext.Articles.Where(x => x.ArticleId == id).Select(y => y.AppUserId).FirstOrDefault();
            var userValue = _blogContext.Users.Where(x => x.Id == userId).FirstOrDefault();
            return userValue;
        }

        public Article GetArticleBySlug(string slug)
        {
            return _blogContext.Articles
        .Include(x => x.AppUser)
        .Include(x => x.Category)
        .FirstOrDefault(x => x.Slug == slug);
        }

        public List<Article> GetArticlesByAppUser(string id)
        {
            return _blogContext.Articles
         .Include(x => x.Category)
         .Include(x => x.AppUser)
         .Where(x => x.AppUserId == id)
         .ToList();
        }

        public List<Article> GetArticlesByCategory(int categoryId)
        {
            return _blogContext.Articles
                .Include(x => x.AppUser)
                .Include(x => x.Category)
                .Where(x => x.CategoryId == categoryId)
                .ToList();
        }

        public List<Article> GetArticlesWithCategories()
        {
            return _blogContext.Articles.Include(x => x.Category).ToList();
        }

        public Article GetArticleWithUser(int id)
        {
            return _blogContext.Articles.Include(x => x.AppUser).FirstOrDefault(x => x.ArticleId == id);
        }

        public List<Article> GetTop3PopularArticles()
        {
            var values = _blogContext.Articles.OrderByDescending(x => x.ArticleId).Take(3).ToList();
            return values;
        }
    }
}