# 💳 DigitalWallet - Secure Digital Wallet API 🛡️

### Developed by Özgür Kadakal

> **⚠️ Important:** This project uses **SQL Server (LocalDB)** and requires a **JWT Secret Key** before running.

---

# 🖼️ Previews & API Testing

## 🟢 API Overview (Swagger UI)

![Swagger](Galery/6.png)

---

## 🔐 Authentication & JWT Generation (Postman)

> Secure login system that generates a valid JSON Web Token (JWT) for authorized requests.

![Login](Galery/2.png)

---

## 💰 Secure Deposit & Idempotency (Postman)

> Users can securely deposit money. The Idempotency mechanism prevents duplicate financial transactions caused by network failures or repeated requests.

![Deposit](Galery/3.png)

---

## 💸 Fund Transfer & KVKK Compliance (Postman)

> Secure peer-to-peer transfers using Turkish Identity Numbers (TC). Sensitive information is masked in logs to comply with KVKK privacy principles.

![Transfer](Galery/4.png)

---

## 📜 Paginated Transaction History (Postman)

> Optimized transaction history with server-side pagination using **PageNumber** and **PageSize**.

![History](Galery/5.png)

---

# 💳 DigitalWallet API

DigitalWallet is a secure backend project developed with **ASP.NET Core Web API**.

The project focuses on:

- Financial security
- Wallet management
- JWT authentication
- Idempotent financial operations
- Rate limiting
- Structured logging
- KVKK-compliant sensitive data masking
- Clean layered architecture

---

# ✨ Features

## 🔐 Authentication & Security

- 🛡️ JWT Authentication
  - Secure token-based authentication.

- 🔑 Password Hashing
  - Passwords are securely stored using BCrypt.

- 🔒 KVKK Data Protection
  - Sensitive data such as Turkish Identity Numbers are masked in logs.
  - Example:
    ```
    111*****112
    ```

---

## 💰 Wallet & Financial Operations

### 💵 Balance Inquiry

- View the current wallet balance instantly.

### 📥 Deposit

- Securely deposit money into the wallet.

### 📤 Withdraw

- Withdraw money with balance validation.

### 💸 Transfer

- Send money to another user using their Turkish Identity Number.

### 🔄 Idempotency Support

- Prevents duplicate deposits and transfers during unstable network conditions or repeated requests.

### 📜 Transaction History

- Paginated transaction history.

- Supports:
  - PageNumber
  - PageSize

---

## 🛡️ Protection & Observability

### ⚡ Rate Limiting

Sliding Window Rate Limiter protects endpoints against:

- Brute Force attacks
- DDoS attempts
- Excessive requests

Returns

```
429 Too Many Requests
```

when limits are exceeded.

### 📝 Structured Logging

Serilog records important operations into rolling log files.

Examples include:

- Login attempts
- Transfers
- Deposits
- Withdrawals
- Security warnings

---

## ⚙️ Backend Architecture

- 🧩 Layered Architecture

```
Controller
     ↓
 Service
     ↓
 Repository
     ↓
 Database
```

- 🔄 Dependency Injection

- 🗄️ Entity Framework Core

- 🌐 Global Exception Handling

- ⚡ Async Programming

---

# 🚀 Quick Setup for Testing (Docker)

If you only want to test the project, you do not need to install Visual Studio or SQL Server.

Run:

```bash
docker-compose up -d --build
```

This command automatically starts both the API and the database in the background.

> ⚠️ **Warning:** For testing convenience, the JWT Secret Key and database connection settings are embedded in the `docker-compose.yml` file. If you want to use your own credentials, update the `environment` variables in that file before starting the project.

---

# ⚙️ Setup

## 1️⃣ Clone the Repository

Clone the repository to your local machine.

```bash
git clone https://github.com/kadakalozgur/DigitalWallet.git
```

Navigate to the project directory.

```bash
cd DigitalWallet
```

---

## 2️⃣ Restore / Install Required NuGet Packages

When you open the project with Visual Studio, the required NuGet packages are restored automatically. If, for any reason, they are not restored, you can install them manually using **NuGet Package Manager**.

### Required Packages

