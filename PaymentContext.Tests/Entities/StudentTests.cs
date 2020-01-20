using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("João", "das Neves");
            _document = new Document("05164961903", EDocumentType.CPF);
            _email = new Email("joao@dasneves.com.west");
            _address = new Address("Castle Black", "", "", "", "", "", "");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);

        }

        [TestMethod]
        public void ShouldReturnErrorWhenHasActiveSubscription()
        {
            var payment = new PaypalPayment("123456", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _address, _document, "Night's Watch", _email);

            _subscription.AddPayment(payment);

            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        //[TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
           _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        //[TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var payment = new PaypalPayment("123456", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, _address, _document, "Night's Watch", _email);

            _subscription.AddPayment(payment);

            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Valid);
        }
    }
}
