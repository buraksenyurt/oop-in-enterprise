using Storage.Domain;
using Storage.Infra.Contracts;

namespace Storage.Infra.Validators;

public class SizeValidator
    : IAssetValidator
{
    public bool Validate(Asset asset)
    {
        Console.WriteLine($"Validating asset size for key: {asset.Key}");
        return true;
    }
}
