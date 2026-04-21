public class Product
{
    private string _ad;
    private double _fiyat;
    private int _stok;
    private int _id;

    public Product(string ad, double fiyat, int stok)
    {
        _ad = ad;
        _fiyat = fiyat;
        _stok = stok;
    }

    public string Ad
    {
        get { return _ad; }
        set { _ad = value; }
    }

    public double Fiyat
    {
        get { return _fiyat; }
        set
        {
            if (value < 0)
                throw new Exception("Fiyat negatif olamaz!");
            _fiyat = value;
        }
    }

    public int Stok
    {
        get { return _stok; }
        set { _stok = value; }
    }
    public virtual string BilgileriGoster()
{
    return "Ürün: " + Ad + " | Fiyat: " + Fiyat + "₺ | Stok: " + Stok;
}
}