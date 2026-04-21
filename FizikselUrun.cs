public class FizikselUrun : Product
{
    private double _kargoAgirligi;

    public FizikselUrun(string ad, double fiyat, int stok, double kargoAgirligi)
        : base(ad, fiyat, stok)
    {
        _kargoAgirligi = kargoAgirligi;
    }

    public double KargoAgirligi
    {
        get { return _kargoAgirligi; }
        set { _kargoAgirligi = value; }
    }
    public override string BilgileriGoster()
{
    return "Fiziksel Ürün: " + Ad + " | Fiyat: " + Fiyat + "₺ | Kargo Ağırlığı: " + KargoAgirligi + "kg";
}
}