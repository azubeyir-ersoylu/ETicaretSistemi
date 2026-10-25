public class Cart
{
    private List<Product> _urunler;

    public Cart()
    {
        _urunler = new List<Product>();
    }

    // Stok burada düşülmez, sadece kontrol edilir. Stok sipariş verilince
    // düşer; böylece sepete eklenip satın alınmayan ürünler stoktan kaybolmaz.
    public void UrunEkle(Product urun)
    {
        int sepettekiAdet = Adet(urun);
        if (sepettekiAdet >= urun.Stok)
            throw new InvalidOperationException(urun.Ad + " için yeterli stok yok! (Stok: " + urun.Stok + ")");

        _urunler.Add(urun);
        Console.WriteLine(urun.Ad + " sepete eklendi! (Sepette: " + (sepettekiAdet + 1) + " adet, stok: " + urun.Stok + ")");
    }

    public int Adet(Product urun)
    {
        int adet = 0;
        foreach (Product u in _urunler)
        {
            if (u == urun)
                adet++;
        }
        return adet;
    }

    public void UrunCikar(Product urun)
    {
        _urunler.Remove(urun);
        Console.WriteLine(urun.Ad + " sepetten çıkarıldı!");
    }

    public decimal ToplamFiyat()
    {
        decimal toplam = 0;
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
