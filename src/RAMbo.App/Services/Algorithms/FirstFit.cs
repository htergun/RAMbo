using RAMbo.App.Models;

namespace RAMbo.App.Services.Algorithms;

/// <summary>İlk uygun (yeterince büyük) bloğa yerleştirir.</summary>
public class FirstFit : PartitionAlgorithmBase
{
    public override string Name => "First Fit";

    protected override int SelectBlock(int[] available, int processSize)
    {
        for (int i = 0; i < available.Length; i++)
            if (available[i] >= processSize)
                return i;
        return -1;
    }
}