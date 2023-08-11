using Moq;

namespace UnitTesings.Test
{
    public class CreditApplicationShouldEvaluator
    {

        [Fact]
        public void AcceptHighIncomeApplications()
        {
            Mock<IFreqValidator> mockValidator = new Mock<IFreqValidator>();

            var sut = new CreditEvaluator(mockValidator.Object);

            var application = new CreditApplication { GrossAnnualIncome = 100_000 };

            CreditDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditDecision.AutoAccepted, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplications() 
        {
            Mock<IFreqValidator> mockValidator = new Mock<IFreqValidator>();

            mockValidator.Setup(x=>x.Equals("y")).Returns(true);

            var sut = new CreditEvaluator(mockValidator.Object);

            var application = new CreditApplication
            {
                GrossAnnualIncome = 19_999,
                Age = 42,
                FrequentFlyerNumber = "y"
            };

            CreditDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditDecision.AutoDeclined, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplications2()
        {
            Mock<IFreqValidator> mockValidator = new Mock<IFreqValidator>();
            // mockValidator.Setup(x=>x.Equals("y")).Returns(true);
            // mockValidator.Setup(x => x.IsValid(It.IsRegex("[a-z]"))).Returns(true);
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);

            var sut = new CreditEvaluator(mockValidator.Object);

            var application = new CreditApplication
            {
                GrossAnnualIncome = 19_999,
                Age = 42,
                FrequentFlyerNumber = "x"
            };

            CreditDecision decision = sut.Evaluate(application);

            Assert.Equal(CreditDecision.AutoDeclined, decision);
        }
    }
}