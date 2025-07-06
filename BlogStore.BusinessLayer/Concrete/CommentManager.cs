using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;
        private readonly IToxicDetectionService _toxicDetectionService;

        public CommentManager(ICommentDal commentDal, IToxicDetectionService toxicDetectionService)
        {
            _commentDal = commentDal;
            _toxicDetectionService = toxicDetectionService;
        }

        public void TDelete(int id)
        {
            _commentDal.Delete(id);
        }

        public List<Comment> TGetAll()
        {
            return _commentDal.GetAll();
        }

        public Comment TGetById(int id)
        {
            return _commentDal.GetById(id);
        }

        public List<Comment> TGetCommentsByArticle(int id)
        {
            return _commentDal.GetCommentsByArticle(id);
        }

        public List<Comment> TGetCommentsWithUser()
        {
            return _commentDal.GetCommentsWithUser();
        }

        public List<Comment> TGetLatestComments(int count)
        {
            return _commentDal.GetLatestComments(count);
        }

        public void TInsert(Comment entity)
        {
            _commentDal.Insert(entity);
        }

        public void TUpdate(Comment entity)
        {
            _commentDal.Update(entity);
        }

        // Toxic Detection metodları
        public async Task<bool> TInsertWithToxicCheckAsync(Comment entity)
        {
            try
            {
                var toxicResult = await _toxicDetectionService.AnalyzeCommentAsync(entity.CommentDetail);

                entity.IsToxic = toxicResult.IsToxic;
                entity.ToxicityScore = toxicResult.ToxicityScore;
                entity.ToxicityCategory = toxicResult.Category;
                entity.ToxicityAnalyzedDate = DateTime.Now;
                entity.ToxicityReason = toxicResult.Reason;

                if (toxicResult.IsToxic)
                {
                    entity.IsValid = false; // Moderasyona gönder
                    _commentDal.Insert(entity);
                    return false; // Yorum reddedildi
                }
                else
                {
                    entity.IsValid = true; // Onaylandı
                    _commentDal.Insert(entity);
                    return true; // Yorum onaylandı
                }
            }
            catch (Exception)
            {
                entity.IsValid = false; // Güvenli taraf
                entity.IsToxic = null; // Analiz edilemedi
                _commentDal.Insert(entity);
                return false;
            }
        }

        public async Task TAnalyzeExistingCommentsAsync()
        {
            var commentsToAnalyze = _commentDal.GetAll()
                .Where(c => c.IsToxic == null)
                .ToList();

            foreach (var comment in commentsToAnalyze)
            {
                try
                {
                    var toxicResult = await _toxicDetectionService.AnalyzeCommentAsync(comment.CommentDetail);

                    comment.IsToxic = toxicResult.IsToxic;
                    comment.ToxicityScore = toxicResult.ToxicityScore;
                    comment.ToxicityCategory = toxicResult.Category;
                    comment.ToxicityAnalyzedDate = DateTime.Now;
                    comment.ToxicityReason = toxicResult.Reason;

                    if (toxicResult.IsToxic && comment.IsValid)
                    {
                        comment.IsValid = false;
                    }

                    _commentDal.Update(comment);
                }
                catch
                {
                    continue;
                }
            }
        }

        public List<Comment> TGetToxicComments()
        {
            return _commentDal.GetAll()
                .Where(c => c.IsToxic == true)
                .OrderByDescending(c => c.ToxicityScore)
                .ToList();
        }

        public List<Comment> TGetPendingModerationComments()
        {
            return _commentDal.GetAll()
                .Where(c => !c.IsValid && (c.IsToxic == true || c.IsToxic == null))
                .OrderByDescending(c => c.CommentDate)
                .ToList();
        }

        public void TApproveComment(int commentId)
        {
            var comment = _commentDal.GetById(commentId);
            if (comment != null)
            {
                comment.IsValid = true;
                _commentDal.Update(comment);
            }
        }

        public void TRejectComment(int commentId)
        {
            var comment = _commentDal.GetById(commentId);
            if (comment != null)
            {
                comment.IsValid = false;
                _commentDal.Update(comment);
            }
        }
    }
}