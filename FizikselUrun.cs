public class FizikselUrun : Product
{
    private decimal _kargoAgirligi;

    public FizikselUrun(string ad, decimal fiyat, int stok, decimal kargoAgirligi)
        : base(ad, fiyat, stok)
    {
        KargoAgirligi = kargoAgirligi;
    }

    public decimal KargoAgirligi
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
        return "Fiziksel Ürün: " + Ad + " | Fiyat: " + Para.Yaz(Fiyat) + " | Stok: " + Stok + " | Kargo: " + KargoAgirligi + " kg";
    }
}
