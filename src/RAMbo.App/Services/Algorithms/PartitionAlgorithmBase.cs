using RAMbo.App.Models;
using RAMbo.App.Services.Interfaces;

namespace RAMbo.App.Services.Algorithms;

/// <summary>
/// First/Best/Worst Fit için ortak yerleştirme ve fragmentation hesabı.
/// Alt sınıflar sadece hangi bloğun seçileceğini (SelectBlock) belirler.
/// Girdi listelerindeki nesneler değiştirilmez.
/// </summary>
public abstract class PartitionAlgorithmBase : IPartitionAlgorithm
{
    public abstract string Name { get; }

    public PartitionMode Mode { get; set; } = PartitionMode.Fixed;

    /// <summary>
    /// available[i]: i. bloğun şu an kullanılabilir boyutu (-1 = dolu, kullanılamaz).
    /// Uygun bloğun indeksini döner, yoksa -1.
    /// </summary>
    protected abstract int SelectBlock(int[] available, int processSize);

    public PartitionResult Run(List<MemoryBlock> blocks, List<ProcessInfo> processes)
    {
        var ordered = blocks.OrderBy(b => b.BlockIndex).ToList();
        var available = ordered.Select(b => b.Size).ToArray();

        var result = new PartitionResult { AlgorithmName = Name };
        bool anyFailed = false;

        foreach (var p in processes)
        {
            int idx = SelectBlock(available, p.Size);

            if (idx < 0)
            {
                anyFailed = true;
                result.Allocations.Add(new Allocation
                {
                    SimulationId = p.SimulationId,
                    ProcessId = p.Id,
                    MemoryBlockId = null,          // yerleştirilemedi
                    InternalFragmentation = null
                });
                continue;
            }

            int internalFrag;
            if (Mode == PartitionMode.Fixed)
            {
                internalFrag = ordered[idx].Size - p.Size;
                available[idx] = -1;               // blok tamamen dolu sayılır
            }
            else
            {
                internalFrag = 0;
                available[idx] -= p.Size;          // kalan boşluk kullanılabilir
            }

            result.Allocations.Add(new Allocation
            {
                SimulationId = p.SimulationId,
                ProcessId = p.Id,
                MemoryBlockId = ordered[idx].Id,
                InternalFragmentation = internalFrag
            });
            result.InternalFragmentation += internalFrag;
        }

        // Yerleşemeyen process varken boşta kalan toplam bellek = external fragmentation
        result.ExternalFragmentation = anyFailed
            ? available.Where(a => a > 0).Sum()
            : 0;

        return result;
    }
}