using System;
using System.Collections.Generic;
using System.Text;

namespace NNNET.Performance
{
    public abstract class Metric : Op
    {
        public string name { get; set; }

        public Metric(string name)
        {
            this.name = name;
        }

        public abstract NDimensionArray Calculate(NDimensionArray x, NDimensionArray l);
    }
}