using System;
using System.Collections.Generic;
using System.Text;

namespace NNNET.Performance
{
    public abstract class BA : Metric
    {


        public BA(string name) : base("ba")
        {

        }

        public override NDimensionArray Calculate(NDimensionArray x, NDimensionArray l)
        {
            var output = Round(Clip(x, 0, 1));
            return Mean(x == l);
        }
    }
}