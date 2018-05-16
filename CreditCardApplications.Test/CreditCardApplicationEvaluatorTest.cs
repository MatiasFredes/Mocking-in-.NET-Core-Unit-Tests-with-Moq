using System;
using Xunit;
using Moq;

namespace CreditCardApplications.Test
{
    public class CreditCardApplicationEvaluatorTest
    {
        private const string FRECUENT_FLYER_NUMBER = "1234A";
        [Fact]
        public void AcceptCredit()
        {
            Mock<IFrequentFlyerNumberValidator> frecuentFlyerValidator = new Mock<IFrequentFlyerNumberValidator>();

            CreditCardApplication creditCardApplication = new CreditCardApplication() { GrossAnnualIncome = 100000, FrequentFlyerNumber = FRECUENT_FLYER_NUMBER };
            CreditCardApplicationEvaluator creditCardApplicationEvaluator = new CreditCardApplicationEvaluator(frecuentFlyerValidator.Object);
            frecuentFlyerValidator.Setup(x => x.IsValid(It.Is<string>(number=>number.StartsWith("1")))).Returns(true);
            CreditCardApplicationDecision result = creditCardApplicationEvaluator.Evaluate(creditCardApplication);

            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, result);
        }

        [Fact]
        public void ReferYoungApplication()
        {
            Mock<IFrequentFlyerNumberValidator> frecuentFlyerValidator = new Mock<IFrequentFlyerNumberValidator>();
            CreditCardApplication creditCardApplication = new CreditCardApplication() { Age = 18 };
            CreditCardApplicationEvaluator creditCardApplicationEvaluator = new CreditCardApplicationEvaluator(frecuentFlyerValidator.Object);

            CreditCardApplicationDecision result = creditCardApplicationEvaluator.Evaluate(creditCardApplication);

            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, result);
        }
    }
}
