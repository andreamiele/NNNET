using NNNET.LayerN;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNNET.Costs
{
    public abstract class Cost : Op
    {
        public string name { get; set; }
        public Cost(string name)
        {
            this.name = name;
        }
        public abstract NDimensionArray forward(NDimensionArray x, NDimensionArray l);
        public abstract NDimensionArray backward(NDimensionArray x, NDimensionArray l);
    }
}