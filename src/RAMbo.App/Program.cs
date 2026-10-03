using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RAMbo.App.Data;
using RAMbo.App.Services;

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

Console.WriteLine("RAMbo - Bellek Yönetimi Simülatörü");

// Bağlantı testi
try
{
    Console.WriteLine(db.Database.CanConnect()
        ? "Veritabanı bağlantısı: OK"
        : "Veritabanı bağlantısı BAŞARISIZ. Database/01_schema.sql dosyasını çalıştırdınız mı?");
}
catch (Exception ex)
{
    Console.WriteLine("Bağlantı hatası: " + ex.Message);
}

// Algoritmalar yazıldıkça burası açılacak:
// var refs = new[] { 7, 0, 1, 2, 0, 3, 0, 4, 2, 3, 0, 3, 2 };
// var result = runner.RunPageReplacement(new FifoAlgorithm(), refs, 3);
// Console.WriteLine($"{result.AlgorithmName}: {result.PageFaults} sayfa hatası");
