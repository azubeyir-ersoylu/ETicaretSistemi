using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class VeriYonetici
{
    private string _dosyaYolu = "urunler.json";

    // Listenin türü List<Product> olduğu için JSON'a ürünün gerçek türü
    // (FizikselUrun / DijitalUrun) yazılmıyordu; geri yüklenince hepsi düz
    // Product oluyor, kargo ağırlığı ve indirme linki kayboluyordu.
    // TypeNameHandling.Auto, gerçek türü "$type" alanı olarak kaydeder.
    private JsonSerializerSettings _ayarlar = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Auto,
        SerializationBinder = new UrunTuruBinder(),
        Formatting = Formatting.Indented
    };

    public void UrunleriKaydet(List<Product> urunler)
    {
        string json = JsonConvert.SerializeObject(urunler, _ayarlar);
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
        List<Product>? urunler = JsonConvert.DeserializeObject<List<Product>>(json, _ayarlar);
        return urunler ?? new List<Product>();
    }

    // "$type" alanına güvenmek tehlikeli olabilir: dosyayı değiştiren biri
    // programa istediği sınıfı oluşturtabilir. Bu yüzden sadece bizim
    // ürün sınıflarımıza izin veriyoruz.
    private class UrunTuruBinder : ISerializationBinder
    {
        private static readonly Type[] _izinliTurler =
        {
            typeof(Product), typeof(FizikselUrun), typeof(DijitalUrun)
        };

        public Type BindToType(string? assemblyName, string typeName)
        {
            foreach (Type tur in _izinliTurler)
            {
                if (tur.Name == typeName)
                    return tur;
            }
            throw new JsonSerializationException("İzin verilmeyen tür: " + typeName);
        }

        public void BindToName(Type serializedType, out string? assemblyName, out string? typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}
