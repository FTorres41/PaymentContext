using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : 
        Notifiable, 
        IHandler<CreateBoletoSubscriptionCommand>, 
        IHandler<CreatePayPalSubscriptionCommand>,
        IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar o cadastro");
            }

            //Check if document is already registered
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso");

            //Check if email is already registered
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este e-mail já está em uso");

            //Create VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.StreetNumber, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var payerDocument = new Document(command.PayerDocument, command.PayerDocumentType);

            //Create Entitites
            var student = new Student(name, document, email);
            var subscription = new Subscription(command.ExpireDate);
            var payment = new BoletoPayment(
                command.BarCode, 
                command.BoletoNumber, 
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                address, 
                payerDocument, 
                command.Payer,
                email);

            //Relationships
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Apply Validations
            AddNotifications(name, document, email, address, student, subscription, payment);

            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //Save data
            _repository.CreateSubscription(student);

            //Sent Welcome email
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem-vindo!", "Sua assinatura foi criada");

            //Return
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar o cadastro");
            }

            //Check if document is already registered
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso");

            //Check if email is already registered
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este e-mail já está em uso");

            //Create VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.StreetNumber, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var payerDocument = new Document(command.PayerDocument, command.PayerDocumentType);

            //Create Entitites
            var student = new Student(name, document, email);
            var subscription = new Subscription(command.ExpireDate);
            var payment = new PaypalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                address,
                payerDocument,
                command.Payer,
                email);

            //Relationships
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Apply Validations
            AddNotifications(name, document, email, address, student, subscription, payment);
            
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //Save data
            _repository.CreateSubscription(student);

            //Sent Welcome email
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem-vindo!", "Sua assinatura foi criada");

            //Return
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar o cadastro");
            }

            //Check if document is already registered
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso");

            //Check if email is already registered
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este e-mail já está em uso");

            //Create VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.StreetNumber, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var payerDocument = new Document(command.PayerDocument, command.PayerDocumentType);

            //Create Entitites
            var student = new Student(name, document, email);
            var subscription = new Subscription(command.ExpireDate);
            var payment = new CreditCardPayment(
                command.CardHolderName,
                command.CardNumber,
                command.LastTransactionNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                address,
                payerDocument,
                command.Payer,
                email);

            //Relationships
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Apply Validations
            AddNotifications(name, document, email, address, student, subscription, payment);

            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            //Save data
            _repository.CreateSubscription(student);

            //Sent Welcome email
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem-vindo!", "Sua assinatura foi criada");

            //Return
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
