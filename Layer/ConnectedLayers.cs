using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NNNET.LayerN.Activation;

namespace NNNET.LayerN
{
    public class Connected : Layer
    {

        public Connected(int n, int np, string npp) : base("f")
        {
            parameters["weight"] = Random(n, np);
            NbInput = n;
            NbOutput = np;
            activation = ActivationB.Get(npp);
        }
        public int NbInput { get; set; }
        public int NbOutput { get; set; }
        public ActivationB activation { get; set; }

        public override void forward(NDimensionArray x)
        {
            base.forward(x);
            output = Dot(x, parameters["weight"]);
            if (activation != null)
            {
                activation.forward(output);
                output = activation.output;
            }
        }

        public override void backward(NDimensionArray grad)
        {
            if (activation != null)
            {
                activation.backward(grad);
                grad = activation.inGrad;
            }
            inGrad = Dot(grad, parameters["weight"].Transpose());
            grads["weight"] = Dot(input.Transpose(), grad);
        }

    }
}