| Package | Purpose |
|----------|---------|
| BCrypt.Net-Next | Password hashing |
| IdempotentAPI | Prevent duplicate financial requests |
| IdempotentAPI.Cache.DistributedCache | Distributed cache support for IdempotentAPI |
| Microsoft.AspNetCore.Authentication.JwtBearer | JWT Authentication |
| Microsoft.EntityFrameworkCore.SqlServer | SQL Server provider |
| Microsoft.EntityFrameworkCore.Tools | Entity Framework migrations |
| Serilog.AspNetCore | Structured logging |
| Swashbuckle.AspNetCore | Swagger / OpenAPI documentation |

Or install them manually using the **Package Manager Console**:

```powershell
Install-Package BCrypt.Net-Next
Install-Package IdempotentAPI
Install-Package IdempotentAPI.Cache.DistributedCache
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Serilog.AspNetCore
Install-Package Swashbuckle.AspNetCore
```

---

## 3️⃣ Configure the Database Connection (Optional)

Open the `appsettings.json` file and ensure that the connection string matches your local SQL Server instance.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DigitalWalletDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

> 💡 The default configuration uses **SQL Server LocalDB**. If you are using SQL Server Express or another SQL Server instance, update the connection string accordingly.

---

## 4️⃣ Create the Database

Open:

```
Tools
→ NuGet Package Manager
→ Package Manager Console
```

The application is configured to automatically apply any pending Entity Framework Core migrations during startup via `Program.cs`. If you prefer to create the database manually before running the application, you can use the following command.

```powershell
Update-Database
```

> ⚠️ Skipping this step will cause the application to fail during startup.

---

## 5️⃣ Configure JWT Secret Key

### Option 1 — appsettings.json

```json
"JwtSettings": {
  "Issuer": "DigitalWalletApp",
  "Audience": "DigitalWalletUsers",
  "Key": "your-super-secret-key-at-least-32-bytes"
}
```

### Option 2 — User Secrets (Recommended)

Initialize User Secrets:

```bash
dotnet user-secrets init
```

Then set your JWT key:

```bash
dotnet user-secrets set "JwtSettings:Key" "your-super-secret-key-at-least-32-bytes"
```

---

# 🛠 Tech Stack

| Category | Technology |
|----------|------------|
| Language | C# |
| Framework | ASP.NET Core Web API |
| Database | SQL Server (LocalDB) |
| ORM | Entity Framework Core |
| Authentication | JWT (JSON Web Token) |
| Security | IdempotencyAPI, ASP.NET Core Rate Limiting |
| Logging | Serilog |
| IDE | Visual Studio 2022 |

---

# 📚 Additional Notes

- This project was developed to practice **advanced backend engineering** and **financial application architecture**.
- AI-assisted tools were used during the development process.
- The project was developed from scratch by **Özgür Kadakal**.

---

# 📬 Contact

For feedback, questions or suggestions:

📧 **ozgurkreach@gmail.com**

---

---

# 🇹🇷 Türkçe

# 💳 DigitalWallet - Güvenli Dijital Cüzdan API 🛡️

### Geliştirici: Özgür Kadakal

> **⚠️ Önemli:** Bu proje **SQL Server (LocalDB)** kullanır ve çalıştırmadan önce **JWT Secret Key** tanımlanmalıdır.

---

# 🖼️ Önizlemeler & API Testleri

## 🟢 API Genel Bakış (Swagger UI)

![Swagger](Galery/6.png)

---

## 🔐 Kimlik Doğrulama & JWT Üretimi (Postman)

> Yetkili istekler için geçerli bir JSON Web Token (JWT) üreten güvenli giriş sistemi.

![Login](Galery/2.png)

---

## 💰 Güvenli Para Yatırma & Idempotency (Postman)

> Kullanıcılar güvenli şekilde para yatırabilir. Idempotency mekanizması, ağ hataları veya tekrar eden istekler nedeniyle oluşabilecek mükerrer finansal işlemleri engeller.

![Deposit](Galery/3.png)

---

## 💸 Para Transferi & KVKK Uyumu (Postman)

