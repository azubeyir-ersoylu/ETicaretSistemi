public class User
{
    private int _id;
    private string _ad;
    private string _email;
    private string _sifre;

    public User(int id, string ad, string email, string sifre)
    {
        _id = id;
        _ad = ad;
        _email = email;
        _sifre = sifre;
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Ad
    {
        get { return _ad; }
        set { _ad = value; }
    }

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public string Sifre
    {
        get { return _sifre; }
        set { _sifre = value; }
    }
}