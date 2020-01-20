using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        private readonly FakeStudentRepository _mockStudentRepository;
        private readonly FakeEmailService _mockEmailService;
        public CreateBoletoSubscriptionCommand _boletoCommand;

        public SubscriptionHandlerTests()
        {
            _mockEmailService = new FakeEmailService();
            _mockStudentRepository = new FakeStudentRepository();

            _boletoCommand = new CreateBoletoSubscriptionCommand
            {
                BarCode = "1234567890123",
                BoletoNumber = "111",
                City = "Curitiba",
                Country = "Brasil",
                Document = "99999999999",
                Email = "hello@world.com",
                ExpireDate = DateTime.Now.AddDays(1),
                FirstName = "Bruce",
                LastName = "Wayne",
                Neighborhood = "Gotham",
                PaidDate = DateTime.Now,
                Payer = "Alfred",
                PayerDocument = "88888888888",
                PayerDocumentType = Domain.Enums.EDocumentType.CPF,
                PayerEmail = "alf@red.net",
                PaymentNumber = "987654321",
                State = "PR",
                Street = "Rua Top",
                StreetNumber = "666",
                Total = 60,
                TotalPaid = 60,
                ZipCode = "88888-777"
            };
        }

        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(_mockStudentRepository, _mockEmailService);

            _boletoCommand.Document = "99999999999";

            handler.Handle(_boletoCommand);

            Assert.IsTrue(handler.Invalid);
        }
    }
}
