using System;

namespace CreditCardApplications
{
    public class CreditCardApplicationEvaluator
    {
        private const int AutoReferralMaxAge = 20;
        private const int HighIncomeThreshhold = 100_000;
        private const int LowIncomeThreshhold = 20_000;

        public CreditCardApplicationEvaluator(IFrequentFlyerNumberValidator frequentFlyerNumber)
        {
            this._FrecuentFlyerNumber = frequentFlyerNumber != null? frequentFlyerNumber : throw new ArgumentNullException(nameof(_FrecuentFlyerNumber));
        }

        public IFrequentFlyerNumberValidator _FrecuentFlyerNumber { get; private set; }

        public CreditCardApplicationDecision Evaluate(CreditCardApplication application)
        {
            bool isValidFrecuentFlyerNumber = _FrecuentFlyerNumber.IsValid(application.FrequentFlyerNumber);

            if(!isValidFrecuentFlyerNumber)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.GrossAnnualIncome >= HighIncomeThreshhold)
            {
                return CreditCardApplicationDecision.AutoAccepted;
            }

            if (application.Age <= AutoReferralMaxAge)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.GrossAnnualIncome < LowIncomeThreshhold)
            {
                return CreditCardApplicationDecision.AutoDeclined;
            }

            return CreditCardApplicationDecision.ReferredToHuman;
        }       
    }
}
