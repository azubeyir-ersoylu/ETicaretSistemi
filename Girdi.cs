using System.Globalization;

// Kullanıcıdan güvenli şekilde veri okuyan yardımcı metotlar.
// Hepsi aynı fikre dayanır: geçerli bir değer gelene kadar tekrar sor.
public static class Girdi
{
    public static string Metin(string soru, bool bosOlabilir = false)
    {
        while (true)
        {
            Console.Write(soru);
            string? satir = Console.ReadLine();
            if (satir == null)
                throw new EndOfStreamException("Girdi sona erdi.");

            string metin = satir.Trim();
            if (metin != "" || bosOlabilir)
                return metin;

            Console.WriteLine("Bu alan boş bırakılamaz!");
        }
    }

    public static decimal Sayi(string soru, decimal enAz = 0)
    {
        while (true)
        {
            decimal? sayi = SayiyaCevir(Metin(soru));
            if (sayi != null && sayi >= enAz)
                return sayi.Value;

            Console.WriteLine("Geçersiz değer! " + enAz + " veya daha büyük bir sayı girin.");
        }
    }

    // Boş bırakılırsa null döner ("değiştirme" anlamında kullanılır).
    public static decimal? SayiVeyaBos(string soru, decimal enAz = 0)
    {
        while (true)
        {
            string metin = Metin(soru, bosOlabilir: true);
            if (metin == "")
                return null;

            decimal? sayi = SayiyaCevir(metin);
            if (sayi != null && sayi >= enAz)
                return sayi;

            Console.WriteLine("Geçersiz değer! " + enAz + " veya daha büyük bir sayı girin.");
        }
    }

    public static int TamSayi(string soru, int enAz, int enCok = int.MaxValue)
    {
        while (true)
        {
            if (int.TryParse(Metin(soru), out int sayi) && sayi >= enAz && sayi <= enCok)
                return sayi;

            if (enCok == int.MaxValue)
                Console.WriteLine("Geçersiz değer! " + enAz + " veya daha büyük bir tam sayı girin.");
            else
                Console.WriteLine("Geçersiz değer! " + enAz + " ile " + enCok + " arasında bir tam sayı girin.");
        }
    }

    // Hem "12,5" hem "12.5" kabul edilsin diye virgülü noktaya çevirip
    // bilgisayarın dil ayarından bağımsız (InvariantCulture) okuyoruz.
    private static decimal? SayiyaCevir(string metin)
    {
        if (decimal.TryParse(metin.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal sayi))
            return sayi;
        return null;
    }
}
