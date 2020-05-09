using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vocalyz.Analysis;

namespace Vocalyz
{
    public static class Extensions
    {
      
        public static SampleAnalysis[] FilterByFrequency(this SampleAnalysis[] analysis, double maxFrequency)
        {
            return analysis.TakeWhile(x => x.Frequency <= maxFrequency).ToArray();
        }
    }
}
