using NNNET.LayerN;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNNET.Costs
{
    public class MSE : Cost
    {
        public MSE() : base("mse")
        {

        }
        public override NDimensionArray forward(NDimensionArray x, NDimensionArray l)
        {
            var error = x - l;
            return Mean(Square(error));
        }
        public override NDimensionArray backward(NDimensionArray x, NDimensionArray l)
        {
            double n = 2 / x.shape[0];
            return n * (x - l);
        }
    }
}