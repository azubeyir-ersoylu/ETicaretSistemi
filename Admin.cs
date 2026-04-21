public class Admin : User
{
    private string _yetki;

    public Admin(int id, string ad, string email, string sifre, string yetki)
        : base(id, ad, email, sifre)
    {
        _yetki = yetki;
    }

    public string Yetki
    {
        get { return _yetki; }
        set { _yetki = value; }
    }
}