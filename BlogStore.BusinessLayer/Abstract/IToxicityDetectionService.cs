using BlogStore.DataAccessLayer.Dtos;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface IToxicDetectionService
    {
        Task<ToxicDetectionResult> AnalyzeCommentAsync(string commentText);
        Task<bool> IsCommentToxicAsync(string commentText);
        ToxicDetectionResult AnalyzeCommentSync(string commentText);
    }
}