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

    public decimal IndirimUygula(decimal tutar)
    {
        if (_kullanildi)
            throw new Exception("Bu kupon daha önce kullanıldı!");

        _kullanildi = true;
        decimal indirim = tutar * _indirimYuzdesi / 100;
        return tutar - indirim;
    }
}
