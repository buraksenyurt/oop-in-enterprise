using Storage.Domain;
using Storage.Infra.Validators;

namespace Storage.Infra.Contracts;

/*
 IStorageService interface türetmesi yanında storage bileşenlerinin ortaklaşa kullanacağı
 validasyon mekanizmasını da içeren abstract class.
 Her storage bileşeninin sistemde geçerli bir ismi olmasınıda istediğimizden onu da bir özellik olarak buraya dahil edebiliriz.

 BaseStorage, abstract bir sınıf olmak zorunda mıdır?

*/
public abstract class BaseStorage(string name)
{
    // protected erişim belirleyicisi üyeler(members) sadece türeyen türler tarafında kullanılabilir
    protected readonly List<IAssetValidator> _validators = [
        new SizeValidator(),
        new TypeValidator()
    ];
    public string Name { get; } = name;

    // virtual tanımlanan üyeler istenirse türeyen sınıflar içerisinde ezilebilir(override) ve davranışı,
    // işleyişi değiştirebililir. Ezilmezlerse varsayılan davranışlarıyla çalışmaya devam ederler.
    // bknz. Object sınıfının ToString() metodu.
    // name hiding. Türetilen sınıftaki virtual olmayan bir üyenin alt sınıfta tekrardan tanımlanması.
    public virtual void ApplyValidators(Asset asset)
    {
        foreach (var validator in _validators)
        {
            if (!validator.Validate(asset))
            {
                throw new InvalidOperationException($"Validation failed for {asset.Key} with {validator.GetType().Name}");
            }
        }
    }
}
