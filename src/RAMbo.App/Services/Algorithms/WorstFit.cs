using RAMbo.App.Models;
using RAMbo.App.Services.Interfaces;

namespace RAMbo.App.Services.Algorithms;

// TODO (Hafta 7): Bu sınıfı ilgili kişi dolduracak.
public class WorstFit : IPartitionAlgorithm
{
    public string Name => "Worst Fit";

    public PartitionResult Run(List<MemoryBlock> blocks, List<ProcessInfo> processes)
    {
        throw new NotImplementedException("Worst Fit Hafta 7'de yazılacak.");
    }
}
