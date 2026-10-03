namespace RAMbo.App.Models;

/// <summary>Bölümleme algoritmalarının (Hafta 7) döndürdüğü ortak sonuç.</summary>
public class PartitionResult
{
    public string AlgorithmName { get; set; } = "";
    public List<Allocation> Allocations { get; set; } = new();
    public int InternalFragmentation { get; set; }
    public int ExternalFragmentation { get; set; }
}
