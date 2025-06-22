using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.DataAccessLayer.EntityFramework
{
    public class EfCommentDAL : GenericRepository<Comment>, ICommentDal
    {
        private readonly BlogContext _Context;
        public EfCommentDAL(BlogContext context) : base(context)
        {
            _Context = context;
        }

        public List<Comment> GetCommentsByArticle(int id)
        {
            throw new NotImplementedException();
        }
    }
}
