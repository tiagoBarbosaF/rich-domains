using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string address)
    {
        Address = address;
        
        AddNotifications(new Contract<Email>()
            .Requires()
            .IsEmail(address, "Email.Address", "Invalid Email"));
    }

    public string Address { get; private set; }
}