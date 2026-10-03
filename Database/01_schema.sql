-- RAMbo | COME 361 | Bellek Yönetimi Simülatörü
-- Hafta 2: SQL Server şeması
IF DB_ID('RamboMemoryDb') IS NULL CREATE DATABASE RamboMemoryDb;
GO
USE RamboMemoryDb;
GO

-- Her simülasyon çalıştırması (sayfa değiştirme veya bölümleme)
CREATE TABLE Simulations (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    Name            NVARCHAR(100) NOT NULL,
    SimulationType  NVARCHAR(30)  NOT NULL
        CHECK (SimulationType IN ('PageReplacement','Partitioning')),
    AlgorithmName   NVARCHAR(30)  NOT NULL,   -- FIFO, LRU, Optimal, FirstFit, BestFit, WorstFit
    FrameCount      INT NULL,                 -- sadece sayfa değiştirme için
    TotalPageFaults INT NULL,
    TotalHits       INT NULL,
    HitRatio        DECIMAL(5,2) NULL,
    InternalFragmentation INT NULL,           -- sadece bölümleme için
    ExternalFragmentation INT NULL,
    CreatedAt       DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

-- Sayfa değiştirme: referans dizisi
CREATE TABLE PageReferences (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    SimulationId  INT NOT NULL REFERENCES Simulations(Id) ON DELETE CASCADE,
    SequenceOrder INT NOT NULL,
    PageNumber    INT NOT NULL,
    CONSTRAINT UQ_PageRef UNIQUE (SimulationId, SequenceOrder)
);

-- Sayfa değiştirme: adım adım sonuç
CREATE TABLE Results (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    SimulationId INT NOT NULL REFERENCES Simulations(Id) ON DELETE CASCADE,
    StepNumber   INT NOT NULL,
    PageNumber   INT NOT NULL,
    IsFault      BIT NOT NULL,
    FrameState   NVARCHAR(200) NOT NULL,      -- örn. "7,0,1"
    VictimPage   INT NULL,                    -- çıkarılan sayfa (varsa)
    CONSTRAINT UQ_Result UNIQUE (SimulationId, StepNumber)
);

-- Bölümleme: süreçler
CREATE TABLE Processes (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    SimulationId INT NOT NULL REFERENCES Simulations(Id) ON DELETE CASCADE,
    Name         NVARCHAR(50) NOT NULL,
    Size         INT NOT NULL CHECK (Size > 0)
);

-- Bölümleme: bellek blokları
CREATE TABLE MemoryBlocks (
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    SimulationId INT NOT NULL REFERENCES Simulations(Id) ON DELETE CASCADE,
    BlockIndex   INT NOT NULL,
    Size         INT NOT NULL CHECK (Size > 0)
);

-- Bölümleme: hangi süreç hangi bloğa yerleşti
CREATE TABLE Allocations (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    SimulationId  INT NOT NULL REFERENCES Simulations(Id) ON DELETE CASCADE,
    ProcessId     INT NOT NULL REFERENCES Processes(Id),   -- cascade yok (döngü önleme)
    MemoryBlockId INT NULL REFERENCES MemoryBlocks(Id),    -- NULL = yerleştirilemedi
    InternalFragmentation INT NULL
);
GO

CREATE INDEX IX_Simulations_Algorithm ON Simulations(AlgorithmName, CreatedAt DESC);
GO
