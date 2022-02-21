using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNNET.LayerN.Activation
{
    public class LeakyReLu : ActivationB
    {
        public LeakyReLu() : base("leaky")
        {

        }

        public override void forward(NDimensionArray x)
        {
            base.forward(x);
            /// A compléter
        }

    }
}