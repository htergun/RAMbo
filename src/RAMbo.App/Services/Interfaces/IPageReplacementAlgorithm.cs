using RAMbo.App.Models;

namespace RAMbo.App.Services.Interfaces;

/// <summary>FIFO, LRU ve Optimal bu arayüzü uygular (Hafta 4-6).</summary>
public interface IPageReplacementAlgorithm
{
    string Name { get; }
    SimulationResult Run(int[] referenceString, int frameCount);
}
