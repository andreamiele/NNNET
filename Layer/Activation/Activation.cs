using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNNET.LayerN.Activation
{
    public class ActivationB : Layer
    {
        public ActivationB(string n) : base(n)
        {

        }

        public static ActivationB Get(string name)
        {
            ActivationB activation = null;
            switch (name)
            {
                case "ReLu":
                    activation = new ReLu();
                    break;
                case "sigmoid":
                    activation = new Sigmoid();
                    break;
                case "leaky":
                    activation = new LeakyReLu();
                    break;
                default:
                    break;
            }
            return activation;
        }

    }

}