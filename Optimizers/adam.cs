using System;
using System.Collections.Generic;
using System.Text;
using NNNET.LayerN;
using System.Linq;

namespace NNNET.Optimizers
{
    /// <summary>
    /// Adam is an optimization algorithm that can used instead of the classical stochastic gradient descent procedure to update network weights iterative based in training data.
    /// <para>
    /// Adam was presented by Diederik Kingma from OpenAI and Jimmy Ba from the University of Toronto in their 2015 ICLR paper(poster) titled “Adam: A Method for Stochastic Optimization“. 
    /// I will quote liberally from their paper in this post, unless stated otherwise.
    /// </para>
    /// </summary>
    public class adam : OptimizerB
    {
        /// <summary>
        /// Gets or sets the beta 1 value.
        /// </summary>
        /// <value>
        /// The beta1.
        /// </value>
        public double Beta1 { get; set; }

        /// <summary>
        /// Gets or sets the beta 2 value.
        /// </summary>
        /// <value>
        /// The beta2.
        /// </value>
        public double Beta2 { get; set; }

        private Dictionary<string, NDimensionArray> ms;
        private Dictionary<string, NDimensionArray> vs;

        public adam(double lr = 0.01, double beta_1 = 0.9, double beta_2 = 0.999, double decayRate = 0) : base(lr, "adam")
        {
            Beta1 = beta_1;
            Beta2 = beta_2;
            DecayRate = decayRate;
            ms = new Dictionary<string, NDimensionArray>();
            vs = new Dictionary<string, NDimensionArray>();
        }

        public override void Update(int iteration, Layer layer)
        {
            //If Decay rate is more than 0, the correct the learnng rate per iteration.
            if (DecayRate > 0)
            {
                LearningRate = LearningRate * (1 / (1 + DecayRate * iteration));
            }

            //Loop through all the parameters in the layer
            foreach (var p in layer.parameters.ToList())
            {
                //Get the parameter name
                string paramName = p.Key;

                //Create a unique name to store in the dictionary
                string varName = layer.name + "_" + p.Key;

                //Get the weight values
                NDimensionArray param = p.Value;

                //Get the gradient/partial derivative values
                NDimensionArray grad = layer.grads[paramName];

                //If this is first time, initlalise all the moving average values with 0
                if (!ms.ContainsKey(varName))
                {
                    var ms_new = new NDimensionArray(param.shape);
                    ms_new.Filler(0);
                    ms[varName] = ms_new;
                }

                //If this is first time, initlalise all the moving average values with 0
                if (!vs.ContainsKey(varName))
                {
                    var vs_new = new NDimensionArray(param.shape);
                    vs_new.Filler(0);
                    vs[varName] = vs_new;
                }

                // Calculate the exponential moving average for Beta 1 against the gradient
                ms[varName] = (Beta1 * ms[varName]) + (1 - Beta1) * grad;

                //Calculate the exponential squared moving average for Beta 2 against the gradient
                vs[varName] = (Beta2 * vs[varName]) + (1 - Beta2) * Square(grad);

                //Correct the moving averages
                var m_cap = ms[varName] / (1 - Math.Pow(Beta1, iteration));
                var v_cap = vs[varName] / (1 - Math.Pow(Beta2, iteration));

                //Update the weight of of the neurons
                layer.parameters[paramName] = param - (LearningRate * m_cap / (Sqrt(v_cap) + double.Epsilon));
            }
        }
    }
}