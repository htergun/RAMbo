namespace RAMbo.App.Models;

public class Simulation
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string SimulationType { get; set; } = "PageReplacement"; // veya "Partitioning"
    public string AlgorithmName { get; set; } = "";
    public int? FrameCount { get; set; }
    public int? TotalPageFaults { get; set; }
    public int? TotalHits { get; set; }
    public decimal? HitRatio { get; set; }
    public int? InternalFragmentation { get; set; }
    public int? ExternalFragmentation { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<PageReference> PageReferences { get; set; } = new();
    public List<StepResult> Results { get; set; } = new();
    public List<ProcessInfo> Processes { get; set; } = new();
    public List<MemoryBlock> MemoryBlocks { get; set; } = new();
    public List<Allocation> Allocations { get; set; } = new();
}
