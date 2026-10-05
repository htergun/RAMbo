using RAMbo.App.Models;

namespace RAMbo.App.Services.Algorithms;

/// <summary>Sığan en büyük bloğa yerleştirir.</summary>
public class WorstFit : PartitionAlgorithmBase
{
    public override string Name => "Worst Fit";

    protected override int SelectBlock(int[] available, int processSize)
    {
        int worst = -1;
        for (int i = 0; i < available.Length; i++)
        {
            if (available[i] < processSize) continue;
            if (worst == -1 || available[i] > available[worst]) worst = i;
        }
        return worst;
    }
}
