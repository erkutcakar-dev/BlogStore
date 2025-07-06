using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Helpers
{
    public class SlugHelper
    {
        public static string GenerateSlug(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return string.Empty;
            // Convert to lower case
            title = title.ToLowerInvariant();
            // Replace spaces with hyphens
            title = title.Replace(" ", "-");
            // Remove invalid characters
            var validChars = new StringBuilder();
            foreach (var c in title)
            {
                if (char.IsLetterOrDigit(c) || c == '-')
                {
                    validChars.Append(c);
                }
            }
            return validChars.ToString().Trim('-');
        }
    }
}
