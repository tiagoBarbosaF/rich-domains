using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;

[TestClass]
public class SubscriptionHandlerTests
{
    public void ShouldReturnErrorWhenDocumentExists()
    {
        var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
        var command = new CreateBoletoSubscriptionCommand();
        
    command.FirstName = "Tiago";
    command.LastName = "Barbosa";
    command.Document = "99999999999";
    command.Email = "teste2@gmail.com";
    command.BarCode = "123456789";
    command.BoletoNumber = "123455";
    command.PaymentNumber = "12345";
    command.PaidDate = DateTime.Now;
    command.ExpireDate = DateTime.Now.AddMonths(1);
    command.Total = 10;
    command.TotalPaid = 10;
    command.Payer = command.FirstName;
    command.PayerDocument = command.Document;
    command.PayerDocumentType = EDocumentType.Cpf;
    command.PayerEmail = command.Email;
    command.Street = "Rua teste";
    command.Number = "1212";
    command.Neighborhood = "TesteTest";
    command.City = "Fortal";
    command.State = "CE";
    command.Country = "Brazil";
    command.ZipCode = "60600600";

    handler.Handle(command);
    Assert.AreEqual(false, handler.IsValid);
    }
}