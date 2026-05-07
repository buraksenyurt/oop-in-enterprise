using Storage.Domain;
using Storage.Infra.Contracts;

namespace Storage.Infra.Validators;

public class TypeValidator
    : IAssetValidator
{
    enum AssetType
    {
        Pdf,
        Word,
        Excel,
        Audio,
        Video,
        Markdown
    }
    public bool Validate(Asset asset)
    {
        Console.WriteLine($"Validating asset type for key: {asset.Key}");
        return true;
    }
}
