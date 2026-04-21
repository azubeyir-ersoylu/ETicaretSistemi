# E-Ticaret Sistemi

C# ile geliştirilmiş nesne yönelimli e-ticaret yönetim sistemi.

## Özellikler

- Ürün listeleme, ekleme, silme ve güncelleme (CRUD)
- Fiziksel ve dijital ürün desteği
- Sepet yönetimi ve sipariş sistemi
- İndirim kuponu sistemi
- Ürün arama
- Sipariş geçmişi
- JSON ile veri saklama
- Stok takibi

## Kullanılan OOP Prensipleri

- **Encapsulation**: Tüm field'lar private, property ile erişim
- **Inheritance**: Product → FizikselUrun / DijitalUrun, User → Customer / Admin
- **Polymorphism**: BilgileriGoster() metodu her sınıfta farklı davranış
- **Exception Handling**: Stok kontrolü, geçersiz giriş yönetimi

## Sınıf Yapısı
