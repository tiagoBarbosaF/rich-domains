using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests;

[TestClass]
public class DocumentTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenCnpjIsInvalid()
    {
        var doc = new Document("123", EDocumentType.Cnpj);
        Assert.IsTrue(!doc.IsValid);
    }
    [TestMethod]
    public void ShouldReturnSuccessWhenCnpjIsValid()
    {
        var doc = new Document("18448402000119", EDocumentType.Cnpj);
        Assert.IsTrue(doc.IsValid);
    }
    [TestMethod]
    public void ShouldReturnErrorWhenCpfIsInvalid()
    {
        var doc = new Document("123", EDocumentType.Cpf);
        Assert.IsTrue(!doc.IsValid);
    }
    [TestMethod]
    public void ShouldReturnSuccessWhenCpfIsValid()
    {
        var doc = new Document("81039629024", EDocumentType.Cpf);
        Assert.IsTrue(doc.IsValid);
    }
}