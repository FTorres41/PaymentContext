using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
            
        }

        public bool DocumentExists(string document)
        {
            if (document.Equals("99999999999"))
                return true;
            else
                return false;
        }

        public bool EmailExists(string email)
        {
            if (email.Equals("hello@world.com"))
                return true;
            else
                return false;
        }
    }
}
