# 🧠 Akıllı İçerik Platformu - Katmanlı Mimari ve Yorum Analizi Destekli Blog Uygulaması 🚀

## 🏗️ Katmanlı Mimari Yapısı

Projede sürdürülebilirlik ve okunabilirlik adına çok katmanlı mimari tercih edilmiştir. Her katman farklı bir sorumluluğa sahiptir:

### 🎨 Presentation Layer (Sunum Katmanı)
- Kullanıcı ile doğrudan etkileşimi sağlar.
- ASP.NET MVC ile Controller ve View yapıları oluşturulmuştur.
- Kullanıcı arayüzü bu katmanda yer alır.

### 📁 Entity Layer (Varlık Katmanı)
- Projede kullanılan temel modeller burada tanımlanmıştır:
  - `User`
  - `Post`
  - `Category`
  - `Label`
- Sadece veri yapıları içerir, iş mantığı barındırmaz.

### 🗃️ Data Access Layer (Veri Katmanı)
- Entity Framework Core kullanılarak veritabanı işlemleri gerçekleştirilir.
- CRUD işlemleri ve özel sorgular burada yazılmıştır.

### ⚙️ Business Layer (İş Katmanı)
- Tüm iş kuralları ve servis işlemleri bu katmanda tanımlanmıştır.
- Manager sınıfları üzerinden veri işlenir.
- IoC prensibine göre bağımlılıklar Extension sınıfında yönetilmiştir.

---

## 🛠️ Özel Özellikler

### 🔄 EF Core & Dinamik Sorgular
- EF Core'un sağladığı metotlar dışında ihtiyaca yönelik özel sorgular geliştirilmiştir.
- Örneğin: Bir yazarın tüm yazılarını listelemek, kategoriye göre filtreleme vb.

### ✨ AJAX Tabanlı Yorum Sistemi
- Yorumlar sayfa yenilenmeden gönderilir.
- Giriş yapmayan kullanıcılar yorum yapamaz; uyarı ve yönlendirme yapılır.

### 🔐 Slug Kullanımı ile URL Güvenliği
- URL'lerde ID yerine `slug` yapısı tercih edilmiştir.
- Örnek: `/blog/yazilimda-temiz-kod`

---

## 👥 Kimlik Doğrulama ve Yetkilendirme

- ASP.NET Identity altyapısı kullanılmıştır.
- Kullanıcı kayıt, giriş, şifre sıfırlama ve rol bazlı yetkilendirme işlemleri uygulanmıştır.

---

## 📚 Varlıklar ve İlişkiler

| Entity    | Açıklama                                |
|-----------|-----------------------------------------|
| 👤 User     | Kullanıcı bilgilerini barındırır         |
| 📝 Post     | Başlık, içerik, görsel gibi yazı verileri |
| 📂 Category | Yazının ait olduğu kategori              |
| 🏷️ Label    | Etiket bilgileri                         |

🔗 İlişkiler:
- Her yazı bir kullanıcıya aittir.
- Yazı ile etiket arasında çoktan çoğa ilişki bulunmaktadır.

---

## 🤖 Akıllı Yorum Analizi: Toksik İçeriği Engelleme

- Yorumlar, NLP destekli bir sistemle toksik içerik açısından analiz edilir.
- 🧠 Türkçe yorumlar önce İngilizceye çevrilir (Helsinki-NLP/opus-mt-tr-en).
- ☢️ Toksik olarak belirlenen yorumlar gösterilmez ya da silinir.

---

## 🖥️ Sayfa Özeti

### 🎛️ Admin Panel
- 📊 Dashboard: Grafikler, son makaleler ve son yorumlar.
- 📄 İçerikler: Kullanıcının yazıları kart görünümünde listelenir.
- ➕ Yeni Yazı: Başlık, içerik, görsel ve kategori bilgisi ile yeni içerik eklenir.
- 👤 Profil Güncelleme: Bilgi güncellemesi sonrası güvenlik için oturum kapanır.

### 🌐 Kullanıcı Arayüzü
- 🏠 Ana Sayfa: Yayınlanan tüm yazılar listelenir.
- 🧾 Kategoriler: Kategoriler ve ait oldukları yazılar görüntülenir.
- 🖋️ Yazarlar: Yazar listesi ve içerikleri.
- 🔐 Giriş Paneli: Üyelik ve yorum yapma yetkisi bu alandan sağlanır.

---

## 🧩 Temiz Başlangıç: DI & Program.cs

- BusinessLayer bağımlılıkları `ServiceRegistration` adında bir sınıfla yönetilir.
- `Program.cs` dosyası sadeleştirilmiş ve konfigürasyonlar ayrıştırılmıştır.

---

## 📦 Kullanılan Teknolojiler

- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Identity
- LINQ & Lambda
- AJAX / jQuery
- Bootstrap 5
- ToxicBERT + NLP çeviri modelleri
- Slugify

---

## 🙌 Katkı ve Geri Bildirim

Bu projeyi deneyimlediğiniz ve ilgi gösterdiğiniz için teşekkür ederim. Her türlü öneri ve geri bildirim benim için çok kıymetli.

📎 Projeyi klonlamak için:

```bash
git clone https://github.com/kullaniciadi/proje-adi.git
