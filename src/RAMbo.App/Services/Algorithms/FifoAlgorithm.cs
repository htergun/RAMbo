using RAMbo.App.Models;
using RAMbo.App.Services.Interfaces;

namespace RAMbo.App.Services.Algorithms;

public class FifoAlgorithm : IPageReplacementAlgorithm
{
    public string Name => "FIFO";

    public SimulationResult Run(int[] referenceString, int frameCount)
    {
        ArgumentNullException.ThrowIfNull(referenceString);

        if (frameCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(frameCount));

        var result = new SimulationResult
        {
            AlgorithmName = Name,
            FrameCount = frameCount,
            ReferenceString = referenceString.ToArray()
        };

        var frames = new List<int>();
        var queue = new Queue<int>();

        for (int i = 0; i < referenceString.Length; i++)
        {
            int page = referenceString[i];
            bool isFault = !frames.Contains(page);
            int? victimPage = null;

            if (isFault)
            {
                if (frames.Count < frameCount)
                {
                    frames.Add(page);
                    queue.Enqueue(page);
                }
                else
                {
                    victimPage = queue.Dequeue();
                    int index = frames.IndexOf(victimPage.Value);
                    frames[index] = page;
                    queue.Enqueue(page);
                }
            }

            result.Steps.Add(new StepResult
            {
                StepNumber = i + 1,
                PageNumber = page,
                IsFault = isFault,
                FrameState = string.Join(",", frames),
                VictimPage = victimPage
            });
        }

        return result;
    }
}