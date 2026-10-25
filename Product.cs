public class Product
{
    private string _ad = "";
    private decimal _fiyat;
    private int _stok;

    public Product(string ad, decimal fiyat, int stok)
    {
        // Alanlara (_fiyat) doğrudan değil, özellikler (Fiyat) üzerinden
        // atıyoruz ki set içindeki kontroller kurucuda da çalışsın.
        Ad = ad;
        Fiyat = fiyat;
        Stok = stok;
    }

    public string Ad
    {
        get { return _ad; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ürün adı boş olamaz!");
            _ad = value;
        }
    }

    public decimal Fiyat
    {
        get { return _fiyat; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Fiyat negatif olamaz!");
            _fiyat = value;
        }
    }

    public int Stok
    {
        get { return _stok; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Stok negatif olamaz!");
            _stok = value;
        }
    }

    public virtual string BilgileriGoster()
    {
        return "Ürün: " + Ad + " | Fiyat: " + Para.Yaz(Fiyat) + " | Stok: " + Stok;
    }
}
