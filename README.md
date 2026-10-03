# RAMbo | Bellek Yönetimi Simülatörü (COME 361)

## Kurulum
1. SQL Server (veya LocalDB) üzerinde `Database/01_schema.sql` dosyasını çalıştırın.
2. `src/RAMbo.App/appsettings.json` içindeki bağlantı cümlesini kendi makinenize göre düzenleyin.
3. Çözüm dosyasını oluşturun (bir kere, bir kişi):
   ```
   dotnet new sln -n RAMbo
   dotnet sln add src/RAMbo.App/RAMbo.App.csproj
   ```
4. Çalıştırın: `dotnet run --project src/RAMbo.App`
   Konsolda "Veritabanı bağlantısı: OK" görmelisiniz.

## Kim neyi dolduruyor
| Dosya | Hafta |
|---|---|
| `Services/Algorithms/FifoAlgorithm.cs` | 4 |
| `Services/Algorithms/LruAlgorithm.cs` | 5 |
| `Services/Algorithms/OptimalAlgorithm.cs` | 6 |
| `Services/Algorithms/FirstFit / BestFit / WorstFit.cs` | 7 |

Herkes sadece kendi dosyasına dokunur. `Models/` ve `Services/Interfaces/` değişecekse önce grupla konuşulur.

Mimari ve ER diyagramı: `Docs/Mimari_ve_Veritabani.md`
