using RAMbo.App.Models;

namespace RAMbo.App.Services.Algorithms;

/// <summary>Sığan en küçük bloğa yerleştirir.</summary>
public class BestFit : PartitionAlgorithmBase
{
    public override string Name => "Best Fit";

    protected override int SelectBlock(int[] available, int processSize)
    {
        int best = -1;
        for (int i = 0; i < available.Length; i++)
        {
            if (available[i] < processSize) continue;
            if (best == -1 || available[i] < available[best]) best = i;
        }
        return best;
    }
}