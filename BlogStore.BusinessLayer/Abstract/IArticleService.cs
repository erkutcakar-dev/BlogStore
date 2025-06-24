using System.Collections.Generic;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface IArticleService : IGenericService<Article>
    {
        public List<Article> TGetArticlesWithCategories();
        public AppUser TGetAppUserByArticleId(int id);
        public List<Article> TGetTop3PopulerArticles();

    }
}