> TC Kimlik Numarası ile güvenli para transferi yapılır. Hassas veriler loglarda maskelenerek KVKK ilkelerine uygun şekilde korunur.

![Transfer](Galery/4.png)

---

## 📜 Sayfalanmış İşlem Geçmişi (Postman)

> PageNumber ve PageSize parametreleri kullanılarak sunucu tarafında optimize edilmiş sayfalama desteği.

![History](Galery/5.png)

---

# 💳 DigitalWallet API

DigitalWallet, **ASP.NET Core Web API** kullanılarak geliştirilmiş güvenli bir backend projesidir.

Odak noktaları:

- Finansal güvenlik
- Cüzdan yönetimi
- JWT kimlik doğrulama
- Idempotent finansal işlemler
- Rate Limiting
- Yapısal loglama
- KVKK uyumlu veri maskeleme
- Katmanlı mimari

---

# ✨ Özellikler

## 🔐 Kimlik Doğrulama ve Güvenlik

- 🛡️ JWT Authentication
  - Token tabanlı güvenli kimlik doğrulama.

- 🔑 BCrypt
  - Şifreler BCrypt algoritması ile hashlenerek saklanır.

- 🔒 KVKK Veri Koruması
  - TC Kimlik Numaraları gibi hassas bilgiler loglarda maskelenir.

Örnek:

```
111*****112
```

---

## 💰 Cüzdan İşlemleri

### 💵 Bakiye Sorgulama

- Güncel cüzdan bakiyesi görüntülenebilir.

### 📥 Para Yatırma

- Güvenli şekilde bakiye yükleme.

### 📤 Para Çekme

- Bakiye kontrolü yapılarak para çekilebilir.

### 💸 Para Transferi

- TC Kimlik Numarası ile kullanıcılar arasında para transferi.

### 🔄 Idempotency

- Aynı isteğin tekrar gönderilmesi durumunda mükerrer para transferlerini önler.

### 📜 İşlem Geçmişi

- Sayfalama destekli işlem geçmişi.

Desteklenen parametreler:

- PageNumber
- PageSize

---

## 🛡️ Sistem Koruması

### ⚡ Rate Limiting

Sliding Window algoritması sayesinde API aşağıdakilere karşı korunur:

- Brute Force saldırıları
- DDoS girişimleri
- Çok sık yapılan istekler

Limit aşılırsa

```
429 Too Many Requests
```

cevabı döndürülür.

### 📝 Yapısal Loglama

Serilog ile aşağıdaki işlemler günlük dosyalarına kaydedilir:

- Giriş denemeleri
- Para transferleri
- Para yatırma
- Para çekme
- Güvenlik uyarıları

---

## ⚙️ Backend Mimarisi

```
Controller
     ↓
 Service
     ↓
 Repository
     ↓
 Database
```

- Dependency Injection
- Entity Framework Core
- Global Exception Handling
- Async Programming

---

# 🚀 Test Edenler İçin Hızlı Kurulum (Docker)

Projeyi yalnızca test etmek istiyorsanız Visual Studio veya SQL Server kurmanıza gerek yoktur.

Aşağıdaki komutu çalıştırmanız yeterlidir:

```bash
docker-compose up -d --build
```

Bu komut API ve veritabanını arka planda otomatik olarak ayağa kaldırır.

> ⚠️ **Uyarı:** Test kolaylığı için JWT Secret Key ve veritabanı bağlantı bilgileri `docker-compose.yml` dosyasına gömülüdür. Kendi bilgilerinizi kullanmak isterseniz projeyi başlatmadan önce bu dosyadaki `environment` değişkenlerini güncelleyebilirsiniz.

---

# ⚙️ Kurulum

## 1️⃣ Projeyi Klonlayın

Projeyi bilgisayarınıza klonlayın.

```bash
git clone https://github.com/kadakalozgur/DigitalWallet.git
```

Proje dizinine geçin.

```bash
cd DigitalWallet
```

---

## 2️⃣ Gerekli NuGet Paketlerini Yükleyin

