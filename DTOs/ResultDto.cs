public class ResultDto
{
    public bool Success { get; set; }  // İşlem başarılı mı?
    public string Message { get; set; }  // Kullanıcıya ya da API'yi kullanan geliştiriciye mesaj
    public string Code { get; set; }  // İlgili işlem kodu (örneğin: 200, 400, 404, 500)
    public string ErrorDetails { get; set; }  // Hata mesajı, işlem sırasında oluşan hata detayları
    public object Data { get; set; }  // İşlem sonucunda döndürülen veri
    public List<string> ValidationErrors { get; set; }  // Geçerli validasyon hataları (varsa)
}
