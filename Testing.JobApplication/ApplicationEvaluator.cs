using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.JobApplication.Models;
using Testing.JobApplication.Services;

namespace Testing.JobApplication
{
    public class ApplicationEvaluator
    {
        private readonly IIdentityValidatior identityValidatior;
        private const int minAge = 18;
        private const int autoAcceptedYearsOfExperience = 7;
        private List<string> techStackList = new() { "C#", "Javascript", "Angular", "Rest Api" };

        public ApplicationEvaluator(IIdentityValidatior identityValidatior)
        {
            this.identityValidatior = identityValidatior;
        }

        public ApplicationResult Evalaute(Testing.JobApplication.Models.JobApplication form)
        {
            var similarityRate = GetTechStackListSimilarityRate(form.TechStackList);
            var validIdentity = identityValidatior.IsValid(form.Applicant.IdentityNumber);

            if (!validIdentity)
                return ApplicationResult.TransferredToHr;

            if (form.Applicant.Age < minAge)
                return ApplicationResult.AutoRejected;
            else
            {
                if (similarityRate <= 25)
                    return ApplicationResult.AutoRejected;
                else if (similarityRate > 75 && form.YearsOfExperience >= autoAcceptedYearsOfExperience)
                    return ApplicationResult.AutoAccepted;
            }
            return ApplicationResult.AutoAccepted;
        }


        private int GetTechStackListSimilarityRate(List<string> techSkills)
        {
            var matchedCount = techStackList
                .Where(x => techSkills.Contains(x, StringComparer.OrdinalIgnoreCase))
                .Count();
            return (int)((double)matchedCount / techStackList.Count * 100);
        }

    }

    public enum ApplicationResult
    {
        AutoRejected,
        TransferredToHr,
        TransferredToLead,
        TransferredToCTO,
        AutoAccepted
    }

}
