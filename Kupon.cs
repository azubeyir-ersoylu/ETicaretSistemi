public class Kupon
{
    private string _kod;
    private double _indirimYuzdesi;
    private bool _kullanildi;

    public Kupon(string kod, double indirimYuzdesi)
    {
        _kod = kod;
        _indirimYuzdesi = indirimYuzdesi;
        _kullanildi = false;
    }

    public string Kod
    {
        get { return _kod; }
    }

    public double IndirimYuzdesi
    {
        get { return _indirimYuzdesi; }
    }

    public bool Kullanildi
    {
        get { return _kullanildi; }
    }

    public double IndirimUygula(double tutar)
    {
        if (_kullanildi)
            throw new Exception("Bu kupon daha önce kullanıldı!");

        _kullanildi = true;
        double indirim = tutar * _indirimYuzdesi / 100;
        return tutar - indirim;
    }
}