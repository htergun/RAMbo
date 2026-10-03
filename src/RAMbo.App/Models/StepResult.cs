namespace RAMbo.App.Models;

/// <summary>SQL'deki "Results" tablosunun bir satırı: algoritmanın tek bir adımı.</summary>
public class StepResult
{
    public int Id { get; set; }
    public int SimulationId { get; set; }
    public int StepNumber { get; set; }
    public int PageNumber { get; set; }
    public bool IsFault { get; set; }
    public string FrameState { get; set; } = "";   // örn. "7,0,1"
    public int? VictimPage { get; set; }
}
