using System.Diagnostics;
using System.Globalization;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Document : ValueObject
{
    public Document(string number, EDocumentType type)
    {
        Number = number;
        Type = type;
        
        AddNotifications(new Contract<Document>()
            .Requires()
            .IsTrue(Validate(), "Document.Number", "Invalid document"));
    }

    public string Number { get; private set; }
    public EDocumentType Type { get; private set; }

    private bool Validate()
    {
        var countDocument = Number.Length;
        return countDocument switch
        {
            14 => Type == EDocumentType.Cnpj,
            11 => Type == EDocumentType.Cpf,
            _ => false
        };
    }
}