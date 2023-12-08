﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ega_console_app.ParentSelectors
{
    public class GenotypicInbreedingParentsSelector : GenotypicBreedingParentsSelector
    {
        public GenotypicInbreedingParentsSelector() : base()
        {
        }   

        protected override void GetBestPair(int countDiff, Encoding probablePair, ref int currentBestDiff, ref Encoding currentBest)
        {
            if(currentBestDiff == 0 || countDiff < currentBestDiff)
            {
                currentBestDiff = countDiff;
                currentBest = probablePair;
            }
        }
    }
}
