namespace HumanResources.Domain;

public class Candidate
{
    // GUID: Global Unique Identifier
    public Guid ID { get; set; }
    public required string SSN { get; set; }
    public DateTime BirthDate { get; set; }
    public required string Fullname { get; set; }
    public bool IsActive { get; set; }
    // Sadece Graduate türünden nesneler barındıran bir liste türü kullandık.
    // List<T> generic liste anlamındadır. T (Herhangi bir) türünden bir koleksiyondur.
    // [] operatörü ile Gradutes isimli liste koleksiyonunu varsayılan olarak boş halde(0 elemanlı) oluşturuyoruz.
    public List<Graduate> Graduates { get; set; } = [];
    public ContactInformation ContactInformation { get; set; }

    // İletişim Bilgileri (Contancts)
    // İş tecrübesi (Experience)
    // Hobiler (Hobies)
    // Referanslar (References)
}
