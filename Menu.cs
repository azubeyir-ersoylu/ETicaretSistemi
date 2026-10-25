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
        _urunler.Add(new FizikselUrun("Laptop", 15000, 5, 2.5m));
        _urunler.Add(new FizikselUrun("Mouse", 250, 20, 0.3m));
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

            string secim;
            try
            {
                secim = Girdi.Metin("Seçiminiz: ");
            }
            catch (EndOfStreamException)
            {
                // Girdi kapandıysa (örneğin Ctrl+Z) kaydedip düzgünce çık.
                _veriYonetici.UrunleriKaydet(_urunler);
                break;
            }

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
            // BilgileriGoster virtual olduğu için her ürün kendi türüne göre
            // (fiziksel/dijital) bilgisini yazar: polimorfizm.
            Console.WriteLine((i + 1) + ". " + _urunler[i].BilgileriGoster());
        }
    }

    // Listeden bir ürün seçtirir. Liste boşsa ya da kullanıcı 0 ile
    // vazgeçerse null döner.
    private Product? UrunSec(string soru)
    {
        UrunleriListele();
        if (_urunler.Count == 0)
        {
            Console.WriteLine("Henüz hiç ürün yok!");
            return null;
        }

        int numara = Girdi.TamSayi(soru + " (0 = vazgeç): ", 0, _urunler.Count);
        if (numara == 0)
            return null;
        return _urunler[numara - 1];
    }

    private void SepeteEkle()
    {
        Product? urun = UrunSec("Eklemek istediğiniz ürün numarası");
        if (urun == null)
            return;

        try
        {
            _sepet.UrunEkle(urun);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
        }
    }

    private void UrunEkle()
    {
        string ad = Girdi.Metin("Ürün adı: ");
        decimal fiyat = Girdi.Sayi("Fiyat: ");
        int stok = Girdi.TamSayi("Stok: ", 0);

        // Eskiden "f" dışındaki her cevap (örneğin "fiziksel") dijital
        // sayılıyordu. Artık geçerli bir cevap gelene kadar tekrar soruyoruz.
        while (true)
        {
            string tip = Girdi.Metin("Fiziksel mi Dijital mi? (f/d): ").ToLower();

            if (tip == "f" || tip == "fiziksel")
            {
                decimal agirlik = Girdi.Sayi("Kargo ağırlığı (kg): ");
                _urunler.Add(new FizikselUrun(ad, fiyat, stok, agirlik));
                break;
            }
            if (tip == "d" || tip == "dijital")
            {
                string link = Girdi.Metin("İndirme linki: ");
                _urunler.Add(new DijitalUrun(ad, fiyat, stok, link));
                break;
            }

            Console.WriteLine("Lütfen 'f' (fiziksel) veya 'd' (dijital) yazın.");
        }

        Console.WriteLine("Ürün eklendi!");
    }

    private void UrunSil()
    {
        Product? urun = UrunSec("Silmek istediğiniz ürün numarası");
        if (urun == null)
            return;

        _urunler.Remove(urun);
        // Silinen ürün sepette kalırsa hâlâ sipariş verilebiliyordu.
        _sepet.UrunuTamamenCikar(urun);
        Console.WriteLine(urun.Ad + " silindi!");
    }

    private void UrunGuncelle()
    {
        Product? urun = UrunSec("Güncellemek istediğiniz ürün numarası");
        if (urun == null)
            return;

        string yeniAd = Girdi.Metin("Yeni ad (boş bırakırsan değişmez): ", bosOlabilir: true);
        if (yeniAd != "")
            urun.Ad = yeniAd;

        decimal? yeniFiyat = Girdi.SayiVeyaBos("Yeni fiyat (boş bırakırsan değişmez): ");
        if (yeniFiyat != null)
            urun.Fiyat = yeniFiyat.Value;

        Console.WriteLine("Ürün güncellendi!");
    }

    private void UrunAra()
    {
        string kelime = Girdi.Metin("Aranacak kelime: ");

        Console.WriteLine("\n=== ARAMA SONUÇLARI ===");
        bool bulundu = false;

        foreach (Product urun in _urunler)
        {
            if (urun.Ad.Contains(kelime, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("- " + urun.Ad + " : " + Para.Yaz(urun.Fiyat) + " (Stok: " + urun.Stok + ")");
                bulundu = true;
            }
        }

        if (!bulundu)
            Console.WriteLine("Sonuç bulunamadı!");
    }

    private void KuponKullan()
    {
        // ToUpper() Türkçe ayarlı bilgisayarda "i" harfini "İ" yapar ve
        // "indirim10" kodu "İNDİRİM10" olur. ToUpperInvariant() dilden bağımsızdır.
        string kod = Girdi.Metin("Kupon kodunuz: ").ToUpperInvariant();

        Kupon? bulunanKupon = null;
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

        // Eskiden indirim sadece ekrana yazılıyor, siparişe yansımıyordu;
        // üstelik kupon boşa harcanıyordu. Artık kupon sepete bağlanıyor.
        try
        {
            _sepet.KuponUygula(bulunanKupon);
            Console.WriteLine("%" + bulunanKupon.IndirimYuzdesi + " indirim uygulandı!");
            Console.WriteLine("Yeni toplam: " + Para.Yaz(_sepet.ToplamFiyat()));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
        }
    }

    private void SiparisVer()
    {
        // Fiyata değil ürün sayısına bakıyoruz: 0₺'lik bir ürün veya
        // %100 kupon olsa bile sepet boş sayılmamalı.
        if (_sepet.Urunler().Count == 0)
        {
            Console.WriteLine("Sepetiniz boş!");
            return;
        }

        // Sepete eklendikten sonra stok değişmiş olabilir (ör. admin
        // güncelledi), o yüzden önce hepsini kontrol edip sonra düşüyoruz.
        foreach (Product urun in _sepet.Urunler().Distinct())
        {
            if (_sepet.Adet(urun) > urun.Stok)
            {
                Console.WriteLine(urun.Ad + " için yeterli stok kalmadı! (Stok: " + urun.Stok + ")");
                return;
            }
        }
        foreach (Product urun in _sepet.Urunler())
        {
            urun.Stok--;
        }
        _sepet.Kupon?.Kullan();

        Siparis siparis = new Siparis(_siparisNo++, _sepet.Urunler(), _sepet.Indirim(), _sepet.ToplamFiyat());
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
