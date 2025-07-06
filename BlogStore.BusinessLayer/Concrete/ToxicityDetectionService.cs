using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Concrete
{
    public class ToxicDetectionService : IToxicDetectionService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        private readonly string[] _toxicWords = {
           "bu alana küfür içerikli sözler girilecektir."
        };

        public ToxicDetectionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ToxicDetectionResult> AnalyzeCommentAsync(string commentText)
        {
            if (string.IsNullOrWhiteSpace(commentText))
            {
                return CreateCleanResult();
            }

            try
            {
                var simpleResult = AnalyzeCommentSync(commentText);

                if (simpleResult.IsToxic)
                {
                    return simpleResult;
                }

                return await AnalyzeWithApiAsync(commentText) ?? simpleResult;
            }
            catch (Exception)
            {
                return AnalyzeCommentSync(commentText);
            }
        }

        public async Task<bool> IsCommentToxicAsync(string commentText)
        {
            var result = await AnalyzeCommentAsync(commentText);
            return result.IsToxic;
        }

        public ToxicDetectionResult AnalyzeCommentSync(string commentText)
        {
            if (string.IsNullOrWhiteSpace(commentText))
            {
                return CreateCleanResult();
            }

            var normalizedText = NormalizeText(commentText);
            var foundToxicWords = _toxicWords.Where(word => normalizedText.Contains(NormalizeText(word))).ToList();

            var isToxic = foundToxicWords.Any();
            var score = isToxic ? Math.Min(0.8 + (foundToxicWords.Count * 0.1), 1.0) : 0.1;

            return new ToxicDetectionResult
            {
                IsToxic = isToxic,
                ToxicityScore = score,
                Category = isToxic ? "profanity" : "clean",
                Reason = isToxic ? $"Uygunsuz kelimeler: {string.Join(", ", foundToxicWords)}" : null,
                IsAnalyzed = true
            };
        }

        private string NormalizeText(string text)
        {
            return text.ToLowerInvariant()
                .Replace("ı", "i")
                .Replace("ğ", "g")
                .Replace("ü", "u")
                .Replace("ş", "s")
                .Replace("ö", "o")
                .Replace("ç", "c")
                .Replace("İ", "i")
                .Replace("Ğ", "g")
                .Replace("Ü", "u")
                .Replace("Ş", "s")
                .Replace("Ö", "o")
                .Replace("Ç", "c");
        }

        private ToxicDetectionResult CreateCleanResult()
        {
            return new ToxicDetectionResult
            {
                IsToxic = false,
                ToxicityScore = 0.0,
                Category = "clean",
                IsAnalyzed = true
            };
        }

        private async Task<ToxicDetectionResult?> AnalyzeWithApiAsync(string commentText)
        {
            try
            {
                var apiUrl = _configuration["ToxicBert:ApiUrl"];
                var apiKey = _configuration["ToxicBert:ApiKey"];

                if (string.IsNullOrEmpty(apiUrl) || string.IsNullOrEmpty(apiKey))
                {
                    return null;
                }

                var request = new { inputs = commentText };
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                var response = await _httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return new ToxicDetectionResult
                    {
                        IsToxic = false,
                        ToxicityScore = 0.1,
                        Category = "clean",
                        IsAnalyzed = true
                    };
                }
            }
            catch
            {
                // API hatası
            }

            return null;
        }
    }
}
