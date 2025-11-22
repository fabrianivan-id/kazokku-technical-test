using DmsCreditScoring.Models;

namespace DmsCreditScoring.Services
{
    public interface IScoringService
    {
        ScoringResult Calculate(ApplicationForm form);
    }
}
