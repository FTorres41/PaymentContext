using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        //Red, Green, Refactor
        //Red      = Falhar
        //Green    = Passar
        //Refactor = Alterar método

        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = string.Empty;
            command.Validate();

            Assert.AreEqual(false, command.Valid);
        }

        
    }
}
