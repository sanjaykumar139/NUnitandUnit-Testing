using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesings
{
    public class CreditEvaluator
    {
        private const int AutoReferralMaxAge = 20;
        private const int HighIncomeThreshold = 100_000;
        private const int LowIncomeThreshold = 20_000;
        private readonly IFreqValidator _validator;
        public CreditEvaluator(IFreqValidator validator)
        {
            _validator = validator;
        }
        public CreditDecision Evaluate(CreditApplication application)
        {
            if (application.GrossAnnualIncome >= HighIncomeThreshold)
            {
                return CreditDecision.AutoAccepted;
            }
            var isValidFrequentFlyerNumber =
                _validator.IsValid(application.FrequentFlyerNumber);

            if (!isValidFrequentFlyerNumber)
            {
                return CreditDecision.Manual;
            }

            if (application.Age <= AutoReferralMaxAge)
            {
                return CreditDecision.Manual;
            }

            if (application.GrossAnnualIncome < LowIncomeThreshold)
            {
                return CreditDecision.AutoDeclined;
            }

            return CreditDecision.Manual;
        }
    }
}
