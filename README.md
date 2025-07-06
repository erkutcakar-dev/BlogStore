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

git clone https://github.com/erkutcakar-dev

🖼️ Blog Görselleri
🧾
![Ekran görüntüsü 2025-07-06 225201](https://github.com/user-attachments/assets/ca117625-5691-4868-ba29-8aa9f24bf851)
![Ekran görüntüsü 2025-07-06 225151](https://github.com/user-attachments/assets/6403e575-63c5-46a5-a8c3-1bc2610c6dd9)
![Ekran görüntüsü 2025-07-06 224217](https://github.com/user-attachments/assets/db162357-94de-46dd-b0c7-2b857b65a0ae)
![Ekran görüntüsü 2025-07-06 225838](https://github.com/user-attachments/assets/b0f6bb9b-83ea-4ba7-b464-825fd9412f5d)
![Ekran görüntüsü 2025-07-06 225829](https://github.com/user-attachments/assets/5f71c90a-aa02-4a25-abf8-13c54c0e9262)
![Ekran görüntüsü 2025-07-06 225815](https://github.com/user-attachments/assets/6c8ea6dc-18a7-40f9-ba4f-3e98548a2675)
![Ekran görüntüsü 2025-07-06 225714](https://github.com/user-attachments/assets/62006b85-b858-4b0b-aef3-8bd77375542b)
![Ekran görüntüsü 2025-07-06 225710](https://github.com/user-attachments/assets/830fd1d3-5614-441b-9d1a-4f5fc8370bd2)


📊 Admin Panel Görselleri!
![Ekran görüntüsü 2025-07-06 225505](https://github.com/user-attachments/assets/67f5a166-ba00-4498-b893-7fcd09918114)
![Ekran görüntüsü 2025-07-06 222255](https://github.com/user-attachments/assets/ac0eb1ca-d3d2-4734-826a-8ab85b7cf210)
![Ekran görüntüsü 2025-07-06 222249](https://github.com/user-attachments/assets/c2983aa5-ff3b-4b2d-803f-c75656cb0cba)




