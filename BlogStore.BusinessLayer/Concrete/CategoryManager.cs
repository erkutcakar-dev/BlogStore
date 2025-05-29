﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void TDelete(int id)
        {
           _categoryDal.Delete(id);
        }

        public List<Category> TGetAll()
        {
          return _categoryDal.GetAll();
        }

        public Category TGetById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public void TInsert(Category entity)
        {
            if(entity.CategoryName!="" && entity.CategoryName.Length>=3 && entity.CategoryName.Length<=30 && entity.CategoryName.Contains('a'))
            {
                _categoryDal.Insert(entity);
            }
            else
            {
                //Hata Mesajı
            }
               
        }

        public void TUpdate(Category entity)
        {
            if(entity.CategoryName !=null)
            {
                _categoryDal.Update(entity);
            }
            else
            {
                //Hata mesajı
            }
           
        }
    }
}
