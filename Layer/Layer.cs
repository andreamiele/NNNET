
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNNET.LayerN
{
    public abstract class Layer : Op
    {
        public NDimensionArray inGrad { get; set; }
        public Dictionary<string, NDimensionArray> grads { get; set; }
        public string name { get; set; }
        public NDimensionArray input { get; set; }
        public NDimensionArray output { get; set; }
        public Dictionary<string, NDimensionArray> parameters
        {
            get;
            set;
        }
        public Layer(string n)
        {
            this.name = n;
            this.parameters = new Dictionary<string, NDimensionArray>();

        }
        public virtual void forward(NDimensionArray x)
        {
            input = x;
        }
        public virtual void backward(NDimensionArray grad) { }

        public void Print(bool printGrads = true)
        {
            foreach (var item in parameters)
            {
                item.Value.Print(string.Format("Parameter: {0}", item.Key));
                if (printGrads && grads.ContainsKey(item.Key))
                {
                    grads[item.Key].Print(string.Format("Grad: {0}", item.Key));
                }
            }
        }

    }

}