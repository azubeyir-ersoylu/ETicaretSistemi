public class Cart
{
    private List<Product> _urunler;
    private Kupon? _kupon;

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

    public Kupon? Kupon
    {
        get { return _kupon; }
    }

    public void KuponUygula(Kupon kupon)
    {
        if (_urunler.Count == 0)
            throw new InvalidOperationException("Kupon kullanmak için önce sepete ürün ekleyin!");
        if (kupon.Kullanildi)
            throw new InvalidOperationException("Bu kupon daha önce kullanıldı!");
        _kupon = kupon;
    }

    public decimal AraToplam()
    {
        decimal toplam = 0;
        foreach (Product urun in _urunler)
        {
            toplam += urun.Fiyat;
        }
        return toplam;
    }

    public decimal Indirim()
    {
        if (_kupon == null)
            return 0;
        return _kupon.IndirimHesapla(AraToplam());
    }

    public decimal ToplamFiyat()
    {
        return AraToplam() - Indirim();
    }

    public void SepetiGoster()
    {
        Console.WriteLine("=== SEPETİNİZ ===");
        if (_urunler.Count == 0)
        {
            Console.WriteLine("Sepetiniz boş!");
            return;
        }

        foreach (Product urun in _urunler)
        {
            Console.WriteLine("- " + urun.Ad + " : " + urun.Fiyat + "₺");
        }
        if (_kupon != null)
        {
            Console.WriteLine("Ara toplam: " + AraToplam() + "₺");
            Console.WriteLine("Kupon " + _kupon.Kod + " (%" + _kupon.IndirimYuzdesi + "): -" + Indirim() + "₺");
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
        _kupon = null;
        Console.WriteLine("Sepet temizlendi!");
    }
}
