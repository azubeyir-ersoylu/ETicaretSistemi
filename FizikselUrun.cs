public class FizikselUrun : Product
{
    private double _kargoAgirligi;

    public FizikselUrun(string ad, double fiyat, int stok, double kargoAgirligi)
        : base(ad, fiyat, stok)
    {
        KargoAgirligi = kargoAgirligi;
    }

    public double KargoAgirligi
    {
        get { return _kargoAgirligi; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Kargo ağırlığı negatif olamaz!");
            _kargoAgirligi = value;
        }
    }

    public override string BilgileriGoster()
    {
        return "Fiziksel Ürün: " + Ad + " | Fiyat: " + Fiyat + "₺ | Kargo Ağırlığı: " + KargoAgirligi + "kg";
    }
}
