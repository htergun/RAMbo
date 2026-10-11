using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RAMbo.App.Data;
using RAMbo.App.Services;
using RAMbo.App.Services.Algorithms;
using RAMbo.App.Services.Interfaces;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var options = new DbContextOptionsBuilder<RamboDbContext>()
    .UseSqlServer(config.GetConnectionString("RamboDb"))
    .Options;

using var db = new RamboDbContext(options);
var repo = new SimulationRepository(db);
var runner = new SimulationRunner(repo);

Console.WriteLine("RAMbo - Bellek Yonetimi Simulatoru");
Console.WriteLine(new string('=', 50));

// Baglanti testi
try
{
    Console.WriteLine(db.Database.CanConnect()
        ? "Veritabani baglantisi: OK"
        : "Veritabani baglantisi BASARISIZ. Database/01_schema.sql dosyasini calistirdiniz mi?");
}
catch (Exception ex)
{
    Console.WriteLine("Baglanti hatasi: " + ex.Message);
}

// -------------------------------------------------------
// Hafta 6: Optimal Algoritmasi - Test & Karsilastirma
// -------------------------------------------------------
var refs = new[] { 7, 0, 1, 2, 0, 3, 0, 4, 2, 3, 0, 3, 2 };
int frameCount = 3;

Console.WriteLine($"\nReferans Dizisi: [{string.Join(", ", refs)}]");
Console.WriteLine($"Frame Sayisi   : {frameCount}\n");

// Tek algoritma detayli cikti (Optimal)
var optResult = runner.RunPageReplacement(new OptimalAlgorithm(), refs, frameCount, save: false);
Console.WriteLine($"--- {optResult.AlgorithmName} Adim Adim ---");
Console.WriteLine($"{"Adim",4} | {"Sayfa",5} | {"Frames",-14} | {"Fault?",6} | {"Victim",6}");
Console.WriteLine(new string('-', 50));
foreach (var step in optResult.Steps)
{
    Console.WriteLine($"{step.StepNumber,4} | {step.PageNumber,5} | {step.FrameState,-14} | {(step.IsFault ? "EVET  " : "HAYIR "),6} | {step.VictimPage?.ToString() ?? "-",6}");
}
Console.WriteLine($"\nPage Fault: {optResult.PageFaults} | Hit: {optResult.Hits} | Hit Ratio: {optResult.HitRatio}%");

// Karsilastirma tablosu (LRU hazir oldugunda aktif edilecek)
// IPageReplacementAlgorithm[] algorithms = [new FifoAlgorithm(), new LruAlgorithm(), new OptimalAlgorithm()];
// Console.WriteLine("\n=== KARSILASTIRMA RAPORU ===");
// Console.WriteLine($"{"Algoritma",-12} | {"Page Fault",10} | {"Hit",6} | {"Hit Ratio",10}");
// Console.WriteLine(new string('-', 48));
// foreach (var algo in algorithms)
// {
//     var r = runner.RunPageReplacement(algo, refs, frameCount, save: false);
//     Console.WriteLine($"{r.AlgorithmName,-12} | {r.PageFaults,10} | {r.Hits,6} | {r.HitRatio,9}%");
// }
