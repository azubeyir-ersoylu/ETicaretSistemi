public static class Para
{
    // Tutarı "15.000,00₺" biçiminde yazar (bilgisayarın dil ayarına göre).
    public static string Yaz(decimal tutar)
    {
        return tutar.ToString("N2") + "₺";
    }
}
