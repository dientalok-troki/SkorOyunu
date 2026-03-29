# 🎮 Skor Toplama Oyunu (C# Console)

## 📌 Proje Açıklaması
Bu proje, C# programlama dili kullanılarak geliştirilmiş console tabanlı bir skor toplama oyunudur. Oyuncu, yukarıdan düşen objeleri yakalayarak skor elde etmeye çalışır.

---

## 🎯 Oyun Mekanikleri
- Oyuncu karakteri: `@`
- Ok tuşları ile hareket edilir
- Oyuncu:
  - Yatayda 4 birim
  - Dikeyde 5 birim hareket eder

---

## 🎲 Düşen Objeler
Oyunda aynı anda 4 adet obje düşmektedir:

- `*` → 1 puan (2 adet)
- `O` → 3 puan (2 adet)

Objeler ekranın üstünden rastgele konumlarda doğar ve aşağı doğru hareket eder.

---

## 🧮 Skor Sistemi
- `*` yakalanırsa → +1 puan
- `O` yakalanırsa → +3 puan
- Skor 30'a ulaştığında oyun sona erer

---

## ⏱ Oyun Süresi
- Oyun süresi: **25 saniye**
- Süre dolduğunda oyun otomatik olarak biter

---

## 🎮 Kontroller
- `←` `→` `↑` `↓` → Hareket
- `R` → Oyunu yeniden başlat
- `ESC` → Oyundan çık

---

## 🧾 Log (Debug) Sistemi
Oyun sırasında aşağıdaki işlemler `log.txt` dosyasına kaydedilir:

- Tuş basımları (INPUT)
- Oyuncu konum değişimi (PLAYER)
- Obje hareketleri (UPDATE)
- Çarpışma kontrolleri (COLLISION)
- Skor değişimleri (SCORE)
- Oyun bitişi (GAME OVER)

---

## 🛠 Kullanılan Teknolojiler
- C#
- .NET Console
- System.IO (log sistemi için)
