using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
        public List<Comment> TGetCommentsByArticle(int id);
        public List<Comment> TGetLatestComments(int count);
        public List<Comment> TGetCommentsWithUser();

        Task<bool> TInsertWithToxicCheckAsync(Comment entity);
        Task TAnalyzeExistingCommentsAsync();
        List<Comment> TGetToxicComments();
        List<Comment> TGetPendingModerationComments();
        void TApproveComment(int commentId);
        void TRejectComment(int commentId);
    }
}