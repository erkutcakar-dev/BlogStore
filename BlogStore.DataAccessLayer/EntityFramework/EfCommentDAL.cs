using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

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
            return _Context.Comments
                .Include(c => c.AppUser)   // Burada User navigation property'si
                .Where(c => c.ArticleId == id)
                .ToList();
        }

        public List<Comment> GetCommentsWithUser()
        {
            return _Context.Comments
                .Include(c => c.AppUser)
                .ToList();
        }

        public List<Comment> GetLatestComments(int count)
        {
            return _Context.Comments
                .Include(c => c.AppUser)
                .OrderByDescending(c => c.CommentDate)
                .Take(count)
                .ToList();
        }
    }
}
