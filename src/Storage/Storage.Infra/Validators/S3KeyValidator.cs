using Storage.Domain;
using Storage.Infra.Contracts;

namespace Storage.Infra.Validators;

/*
 Özellikle Cloud storage sistemlerinde,
 asset adının geçerli formatta olup olmadığını kontrol etmek önemli olabilir.

  Örneğin uzunluk 3 - 63 karakter arası,
  case-sensitive, utf-8 destekli
  
*/
public class S3KeyValidator
    : IAssetValidator
{
    public S3KeyValidator()
    {
    }

    public bool Validate(Asset asset)
    {
        throw new NotImplementedException();
    }
}
