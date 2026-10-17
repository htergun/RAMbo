using System;
using System.Collections.Generic;
using RAMbo.App.Models;
using RAMbo.App.Services.Interfaces;

namespace RAMbo.App.Services.Algorithms;


public class LruAlgorithm : IPageReplacementAlgorithm
{
    public string Name => "LRU";

    public SimulationResult Run(int[] referenceString, int frameCount)
    {
        var result = new SimulationResult
        {
            AlgorithmName = this.Name,
            FrameCount = frameCount,
            ReferenceString = referenceString
        };

        List<int> frames = new List<int>(frameCount);
        int stepNumber = 1;

        foreach (int page in referenceString)
        {
            bool isFault = false;
            int? victimPage = null;

            if (frames.Contains(page))
            {
                frames.Remove(page);
                frames.Add(page);
            }
            else
            {
                isFault = true;

                if (frames.Count == frameCount)
                {
                    victimPage = frames[0];
                    frames.RemoveAt(0);
                }

                frames.Add(page);
            }

            string frameState = string.Join(",", frames);

            result.Steps.Add(new StepResult
            {
                StepNumber = stepNumber,
                PageNumber = page,
                IsFault = isFault,
                FrameState = frameState,
                VictimPage = victimPage
            });

            stepNumber++;
        }

        return result;
    }
}