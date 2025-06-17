using System;
using System.Collections.Generic;
using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.BusinessLayer.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IArticleDal _articleDal;

        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }

        public void TDelete(int id)
        {
            _articleDal.Delete(id);
        }

        public List<Article> TGetAll()
        {
            return _articleDal.GetAll();
        }

        public Article TGetById(int id)
        {
            return _articleDal.GetById(id);
        }

        public void TInsert(Article entity)
        {
            if (!string.IsNullOrEmpty(entity.Title) &&
                entity.Title.Length >= 10 &&
                entity.Title.Length <= 100 &&
                !string.IsNullOrEmpty(entity.Description) &&
                !string.IsNullOrEmpty(entity.ImageUrl) &&
                entity.ImageUrl.Contains("a"))
            {
                _articleDal.Insert(entity);
            }
            else
            {
                throw new ArgumentException("Geçersiz veri: Başlık 10-100 karakter aralığında olmalı, açıklama ve görsel URL boş olmamalı, URL 'a' harfi içermeli.");
            }
        }

        public void TUpdate(Article entity)
        {
            _articleDal.Update(entity);
        }
    }
}
