using Microsoft.EntityFrameworkCore;
using RAMbo.App.Models;

namespace RAMbo.App.Data;

/// <summary>Hafta 8'de CRUD tamamlanacak. Şimdilik kaydet, listele, getir, sil var.</summary>
public class SimulationRepository
{
    private readonly RamboDbContext _db;

    public SimulationRepository(RamboDbContext db) => _db = db;

    public int SavePageReplacement(SimulationResult r)
    {
        var sim = new Simulation
        {
            Name = $"{r.AlgorithmName} - {r.FrameCount} frame",
            SimulationType = "PageReplacement",
            AlgorithmName = r.AlgorithmName,
            FrameCount = r.FrameCount,
            TotalPageFaults = r.PageFaults,
            TotalHits = r.Hits,
            HitRatio = r.HitRatio,
            PageReferences = r.ReferenceString
                .Select((p, i) => new PageReference { SequenceOrder = i + 1, PageNumber = p }).ToList(),
            Results = r.Steps
        };
        _db.Simulations.Add(sim);
        _db.SaveChanges();
        return sim.Id;
    }

    public List<Simulation> GetAll() =>
        _db.Simulations.AsNoTracking().OrderByDescending(s => s.CreatedAt).ToList();

    public Simulation? GetById(int id) =>
        _db.Simulations.Include(s => s.Results).Include(s => s.PageReferences)
            .FirstOrDefault(s => s.Id == id);

    public void Delete(int id)
    {
        var s = _db.Simulations.Find(id);
        if (s == null) return;
        _db.Simulations.Remove(s);
        _db.SaveChanges();
    }
}
