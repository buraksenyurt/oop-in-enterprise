using Storage.Domain;
using Storage.Infra.Contracts;

namespace Storage.Infra.Services;

public class TypeValidator
    : IAssetValidator
{
    enum DocumentType
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
        throw new NotImplementedException();
    }
}
