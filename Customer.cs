public class Customer : User
{
    private string _adres;

    public Customer(int id, string ad, string email, string sifre, string adres)
        : base(id, ad, email, sifre)
    {
        _adres = adres;
    }

    public string Adres
    {
        get { return _adres; }
        set { _adres = value; }
    }
}