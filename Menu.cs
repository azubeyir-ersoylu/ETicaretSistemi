public class Menu
{
    private List<Product> _urunler;
    private Cart _sepet;
    private VeriYonetici _veriYonetici;
    private List<Kupon> _kuponlar;
    private List<Siparis> _siparisler;
private int _siparisNo;
    public Menu()
    {
        _urunler = new List<Product>();
        _sepet = new Cart();
        _veriYonetici = new VeriYonetici();
        _urunler = _veriYonetici.UrunleriYukle();
        _kuponlar = new List<Kupon>();
        _kuponlar.Add(new Kupon("INDIRIM10", 10));
        _kuponlar.Add(new Kupon("INDIRIM20", 20));
        _kuponlar.Add(new Kupon("HOSGELDIN", 15));
        _siparisler = new List<Siparis>();
_siparisNo = 1;
        if (_urunler.Count == 0)
            UrunleriDoldur();
    }

    private void UrunleriDoldur()
    {
        _urunler.Add(new FizikselUrun("Laptop", 15000, 5, 2.5));
        _urunler.Add(new FizikselUrun("Mouse", 250, 20, 0.3));
        _urunler.Add(new DijitalUrun("Minecraft", 200, 999, "minecraft.com/indir"));
        _urunler.Add(new DijitalUrun("Photoshop", 1200, 999, "adobe.com/indir"));
    }

    public void Baslat()
    {
        while (true)
        {
            Console.WriteLine("\n=== E-TİCARET SİSTEMİ ===");
            Console.WriteLine("1. Ürünleri Listele");
            Console.WriteLine("2. Sepete Ekle");
            Console.WriteLine("3. Sepeti Göster");
            Console.WriteLine("4. Ürün Ekle (Admin)");
            Console.WriteLine("5. Ürün Sil (Admin)");
            Console.WriteLine("6. Ürün Güncelle (Admin)");
            Console.WriteLine("7. Ürün Ara");
            Console.WriteLine("8. Kupon Kullan");
            Console.WriteLine("9. Sipariş Ver");
Console.WriteLine("10. Sipariş Geçmişi");
Console.WriteLine("11. Çıkış");
           
            Console.Write("Seçiminiz: ");

            string secim = Console.ReadLine();

            if (secim == "1") UrunleriListele();
            else if (secim == "2") SepeteEkle();
            else if (secim == "3") _sepet.SepetiGoster();
            else if (secim == "4") UrunEkle();
            else if (secim == "5") UrunSil();
            else if (secim == "6") UrunGuncelle();
            else if (secim == "7") UrunAra();
            else if (secim == "8") KuponKullan();
            else if (secim == "9") SiparisVer();
else if (secim == "10") SiparisGecmisiniGoster();
else if (secim == "11")
{
    _veriYonetici.UrunleriKaydet(_urunler);
    break;
}
          
            else Console.WriteLine("Geçersiz seçim!");
        }
    }

    private void UrunleriListele()
    {
        Console.WriteLine("\n=== ÜRÜNLER ===");
        for (int i = 0; i < _urunler.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + _urunler[i].Ad + " - " + _urunler[i].Fiyat + "₺");
        }
    }

    private void SepeteEkle()
    {
        UrunleriListele();
        Console.Write("Eklemek istediğiniz ürün numarası: ");
        string input = Console.ReadLine();
        int numara;

        try
        {
            numara = int.Parse(input);
            if (numara >= 1 && numara <= _urunler.Count)
            {
                _sepet.UrunEkle(_urunler[numara - 1]);
            }
            else
            {
                Console.WriteLine("Geçersiz numara!");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Hata: Lütfen bir sayı girin!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
        }
    }

    private void UrunEkle()
    {
        Console.Write("Ürün adı: ");
        string ad = Console.ReadLine();

        Console.Write("Fiyat: ");
        double fiyat = double.Parse(Console.ReadLine());

        Console.Write("Stok: ");
        int stok = int.Parse(Console.ReadLine());

        Console.Write("Fiziksel mi Dijital mi? (f/d): ");
        string tip = Console.ReadLine();

        if (tip == "f")
        {
            Console.Write("Kargo ağırlığı (kg): ");
            double agirlik = double.Parse(Console.ReadLine());
            _urunler.Add(new FizikselUrun(ad, fiyat, stok, agirlik));
        }
        else
        {
            Console.Write("İndirme linki: ");
            string link = Console.ReadLine();
            _urunler.Add(new DijitalUrun(ad, fiyat, stok, link));
        }

        Console.WriteLine("Ürün eklendi!");
    }

    private void UrunSil()
    {
        UrunleriListele();
        Console.Write("Silmek istediğiniz ürün numarası: ");

        try
        {
            int numara = int.Parse(Console.ReadLine());
            if (numara >= 1 && numara <= _urunler.Count)
            {
                Console.WriteLine(_urunler[numara - 1].Ad + " silindi!");
                _urunler.RemoveAt(numara - 1);
            }
            else
            {
                Console.WriteLine("Geçersiz numara!");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Hata: Lütfen bir sayı girin!");
        }
    }

    private void UrunGuncelle()
    {
        UrunleriListele();
        Console.Write("Güncellemek istediğiniz ürün numarası: ");

        try
        {
            int numara = int.Parse(Console.ReadLine());
            if (numara >= 1 && numara <= _urunler.Count)
            {
                Console.Write("Yeni ad (boş bırakırsan değişmez): ");
                string yeniAd = Console.ReadLine();
                if (yeniAd != "")
                    _urunler[numara - 1].Ad = yeniAd;

                Console.Write("Yeni fiyat (0 bırakırsan değişmez): ");
                double yeniFiyat = double.Parse(Console.ReadLine());
                if (yeniFiyat > 0)
                    _urunler[numara - 1].Fiyat = yeniFiyat;

                Console.WriteLine("Ürün güncellendi!");
            }
            else
            {
                Console.WriteLine("Geçersiz numara!");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Hata: Geçersiz giriş!");
        }
    }

    private void UrunAra()
    {
        Console.Write("Aranacak kelime: ");
        string kelime = Console.ReadLine().ToLower();

        Console.WriteLine("\n=== ARAMA SONUÇLARI ===");
        bool bulundu = false;

        foreach (Product urun in _urunler)
        {
            if (urun.Ad.ToLower().Contains(kelime))
            {
                Console.WriteLine("- " + urun.Ad + " : " + urun.Fiyat + "₺ (Stok: " + urun.Stok + ")");
                bulundu = true;
            }
        }

        if (!bulundu)
            Console.WriteLine("Sonuç bulunamadı!");
    }

    private void KuponKullan()
    {
        Console.Write("Kupon kodunuz: ");
        string kod = Console.ReadLine().ToUpper();

        Kupon bulunanKupon = null;
        foreach (Kupon k in _kuponlar)
        {
            if (k.Kod == kod)
            {
                bulunanKupon = k;
                break;
            }
        }

        if (bulunanKupon == null)
        {
            Console.WriteLine("Geçersiz kupon kodu!");
            return;
        }

        try
        {
            double yeniToplam = bulunanKupon.IndirimUygula(_sepet.ToplamFiyat());
            Console.WriteLine("%" + bulunanKupon.IndirimYuzdesi + " indirim uygulandı!");
            Console.WriteLine("Yeni toplam: " + yeniToplam + "₺");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
        }
    }
    private void SiparisVer()
{
    if (_sepet.ToplamFiyat() == 0)
    {
        Console.WriteLine("Sepetiniz boş!");
        return;
    }

    Siparis siparis = new Siparis(_siparisNo++, _sepet.Urunler(), _sepet.ToplamFiyat());
    _siparisler.Add(siparis);
    siparis.SiparisiGoster();
    Console.WriteLine("Siparişiniz alındı!");
    _sepet.SepetiTemizle();
}

private void SiparisGecmisiniGoster()
{
    if (_siparisler.Count == 0)
    {
        Console.WriteLine("Henüz sipariş vermediniz!");
        return;
    }

    Console.WriteLine("\n=== SİPARİŞ GEÇMİŞİ ===");
    foreach (Siparis s in _siparisler)
    {
        s.SiparisiGoster();
    }
}
}