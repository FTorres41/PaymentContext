using System;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{

    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(string cardHolderName, 
                                    string cardNumber, 
                                    string lastTransactionNumber, 
                                    DateTime paidDate, 
                                    DateTime expireDate, 
                                    decimal total, 
                                    decimal totalPaid,
                                    Address address,
                                    Document document, 
                                    string payer,
                                    Email email) : base(paidDate, expireDate, total, totalPaid, address, document, payer, email)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }

        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string LastTransactionNumber { get; set; }
    }
}
