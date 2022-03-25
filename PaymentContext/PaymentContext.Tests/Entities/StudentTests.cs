using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    private readonly Student _student;
    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;

    public StudentTests()
    {
        _name = new Name("Tiago", "Barbosa");
        _document = new Document("67120741349", EDocumentType.Cpf);
        _email = new Email("tiago@gmail.com");
        _address = new Address("Rua 01", "1010", "TestVille", "Fortalcity", "CE", "Brazil", "60600500");
        _student = new Student(_name, _document, _email);
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var subscription = new Subscription(null);
        var payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Tiago", _document, _address, _email, "12345678");
        
        subscription.AddPayment(payment);
        _student.AddSubscription(subscription);
        _student.AddSubscription(subscription);
        
        Assert.IsTrue(!_student.IsValid);
    }

    // [TestMethod]
    // public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
    // {
    //     _student.AddSubscription(_subscription);
    //     
    //     Assert.IsTrue(!_student.IsValid);
    // }
    //
    // [TestMethod]
    // public void ShouldReturnSuccessWhenHadNoActiveSubscription()
    // {
    //     var payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Tiago", _document, _address, _email, "12345678");
    //     
    //     _subscription.AddPayment(payment);
    //     _student.AddSubscription(_subscription);
    //     
    //     Assert.IsTrue(_student.IsValid);
    // }
}