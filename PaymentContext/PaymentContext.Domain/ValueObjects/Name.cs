using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        AddNotifications(new Contract<Name>()
            .Requires()
            .IsGreaterThan(firstName, 3, "Name.FirstName", "Firstname should have at least three chars")
            .IsGreaterThan(lastName, 3, "Name.LastName", "Lastname should have at least three chars")
            .IsLowerThan(firstName, 50, "Name.FirstName", "Firstname should no more than fifty chars")
            .IsLowerThan(lastName, 50, "Name.LastName", "Lastname should no more than fifty chars"));
    }
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}