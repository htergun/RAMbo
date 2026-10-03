# RAMbo | Hafta 2: Mimari ve Veritabanı Tasarımı

COME 361 – Operating Systems | Chapter 9 – Memory Management

## 1. Katmanlı mimari

```mermaid
flowchart TB
    UI["UI Katmanı<br/>Program.cs (Console) → ileride WPF/WinForms"]
    BLL["İş Mantığı Katmanı (Services)<br/>FIFO, LRU, Optimal, FirstFit, BestFit, WorstFit<br/>SimulationRunner"]
    DAL["Veri Erişim Katmanı (Data)<br/>RamboDbContext, SimulationRepository"]
    DB[("SQL Server<br/>RamboMemoryDb")]
    M["Models<br/>(tüm katmanlar kullanır)"]
    UI --> BLL --> DAL --> DB
    UI -.-> M
    BLL -.-> M
    DAL -.-> M
```

| Katman | Klasör | Sorumluluk |
|---|---|---|
| UI | `Program.cs` | Kullanıcıdan girdi alır, sonucu gösterir. Algoritma bilmez. |
| İş mantığı | `Services/` | Algoritmalar burada. Veritabanını bilmez, sadece model döner. |
| Veri erişimi | `Data/` | EF Core ile kaydet/oku. Algoritma bilmez. |
| Modeller | `Models/` | Düz veri sınıfları. |

**Kural:** Algoritmalar veritabanına doğrudan dokunmaz. Bu sayede herkes kendi algoritmasını bağımsız yazıp sonda birleştirebilir.

## 2. ER diyagramı

```mermaid
erDiagram
    Simulations ||--o{ PageReferences : has
    Simulations ||--o{ Results : has
    Simulations ||--o{ Processes : has
    Simulations ||--o{ MemoryBlocks : has
    Simulations ||--o{ Allocations : has
    Processes ||--o| Allocations : "placed as"
    MemoryBlocks ||--o{ Allocations : "holds"

    Simulations {
        int Id PK
        string Name
        string SimulationType
        string AlgorithmName
        int FrameCount
        int TotalPageFaults
        int TotalHits
        decimal HitRatio
        int InternalFragmentation
        int ExternalFragmentation
        datetime CreatedAt
    }
    PageReferences {
        int Id PK
        int SimulationId FK
        int SequenceOrder
        int PageNumber
    }
    Results {
        int Id PK
        int SimulationId FK
        int StepNumber
        int PageNumber
        bool IsFault
        string FrameState
        int VictimPage
    }
    Processes {
        int Id PK
        int SimulationId FK
        string Name
        int Size
    }
    MemoryBlocks {
        int Id PK
        int SimulationId FK
        int BlockIndex
        int Size
    }
    Allocations {
        int Id PK
        int SimulationId FK
        int ProcessId FK
        int MemoryBlockId FK
        int InternalFragmentation
    }
```

## 3. Tablo açıklamaları

- **Simulations:** Her çalıştırmanın özeti. `SimulationType` ile sayfa değiştirme ve bölümleme ayrılır, tek tablo iki konuya hizmet eder.
- **PageReferences / Results:** Sayfa değiştirme (Hafta 4-6) için referans dizisi ve adım adım frame durumu.
- **Processes / MemoryBlocks / Allocations:** Bölümleme (Hafta 7) için. `MemoryBlockId = NULL` olan satır, sürecin hiçbir bloğa sığmadığını gösterir (dış parçalanma analizi).

## 4. Görev dağılımı için sözleşme

Herkes `Services/Interfaces/` altındaki arayüzleri kullanır. `Models/` ve arayüzler değişecekse önce grupla konuşulur.
