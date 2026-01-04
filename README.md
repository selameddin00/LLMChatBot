| Bilgi | Detay |
| :--- | :--- |
| **Ad Soyad** | Selameddin Tirit |
| **Öğrenci No** | 240541035 |
| **Bölüm** | Yazılım Mühendisliği (A) |
| **Fakülte** | Teknoloji Fakültesi |

# 🤖 GeminiChatBot

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)
![WPF](https://img.shields.io/badge/WPF-Desktop-0078D4?logo=windows&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green.svg)

**Modern ve akıllı masaüstü AI asistanı - Google Gemini ile güçlendirilmiş**

[Özellikler](#-özellikler) • [Kurulum](#-kurulum) • [Kullanım](#-kullanım) • [Teknolojiler](#-teknolojiler)

</div>

---

## 📖 Hakkında

GeminiChatBot, Google Gemini 1.5 Flash modeli ile çalışan modern bir masaüstü AI asistan uygulamasıdır. WPF ve MVVM mimarisi kullanılarak geliştirilmiş, Material Design prensipleriyle tasarlanmış kullanıcı dostu bir arayüze sahiptir. Markdown desteği sayesinde AI yanıtlarınızı kod blokları, kalın yazılar ve listelerle zenginleştirilmiş şekilde görüntüleyebilirsiniz.

## 🖼️ Ekran Görüntüsü

![App Screenshot](screenshot.png)

> **Not:** Ekran görüntüsü eklendikten sonra bu bölüm güncellenecektir.

## ✨ Özellikler

- 🧠 **Google Gemini 1.5 Flash Entegrasyonu** - En güncel AI modeli ile akıllı sohbet deneyimi
- 🎨 **Modern Dark UI** - Material Design prensipleriyle tasarlanmış göz yormayan karanlık tema
- 📝 **Markdown Desteği** - Kod blokları, kalın yazı, listeler ve daha fazlası render edilir
- 💾 **Otomatik Sohbet Geçmişi** - Tüm konuşmalarınız otomatik olarak JSON formatında kaydedilir ve uygulama açılışında yüklenir
- ⚡ **Asenkron Yapı** - Arayüz donmadan sorunsuz kullanım deneyimi
- ⌨️ **Klavye Kısayolları** - Hızlı kullanım için pratik kısayollar
  - `Enter` - Mesaj gönder
  - `Shift + Enter` - Alt satıra geç
- 🔒 **Güvenli API Key Yönetimi** - Secrets.cs ile güvenli API key saklama

## 🛠️ Teknolojiler

Bu proje aşağıdaki teknolojiler ve kütüphaneler kullanılarak geliştirilmiştir:

| Teknoloji | Versiyon | Açıklama |
|-----------|----------|----------|
| **.NET** | 8.0 | Framework |
| **C#** | 12.0 | Programlama Dili |
| **WPF** | - | Masaüstü UI Framework |
| **CommunityToolkit.Mvvm** | 8.2.2 | MVVM Pattern Desteği |
| **MaterialDesignThemes** | 4.9.0 | Modern UI Bileşenleri |
| **MaterialDesignColors** | 2.1.4 | Renk Paleti |
| **Markdig.Wpf** | 0.5.0.1 | Markdown Render Desteği |
| **System.Text.Json** | - | JSON İşlemleri |
| **Google Gemini API** | v1beta | AI Model Entegrasyonu |

## 📦 Kurulum

### Gereksinimler

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) veya üzeri
- Windows 10/11
- Google Gemini API Key ([Nasıl alınır?](https://ai.google.dev/))

### Adım Adım Kurulum

1. **Projeyi Klonlayın**
   ```bash
   git clone https://github.com/kullaniciadi/GeminiChatBot.git
   cd GeminiChatBot
   ```

2. **API Key Dosyasını Oluşturun**
   
   Proje güvenliği için API key'ler `Secrets.cs` dosyasında saklanmaktadır. Bu dosya `.gitignore` içinde olduğu için repository'de bulunmaz.
   
   Ana dizinde (`GeminiChatBot` klasörü içinde) `Secrets.cs` adında yeni bir dosya oluşturun ve aşağıdaki kodu yapıştırın:
   
   ```csharp
   namespace GeminiChatBot;
   
   public static class Secrets
   {
       public const string GeminiApiKey = "BURAYA_API_KEY_GELECEK";
   }
   ```
   
   > **Önemli:** `BURAYA_API_KEY_GELECEK` kısmını kendi Google Gemini API key'iniz ile değiştirin.
   
   > **İpucu:** `Secrets.cs.example` dosyasını referans olarak kullanabilirsiniz.

3. **NuGet Paketlerini Yükleyin**
   ```bash
   dotnet restore
   ```

4. **Projeyi Derleyin**
   ```bash
   dotnet build
   ```

5. **Uygulamayı Çalıştırın**
   ```bash
   dotnet run
   ```
   
   Veya Visual Studio'da `F5` tuşuna basarak çalıştırabilirsiniz.

## 🚀 Kullanım

1. **İlk Kullanım**
   - Uygulama açıldığında, eğer daha önce sohbet geçmişi varsa otomatik olarak yüklenecektir.
   - Alt kısımdaki metin kutusuna sorunuzu yazın.

2. **Mesaj Gönderme**
   - Mesajınızı yazdıktan sonra `Enter` tuşuna basarak gönderebilirsiniz.
   - Çok satırlı mesaj yazmak için `Shift + Enter` kullanarak alt satıra geçebilirsiniz.

3. **Sohbet Geçmişi**
   - Tüm konuşmalarınız otomatik olarak kaydedilir.
   - Uygulama kapatılıp açıldığında sohbet geçmişiniz geri yüklenir.

4. **Markdown Desteği**
   - AI yanıtlarında markdown formatı desteklenir.
   - Kod blokları, listeler, kalın yazılar otomatik olarak render edilir.

## 📁 Proje Yapısı

```
GeminiChatBot/
├── Models/              # Veri modelleri
│   ├── ChatMessage.cs
│   ├── ChatSession.cs
│   └── Gemini/         # Gemini API modelleri
├── Services/            # İş mantığı servisleri
│   ├── GeminiService.cs
│   ├── IGeminiService.cs
│   └── FileService.cs
├── ViewModels/          # MVVM ViewModeller
│   └── MainViewModel.cs
├── Views/               # XAML görünümleri
│   ├── MainWindow.xaml
│   └── Converters.cs
├── Secrets.cs           # API Key (gitignore'da)
└── App.xaml            # Uygulama başlangıç dosyası
```

## 🔧 Geliştirme

### MVVM Mimarisi

Proje MVVM (Model-View-ViewModel) pattern'i kullanılarak geliştirilmiştir:

- **Model**: Veri yapıları ve iş mantığı (`Models/`, `Services/`)
- **View**: Kullanıcı arayüzü (`Views/`)
- **ViewModel**: View ve Model arasındaki köprü (`ViewModels/`)

### Katkıda Bulunma

1. Bu repository'yi fork edin
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add some amazing feature'`)
4. Branch'inizi push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

## 📝 Lisans

Bu proje [MIT Lisansı](LICENSE) altında lisanslanmıştır.

## 🙏 Teşekkürler

- [Google Gemini](https://ai.google.dev/) - AI modeli için
- [Material Design](https://materialdesignicons.com/) - UI bileşenleri için
- [Markdig](https://github.com/xoofx/markdig) - Markdown render desteği için

## 📧 İletişim

Sorularınız veya önerileriniz için issue açabilirsiniz.

---

<div align="center">

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!

Made with ❤️ using C# and WPF

</div>

