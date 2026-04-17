namespace Fundamentals;

/*
    .Net içerisinde de sıklıkla kullanılan birçok Interface türü vardır.
    IEnumerable, IDisposable, IComparable, ICloneable, IEquatable gibi arayüzler bunlara örnek olarak verilebilir.
*/

public class InterfaceUsages
{
    public static void Run()
    {
        var deppo = new List<Stock>
        {
            new() { ProductName = "Laptop", StockQuantity = 10 },
            new() { ProductName = "Mouse", StockQuantity = 50 },
            new() { ProductName = "Keyboard", StockQuantity = 30 }
        };
        // deppo.Sort(); // deppo isimli Stock türünden nesneler taşıyan List koleksiyonunu sıralıyoruz. Ama neye göre ???

        // deppo.Sort(new StockComparer(SortDirection.Descending, SortField.StockQuantity)); 
        deppo.Sort(new StockComparer(SortDirection.Descending, SortField.ProductName));
        // Eğer listeye nasıl bir sıralama yapacağını öğretmediysek, çalışma zamanında 
        // System.InvalidOperationException: 'Failed to compare two elements in the array.'
        // şeklinde bir Exception alırız.
        // Bunun sebebi, Sort metodunun çalıştırıldığı listedeki Stock nesnelerini neye göre karşılaştıracağını bilmemesidir.
        foreach (var stock in deppo)
        {
            Console.WriteLine($"{stock.ProductName}: {stock.StockQuantity}");
        }

        //var amount1 = new Amount { Code = "TRY", Value = 100 };
        //var amount2 = new Amount { Code = "TRY", Value = 100 };
        //if (amount1.Equals(amount2))
        //    Console.WriteLine("Amounts are equal.");
        //else
        //    Console.WriteLine("Amounts are not equal.");
    }
}
/*
    Yeni senaryo. Ürünlerin stok bilgilerinin tutulduğu bir koleksiyonunuz var.
    Bu koleksiyon üzerinden sıralamalar yapabilmek istiyoruz.
*/
public class Stock
    : IComparable<Stock> // Bu arayüzü implmente ederek, Stock nesnelerinin birbirleriyle nasıl karşılaştırılacağını (Compare) programlayabiliriz.
{
    public string ProductName { get; set; }
    public int StockQuantity { get; set; }

    public int CompareTo(Stock? other)
    {
        if (other == null) return 1;

        /*
            Primitive tiplerin (double, int, string vb.) kendilerine özgü bir CompareTo metodu vardır.
            Dolayısıyla bu örnekte parametre olarak gelen Stock verisinin StockQuantity değerini,
            var olan Stock(this) nesnesinin StockQuantity değeriyle karşılaştırarak sıralama yaptırıyoruz.

            Burada sıralamayı taraflar belirler. Gelen argümanı, var olan nesne ile karşılaştırırsak azalan şekilde (Büyükten küçüğe) sıralar. (Descending)
            Var olan nesneyi, gelen argümanla karşılaştırırsak artan şekilde (Küçükten büyüğe) sıralar. (Ascending)
        */
        // return other.StockQuantity.CompareTo(this.StockQuantity);
        return this.StockQuantity.CompareTo(other.StockQuantity);
    }
}

/*
 Daha gelişmiş bir karşılaştırma bileşeni de yazabiliriz.
 IComparer arayüzünü kullanabiliriz.

 StockComparer sınıfı, Stock nesnelerini karşılaştırmak için kullanılacak bir bileşendir.
 Kendi içinde hangi alana göre ve hangi yönde karşılaştırma yapılacağı bilgilerini de taşır.
 IComparer interface türü ile gelen ve yazılmak zorunda olan Compare metodu, bu bilgileri kullanarak karşılaştırma işlemini gerçekleştirir.
*/

public class StockComparer
    : IComparer<Stock>
{
    public SortDirection SortDirection { get; private set; } = SortDirection.Ascending;
    public SortField SortField { get; private set; } = SortField.ProductName;

    public StockComparer(SortDirection sortDirection, SortField sortField)
    {
        SortDirection = sortDirection;
        SortField = sortField;
    }
    public int Compare(Stock? x, Stock? y)
    {
        // TODO@buraksenyurt: Compare metoduna farklı sıralama stratejilerini dışarıdan nasıl alabiliriz?
        // Yani bu kadar çok switch-case yapısı yazmak yerine, farklı sıralama stratejilerini dışarıdan alabilir miyiz?
        switch (SortDirection)
        {
            case SortDirection.Ascending:
                switch (SortField)
                {
                    case SortField.ProductName:
                        return x.ProductName.CompareTo(y?.ProductName);
                    case SortField.StockQuantity:
                        return x.StockQuantity.CompareTo(y?.StockQuantity);
                }
                break;
            case SortDirection.Descending:
                switch (SortField)
                {
                    case SortField.ProductName:
                        return y.ProductName.CompareTo(x?.ProductName);
                    case SortField.StockQuantity:
                        return y.StockQuantity.CompareTo(x?.StockQuantity);
                }
                break;
            default:
                switch (SortField)
                {
                    case SortField.ProductName:
                        return x.ProductName.CompareTo(y?.ProductName);
                    case SortField.StockQuantity:
                        return x.StockQuantity.CompareTo(y?.StockQuantity);
                }
                break;
        }
        return 0;
    }
}
public enum SortDirection
{
    Ascending,
    Descending
}
public enum SortField
{
    ProductName,
    StockQuantity
}

/*
 Senaryomuza göre iki Amount nesne örneğinin Code ve Value değerleri aynı ise eşit kabul etmek istiyoruz.
 Bunun Equals metodu üzerinden yapabiliriz. Bunun için IEquatable arayüzünü implemente ettik.
 Ayrıca öneri üzerine Equals metodunu override etmemiz de yerinde olacaktır.
 Override; Üst sınıfta tanımlanan bir davranışın alt sınıfta(yani türeyen sınıfta) yeniden yazılması olarak ifade edilebilir.
*/
public class Amount
    : IEquatable<Amount> // Amount sınıfının generic IEquatable arayüzünü implemente edeceğiniz belirtiyoruz.
{
    public string Code { get; set; }
    public decimal Value { get; set; }

    public bool Equals(Amount? other) // IEquatable arayüzünde tanımlanan Equals metodu
    {
        if (other == null) return false;

        if (this.Code == other.Code && this.Value == other.Value)
            return true;

        return false;
    }
    public override bool Equals(object? obj)
    {
        if (obj is Amount amount)
        {
            return Equals(amount);
        }
        return false;
    }
}
