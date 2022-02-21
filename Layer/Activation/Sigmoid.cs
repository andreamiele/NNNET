using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNNET.LayerN.Activation
{
    public class Sigmoid : ActivationB
    {
        public Sigmoid() : base("sigmoid")
        {

        }

        public override void forward(NDimensionArray x)
        {
            base.forward(x);
            output = Exp(x) / (1 + Exp(x));
        }
        public override void backward(NDimensionArray grad)
        {
            inGrad = grad * output * (1 - output);
        }
    }
}
