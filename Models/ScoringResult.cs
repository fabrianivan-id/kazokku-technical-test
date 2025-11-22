using System.Collections.Generic;

namespace DmsCreditScoring.Models
{
    public class ScoringResult
    {
        public double TotalScore { get; set; }
        public string RiskLevel { get; set; } = string.Empty;

        // detail per group (INFORMASI 1..6)
        public Dictionary<string, double> GroupScores { get; set; } = new();
    }
}