Projeyi Visual Studio ile açtığınızda gerekli NuGet paketleri otomatik olarak yüklenir. Ancak herhangi bir sebeple yüklenmezse, **NuGet Package Manager** kullanarak aşağıdaki paketleri manuel olarak yükleyebilirsiniz.

### Gerekli Paketler

| Paket | Açıklama |
|--------|----------|
| BCrypt.Net-Next | Şifrelerin güvenli şekilde hashlenmesi |
| IdempotentAPI | Mükerrer finansal istekleri önler |
| IdempotentAPI.Cache.DistributedCache | IdempotentAPI için dağıtık önbellek desteği |
| Microsoft.AspNetCore.Authentication.JwtBearer | JWT tabanlı kimlik doğrulama |
| Microsoft.EntityFrameworkCore.SqlServer | SQL Server sağlayıcısı |
| Microsoft.EntityFrameworkCore.Tools | Entity Framework migration araçları |
| Serilog.AspNetCore | Yapısal loglama (Structured Logging) |
| Swashbuckle.AspNetCore | Swagger / OpenAPI dokümantasyonu |

Veya paketleri **Package Manager Console** üzerinden manuel olarak yükleyebilirsiniz:

```powershell
Install-Package BCrypt.Net-Next
Install-Package IdempotentAPI
Install-Package IdempotentAPI.Cache.DistributedCache
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Serilog.AspNetCore
Install-Package Swashbuckle.AspNetCore
```

---

## 3️⃣ Veritabanı Bağlantısını Yapılandırın (Opsiyonel)

`appsettings.json` dosyasını açın ve bağlantı cümlesinin (**Connection String**) kendi yerel SQL Server örneğinizle eşleştiğinden emin olun.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DigitalWalletDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

> 💡 Varsayılan yapılandırma **SQL Server LocalDB** kullanmaktadır. SQL Server Express veya farklı bir SQL Server örneği kullanıyorsanız bağlantı cümlesini buna göre güncelleyin.

---

## 4️⃣ Veritabanını Oluşturun

Visual Studio'da aşağıdaki yolu izleyin:

```
Tools
→ NuGet Package Manager
→ Package Manager Console
```

Uygulama, `Program.cs` üzerinden bekleyen Entity Framework Core migrationlarını başlangıçta otomatik olarak uygulayacak şekilde yapılandırılmıştır. Ancak projeyi çalıştırmadan önce veritabanını manuel olarak oluşturmak isterseniz aşağıdaki komutu kullanabilirsiniz.

```powershell
Update-Database
```

> ⚠️ Bu adımı atlarsanız uygulama başlatılırken hata oluşacaktır.

---

## 5️⃣ JWT Secret Key Ayarlayın

### Seçenek 1 — appsettings.json

```json
"JwtSettings": {
  "Issuer": "DigitalWalletApp",
  "Audience": "DigitalWalletUsers",
  "Key": "gizli-ve-en-az-32-byte-anahtariniz"
}
```

### Seçenek 2 — User Secrets (Önerilir)

Öncelikle User Secrets özelliğini etkinleştirin:

```bash
dotnet user-secrets init
```

Ardından JWT anahtarınızı tanımlayın:

```bash
dotnet user-secrets set "JwtSettings:Key" "gizli-ve-en-az-32-byte-anahtariniz"
```

---

# 🛠 Kullanılan Teknolojiler

| Kategori | Teknoloji |
|----------|-----------|
| Dil | C# |
| Framework | ASP.NET Core Web API |
| Veritabanı | SQL Server (LocalDB) |
| ORM | Entity Framework Core |
| Kimlik Doğrulama | JWT |
| Güvenlik | IdempotencyAPI, ASP.NET Core Rate Limiting |
| Loglama | Serilog |
| IDE | Visual Studio 2022 |

---

# 📚 Ek Notlar

- Bu proje ileri seviye backend geliştirme pratiği amacıyla hazırlanmıştır.
- Geliştirme sürecinde yapay zekâ destekli araçlardan yararlanılmıştır.
- Proje tamamen Özgür Kadakal tarafından geliştirilmiştir.

---

# 📬 İletişim

Her türlü geri bildirim, öneri veya soru için:

📧 **ozgurkreach@gmail.com**
