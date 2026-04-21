public class Siparis
{
    private int _siparisNo;
    private List<Product> _urunler;
    private double _toplam;
    private string _tarih;

    public Siparis(int siparisNo, List<Product> urunler, double toplam)
    {
        _siparisNo = siparisNo;
        _urunler = new List<Product>(urunler);
        _toplam = toplam;
        _tarih = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    }

    public void SiparisiGoster()
    {
        Console.WriteLine("\n=== SİPARİŞ #" + _siparisNo + " ===");
        Console.WriteLine("Tarih: " + _tarih);
        foreach (Product u in _urunler)
        {
            Console.WriteLine("- " + u.Ad + " : " + u.Fiyat + "₺");
        }
        Console.WriteLine("Toplam: " + _toplam + "₺");
    }
}