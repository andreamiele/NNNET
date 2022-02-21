using NNNET.LayerN;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNNET.Costs
{
    public class BCE : Cost
    {
        public BCE() : base("bce")
        {

        }
        public override NDimensionArray forward(NDimensionArray x, NDimensionArray l)
        {
            var output = Clip(x, double.Epsilon, 1 - double.Epsilon);
            output = Mean(-(l * (Log(output) + (1 - l) * Log(1 - output))));
            return output;
        }
        public override NDimensionArray backward(NDimensionArray x, NDimensionArray l)
        {
            return (x - l) / (x * (1 - x));
        }
    }
}