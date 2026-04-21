public class DijitalUrun : Product
{
    private string _indirmeLinki;

    public DijitalUrun(string ad, double fiyat, int stok, string indirmeLinki)
        : base(ad, fiyat, stok)
    {
        _indirmeLinki = indirmeLinki;
    }

    public string IndirmeLinki
    {
        get { return _indirmeLinki; }
        set { _indirmeLinki = value; }
    }
    public override string BilgileriGoster()
{
    return "Dijital Ürün: " + Ad + " | Fiyat: " + Fiyat + "₺ | İndirme: " + IndirmeLinki;
}
}