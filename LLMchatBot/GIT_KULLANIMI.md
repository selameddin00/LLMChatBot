# Git Kullanımı - GeminiChatBot Projesi

## ÖNEMLİ: Secrets.cs Dosyasını PAYLAŞMAYIN!

`Secrets.cs` dosyası API Key içerir ve **ASLA** Git repository'ye eklenmemelidir.

## .gitignore Nedir?

`.gitignore` dosyası, Git'in hangi dosya ve klasörleri izlemeyeceğini (ignore edeceğini) belirler.

## Git Komutları (İlk Kullanım)

### 1. Git Repository Oluştur (İlk Kez)
```bash
git init
```

### 2. .gitignore'ın Çalıştığını Kontrol Et
```bash
git status
```
Bu komut, Git'in hangi dosyaları takip ettiğini gösterir. `Secrets.cs` listede **OLMAMALI**.

### 3. Dosyaları Ekle (Secrets.cs hariç)
```bash
git add .
```
Bu komut `.gitignore`'daki kurallara göre dosyaları ekler. `Secrets.cs` otomatik olarak atlanır.

### 4. İlk Commit
```bash
git commit -m "Initial commit - GeminiChatBot projesi"
```

## Eğer Secrets.cs Zaten Git'e Eklenmişse

Eğer daha önce `Secrets.cs` dosyasını yanlışlıkla `git add` ile eklediyseniz:

```bash
# Git'ten kaldır (dosya yerel diskte kalır)
git rm --cached Secrets.cs

# Commit et
git commit -m "Secrets.cs dosyasını Git'ten kaldır"
```

## GitHub'a Push Etme

```bash
# GitHub'da repository oluşturduktan sonra
git remote add origin https://github.com/KULLANICI_ADI/REPO_ADI.git
git branch -M main
git push -u origin main
```

## Paylaşılacak Dosyalar

✅ **Paylaşılabilir:**
- Tüm `.cs` dosyaları (Secrets.cs hariç)
- `.xaml` dosyaları
- `.csproj` ve `.sln` dosyaları
- `docs/` klasörü
- `.gitignore` dosyası

❌ **Paylaşılmamalı:**
- `Secrets.cs` - API Key içerir
- `bin/` klasörü - Derleme çıktıları
- `obj/` klasörü - Derleme çıktıları
- `.vs/` klasörü - Visual Studio ayarları

## Önemli Notlar

1. **Secrets.cs dosyasını asla commit etmeyin!**
2. Projeyi paylaşırken, `Secrets.cs` dosyası yerine `Secrets.cs.example` dosyası oluşturabilirsiniz:
   ```csharp
   public const string GeminiApiKey = "BURAYA_GERCEK_KEYINI_YAZ";
   ```
3. `.gitignore` dosyası zaten hazır, ekstra bir şey yapmanıza gerek yok.

