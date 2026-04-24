namespace Fundamentals;
/*

Bu örnekte müşteri listesi üzerinden farklı stratejilerde sıralamalar yapmak istiyoruz.
Örneğin Customer sınıfının ilgili alanlarına göre sıralamalar dışında kompozit sıralamaları da Sort metoduna öğretmek istiyoruz.

Önce şehire göre alfabetik sonra oradaki müşterilerin doğum tarihlerine göre sıralama yapmak istediğimizi düşünelim.
*/
public class InterfaceUsagesV2
{
    public static void Run()
    {
        var customers = new List<Customer>
        {
            new() { RegistrationId = 1, FirstName = "Alice", LastName = "Smith", City = "New York", BirthDate = new DateTime(1990, 5, 1) },
            new() { RegistrationId = 2, FirstName = "Bob", LastName = "Johnson", City = "Los Angeles", BirthDate = new DateTime(1985, 8, 15) },
            new() { RegistrationId = 3, FirstName = "Charlie", LastName = "Brown", City = "Chicago", BirthDate = new DateTime(1992, 12, 20) },
            new() { RegistrationId = 4, FirstName = "David", LastName = "Wilson", City = "New York", BirthDate = new DateTime(1988, 3, 10) },
            new() { RegistrationId = 5, FirstName = "Eve", LastName = "Davis", City = "Los Angeles", BirthDate = new DateTime(1995, 7, 25) },
            new() { RegistrationId = 6, FirstName = "Frank", LastName = "Miller", City = "Chicago", BirthDate = new DateTime(1987, 11, 5) },
            new() { RegistrationId = 7, FirstName = "Grace", LastName = "Garcia", City = "New York", BirthDate = new DateTime(1991, 2, 18) },
        };

        var comparer = new CustomerComparer(new CustomerSortByFirstCityThenBirthDate());
        customers.Sort(comparer);

        foreach (var customer in customers)
        {
            Console.WriteLine(customer);
        }
    }
}

public class Customer
{
    public int RegistrationId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string City { get; set; }
    public DateTime BirthDate { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} from {City}, born on {BirthDate:d}";
    }
}

public class CustomerComparer
    : IComparer<Customer>
{
    private readonly ICustomerSortStrategy _sortStrategy;

    public CustomerComparer(ICustomerSortStrategy sortStrategy)
    {
        _sortStrategy = sortStrategy;
    }
    public int Compare(Customer? x, Customer? y)
    {
        return _sortStrategy.Compare(x, y);
    }
}

public interface ICustomerSortStrategy
{
    int Compare(Customer? x, Customer? y);
}

public class CustomerSortByFirstCityThenBirthDate
    : ICustomerSortStrategy
{
    public int Compare(Customer? x, Customer? y)
    {
        if (x == null) return -1;
        if (y == null) return 1;
        int cityComparison = string.Compare(x.City, y.City, StringComparison.Ordinal);
        if (cityComparison != 0)
            return cityComparison;
        return DateTime.Compare(x.BirthDate, y.BirthDate);
    }
}

public class CustomerSortByFirstName 
    : ICustomerSortStrategy
{
    public int Compare(Customer? x, Customer? y)
    {
        if (x == null) return -1;
        if (y == null) return 1;
        return string.Compare(x.FirstName, y.FirstName, StringComparison.Ordinal);
    }
}

public class CustomerSortByLastName
    : ICustomerSortStrategy
{
    public int Compare(Customer? x, Customer? y)
    {
        if (x == null) return -1;
        if (y == null) return 1;
        return string.Compare(x.LastName, y.LastName, StringComparison.Ordinal);
    }
}

public class CustomerSortByCity
    : ICustomerSortStrategy
{
    public int Compare(Customer? x, Customer? y)
    {
        if (x == null) return -1;
        if (y == null) return 1;
        return string.Compare(x.City, y.City, StringComparison.Ordinal);
    }
}