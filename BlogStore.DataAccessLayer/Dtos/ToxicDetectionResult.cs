using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.Dtos
{
    public class ToxicDetectionResult
    {
        public bool IsToxic { get; set; }
        public double ToxicityScore { get; set; }
        public string Category { get; set; }
        public string? Reason { get; set; }
        public bool IsAnalyzed { get; set; }
    }
}