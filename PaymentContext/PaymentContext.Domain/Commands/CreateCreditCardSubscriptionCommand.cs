using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands;

public class CreateCreditCardSubscriptionCommand : Notifiable<Notification>, ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string BarCode { get; set; }
    public string CardHolderName { get; set; }
    public string CardNumber { get; set; }
    public string LastTransactionNumber { get; set; }
    public DateTime PaidDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public decimal Total { get; set; }
    public decimal TotalPaid { get; set; }
    public string Payer { get; set; }
    public string PayerDocument { get; set; }
    public EDocumentType PayerDocumentType { get; set; }
    public string PayerEmail { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    
    public void Validate()
    {
        AddNotifications(new Contract<Name>()
            .Requires()
            .IsGreaterThan(FirstName, 3, "Name.FirstName", "Firstname should have at least three chars")
            .IsGreaterThan(LastName, 3, "Name.LastName", "Lastname should have at least three chars")
            .IsLowerThan(FirstName, 50, "Name.FirstName", "Firstname should no more than fifty chars")
            .IsLowerThan(LastName, 50, "Name.LastName", "Lastname should no more than fifty chars"));
    }
}