public class Siparis
{
    private int _siparisNo;
    private List<(string Ad, decimal Fiyat)> _kalemler;
    private decimal _indirim;
    private decimal _toplam;
    private string _tarih;

    public Siparis(int siparisNo, List<Product> urunler, decimal indirim, decimal toplam)
    {
        _siparisNo = siparisNo;
        // Ürünün kendisini değil, o anki adını ve fiyatını saklıyoruz.
        // Yoksa ürün sonradan güncellenince eski siparişler de değişirdi.
        _kalemler = new List<(string Ad, decimal Fiyat)>();
        foreach (Product u in urunler)
        {
            _kalemler.Add((u.Ad, u.Fiyat));
        }
        _indirim = indirim;
        _toplam = toplam;
        _tarih = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    }

    public void SiparisiGoster()
    {
        Console.WriteLine("\n=== SİPARİŞ #" + _siparisNo + " ===");
        Console.WriteLine("Tarih: " + _tarih);
        foreach ((string Ad, decimal Fiyat) kalem in _kalemler)
        {
            Console.WriteLine("- " + kalem.Ad + " : " + Para.Yaz(kalem.Fiyat));
        }
        if (_indirim > 0)
            Console.WriteLine("İndirim: -" + Para.Yaz(_indirim));
        Console.WriteLine("Toplam: " + Para.Yaz(_toplam));
    }
}
