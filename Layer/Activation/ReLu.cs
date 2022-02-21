using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNNET.LayerN.Activation
{
    public class ReLu : ActivationB
    {
        public ReLu() : base("ReLu")
        {

        }

        public override void forward(NDimensionArray x)
        {
            base.forward(x);
            NDimensionArray matches = x > 0;
            output = matches * x;
        }
        public override void backward(NDimensionArray grad)
        {
            inGrad = grad * (input > 0);
        }
    }
}
