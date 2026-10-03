using RAMbo.App.Models;

namespace RAMbo.App.Services.Interfaces;

/// <summary>First Fit, Best Fit, Worst Fit bu arayüzü uygular (Hafta 7).</summary>
public interface IPartitionAlgorithm
{
    string Name { get; }
    PartitionResult Run(List<MemoryBlock> blocks, List<ProcessInfo> processes);
}
