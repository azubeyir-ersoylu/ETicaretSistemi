public class Kupon
{
    private string _kod;
    private decimal _indirimYuzdesi;
    private bool _kullanildi;

    public Kupon(string kod, decimal indirimYuzdesi)
    {
        _kod = kod;
        _indirimYuzdesi = indirimYuzdesi;
        _kullanildi = false;
    }

    public string Kod
    {
        get { return _kod; }
    }

    public decimal IndirimYuzdesi
    {
        get { return _indirimYuzdesi; }
    }

    public bool Kullanildi
    {
        get { return _kullanildi; }
    }

    // Sadece hesaplar, kuponu harcamaz. Kupon sipariş verilince harcanır.
    public decimal IndirimHesapla(decimal tutar)
    {
        return Math.Round(tutar * _indirimYuzdesi / 100, 2);
    }

    public void Kullan()
    {
        if (_kullanildi)
            throw new InvalidOperationException("Bu kupon daha önce kullanıldı!");
        _kullanildi = true;
    }
}
