using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable<Notification>,
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
        // Fail fast validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Unable to realize the subscribe.");
        }
        
        // Verificar se o Documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "This document is already in use.");

        // Verificar se o E-mail já está cadastrado
        if (_repository.EmailExists(command.Email)) AddNotification("Email", "This E-mail is already in use.");

        // Gerar VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.Cpf);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State,
            command.Country, command.ZipCode);
        
        // Gerar Entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(
            command.PaidDate, 
            command.ExpireDate, 
            command.Total, 
            command.TotalPaid,
            command.Payer,
            new Document(command.PayerDocument,
                command.PayerDocumentType),
            address,
            email,
            command.BarCode,
            command.BoletoNumber);
        
        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);
        
        // Agrupar as validações
        AddNotifications(name, document, email, address, student, subscription, payment);
        
        // Salvar informações
        _repository.CreateSubscription(student);
        
        // Enviar E-mail de boas-vindas
        _emailService.Send(student.Name.ToString(), student.Email.Address, "Welcome!", "Welcome to site.");
        
        // Retornar informações
        return new CommandResult(true, "Subscription finished was success");
    }

    public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Unable to realize the subscribe.");
        }
        
        // Verificar se o Documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "This document is already in use.");

        // Verificar se o E-mail já está cadastrado
        if (_repository.EmailExists(command.Email)) AddNotification("Email", "This E-mail is already in use.");

        // Gerar VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.Cpf);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State,
            command.Country, command.ZipCode);
        
        // Gerar Entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new PayPalPayment(
            command.PaidDate, 
            command.ExpireDate, 
            command.Total, 
            command.TotalPaid,
            command.Payer,
            new Document(command.PayerDocument,
                command.PayerDocumentType),
            address,
            email,
            command.TransactionCode);
        
        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);
        
        // Agrupar as validações
        AddNotifications(name, document, email, address, student, subscription, payment);
        
        // Salvar informações
        _repository.CreateSubscription(student);
        
        // Enviar E-mail de boas-vindas
        _emailService.Send(student.Name.ToString(), student.Email.Address, "Welcome!", "Welcome to site.");
        
        // Retornar informações
        return new CommandResult(true, "Subscription finished was success");
    }

    public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Unable to realize the subscribe.");
        }
        
        // Verificar se o Documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "This document is already in use.");

        // Verificar se o E-mail já está cadastrado
        if (_repository.EmailExists(command.Email)) AddNotification("Email", "This E-mail is already in use.");

        // Gerar VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.Cpf);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State,
            command.Country, command.ZipCode);
        
        // Gerar Entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new CreditCardPayment(
            command.PaidDate, 
            command.ExpireDate, 
            command.Total, 
            command.TotalPaid,
            command.Payer,
            new Document(command.PayerDocument,
                command.PayerDocumentType),
            address,
            email,
            command.CardHolderName, 
            command.CardNumber, 
            command.LastTransactionNumber);
        
        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);
        
        // Agrupar as validações
        AddNotifications(name, document, email, address, student, subscription, payment);
        
        // Checar as notificacoes
        if (!IsValid)
            return new CommandResult(false, "Not possible conclude you subscription.");
        
        // Salvar informações
        _repository.CreateSubscription(student);
        
        // Enviar E-mail de boas-vindas
        _emailService.Send(student.Name.ToString(), student.Email.Address, "Welcome!", "Welcome to site.");
        
        // Retornar informações
        return new CommandResult(true, "Subscription finished was success");
    }
}