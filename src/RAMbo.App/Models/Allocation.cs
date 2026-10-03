namespace RAMbo.App.Models;

public class Allocation
{
    public int Id { get; set; }
    public int SimulationId { get; set; }
    public int ProcessId { get; set; }
    public int? MemoryBlockId { get; set; }   // null = yerleştirilemedi
    public int? InternalFragmentation { get; set; }
}
