public class Cart
{
    private List<Product> _urunler;

    public Cart()
    {
        _urunler = new List<Product>();
    }

    public void UrunEkle(Product urun)
{
    if (urun.Stok <= 0)
        throw new Exception(urun.Ad + " stokta yok!");
    
    _urunler.Add(urun);
    urun.Stok--;
    Console.WriteLine(urun.Ad + " sepete eklendi! (Kalan stok: " + urun.Stok + ")");
}

    public void UrunCikar(Product urun)
    {
        _urunler.Remove(urun);
        Console.WriteLine(urun.Ad + " sepetten çıkarıldı!");
    }

    public double ToplamFiyat()
    {
        double toplam = 0;
        foreach (Product urun in _urunler)
        {
            toplam += urun.Fiyat;
        }
        return toplam;
    }

    public void SepetiGoster()
    {
        Console.WriteLine("=== SEPETİNİZ ===");
        foreach (Product urun in _urunler)
        {
            Console.WriteLine("- " + urun.Ad + " : " + urun.Fiyat + "₺");
        }
        Console.WriteLine("Toplam: " + ToplamFiyat() + "₺");
    }
    public List<Product> Urunler()
{
    return _urunler;
}

public void SepetiTemizle()
{
    _urunler.Clear();
    Console.WriteLine("Sepet temizlendi!");
}
}