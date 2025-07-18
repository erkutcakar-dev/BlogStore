﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T:class
    {
        void Insert(T entity);
        void Delete(int id);
        void Update (T entity);
        List<T> GetAll();
        T GetById(int id);


    }
}
