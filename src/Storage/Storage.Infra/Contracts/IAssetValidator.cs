using Storage.Domain;

namespace Storage.Infra.Contracts;

public interface IAssetValidator
{
    bool Validate(Asset asset);
}
