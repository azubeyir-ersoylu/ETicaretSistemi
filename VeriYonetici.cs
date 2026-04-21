using Newtonsoft.Json;

public class VeriYonetici
{
    private string _dosyaYolu = "urunler.json";

    public void UrunleriKaydet(List<Product> urunler)
    {
        string json = JsonConvert.SerializeObject(urunler, Formatting.Indented);
        File.WriteAllText(_dosyaYolu, json);
        Console.WriteLine("Ürünler kaydedildi!");
    }

    public List<Product> UrunleriYukle()
    {
        if (!File.Exists(_dosyaYolu))
        {
            Console.WriteLine("Kayıt dosyası bulunamadı, yeni liste oluşturuluyor.");
            return new List<Product>();
        }

        string json = File.ReadAllText(_dosyaYolu);
        List<Product> urunler = JsonConvert.DeserializeObject<List<Product>>(json);
        return urunler;
    }
}