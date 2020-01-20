using Flunt.Validations;
using PaymentContext.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;
            _payments = new List<Payment>();
        }

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }
        private IList<Payment> _payments;

        public void AddPayment(Payment payment)
        {
            AddNotifications(
                    new Contract()
                    .Requires()
                    .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payment", "A data de pagamento deve ser futura")
                );

            //if (Valid) //Only adds to list if it's valid
            _payments.Add(payment);
        }

        public void Activate()
        {
            Active = true;
            LastUpdateDate = DateTime.Now;
        }

        public void Deactivate()
        {
            Active = false;
            LastUpdateDate = DateTime.Now;
        }
    }
}
