using Testing.JobApplication.Models;
using Moq;
using Testing.JobApplication.Services;

namespace Testing.JobApplication.UnitTest
{
    public class ApplicationEvaluateUnitTest
    {
        //Naming rule:
        //WhatWeTest_Condition_WhatResultShouldBe
        //UnitOfWork_Condition_ExpectedResult
        //Condition_ExpectedResult
        [Test]
        public void Application_WithUnderAge_TransferredToAutoRejecet()
        {
            //Arrange - call methods or classes here
            var mockValidator = new Mock<IIdentityValidatior>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new Testing.JobApplication.Models.JobApplication()
            {
                Applicant = new Applicant() { Age = 17,IdentityNumber="2FSAD" },
                YearsOfExperience = 2,
                TechStackList = new() { "C#" }
            };

            //Action - operations go here
            var appResult = evaluator.Evalaute(form);


            //Assert - results go here
            Assert.AreEqual(ApplicationResult.AutoRejected, appResult);

        }

        [Test]
        public void Application_WithUnderSkillsRate_TransferredToAutoRejected()
        {
            //Arrange
            //Moq : uses interface as a class
            var mockValidator = new Mock<IIdentityValidatior>();
            //Configure/Setup IsValid method using Moq
            //IsAny method => allows the function returns true in every parameters
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
                

            var evaluator = new ApplicationEvaluator(mockValidator.Object);

            var form = new Testing.JobApplication.Models.JobApplication()
            {
                Applicant = new Applicant() { Age = 21, IdentityNumber = "3SDA2" },
                YearsOfExperience = 2,
                TechStackList = new() { "C#" }
            };
            //Action
            var result = evaluator.Evalaute(form);

            //Assert
            Assert.AreEqual(ApplicationResult.AutoRejected, result);
        }

        [Test]
        public void Application_WithUpperSkillsAndExperience_TransformedToAutoAccept()
        {
            //Arrange
            var mockValidator = new Mock<IIdentityValidatior>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);

            var form = new Testing.JobApplication.Models.JobApplication()
            {
                Applicant = new Applicant() { Age = 32,IdentityNumber="4IOD3" },
                YearsOfExperience = 9,
                TechStackList = new() { "C#", "javascript", "Typescript", "Angular", "Rest api" }
            };
            //Action
            var result = evaluator.Evalaute(form);

            //Assert
            Assert.AreEqual(ApplicationResult.AutoAccepted, result);
        }

        [Test]
        public void Application_WithInvalidIdentity_TransferredToHr()
        {
            //Arrange
            var mockValidator = new Mock<IIdentityValidatior>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);

            var form = new Testing.JobApplication.Models.JobApplication()
            {
                Applicant = new Applicant() { Age = 22, IdentityNumber = "4IOD3" },
                YearsOfExperience = 2,
                TechStackList = new() { "javascript", "Typescript", "Angular" }
            };
            //Action
            var result = evaluator.Evalaute(form);

            //Assert
            Assert.AreEqual( ApplicationResult.TransferredToHr, result);
        }


    }


}
