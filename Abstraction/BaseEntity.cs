public abstract class BaseEntity
{
    public Guid Id { get; set; }  // Entity ID'si (GUID)
    public DateTime CreatedAt { get; set; }  // Oluşturulma tarihi
    public DateTime UpdatedAt { get; set; }  // Güncellenme tarihi
    public DateTime? DeletedAt { get; set; }  // Silinme tarihi (Soft delete)
    public bool IsActive { get; set; }  // Entity aktif mi?
    public string CreatedBy { get; set; }  // Kaydı oluşturan kullanıcı
    public string UpdatedBy { get; set; }  // Kaydı güncelleyen kullanıcı
    public string? DeletedBy { get; set; }

    // Varsayılan constructor
    protected BaseEntity()
    {
        Id = Guid.NewGuid();  // Yeni bir GUID ataması
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        IsActive = true;
    }
}
