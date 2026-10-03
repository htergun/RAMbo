namespace RAMbo.App.Models;

/// <summary>Sayfa değiştirme algoritmalarının döndürdüğü ortak sonuç.</summary>
public class SimulationResult
{
    public string AlgorithmName { get; set; } = "";
    public int FrameCount { get; set; }
    public int[] ReferenceString { get; set; } = Array.Empty<int>();
    public List<StepResult> Steps { get; set; } = new();

    public int PageFaults => Steps.Count(s => s.IsFault);
    public int Hits => Steps.Count - PageFaults;
    public decimal HitRatio => Steps.Count == 0 ? 0 : Math.Round((decimal)Hits / Steps.Count * 100, 2);
}
