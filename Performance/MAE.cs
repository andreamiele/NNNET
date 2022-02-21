using System;
using System.Collections.Generic;
using System.Text;

namespace NNNET.Performance
{
    public abstract class MAE : Metric
    {


        public MAE(string name) : base("mae")
        {

        }

        public override NDimensionArray Calculate(NDimensionArray x, NDimensionArray l)
        {
            var error = x - l;
            return Mean(Abs(error));
        }
    }
}