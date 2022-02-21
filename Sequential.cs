using NNNET.Costs;
using NNNET.LayerN;
using NNNET.Performance;
using NNNET.Optimizers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNNET
{

    public class Sequential
    {
        public event EventHandler<EpochEndEventArgs> BatchEnd;

        public List<Layer> Layers { get; set; }

        public OptimizerB Optimizer { get; set; }


        public Cost Cost { get; set; }

        public Metric Metric { get; set; }

        public List<double> TrainingLoss { get; set; }


        public List<double> TrainingMetrics { get; set; }


        public Sequential(OptimizerB optimizer, Cost cost, Metric metric = null)
        {
            Layers = new List<Layer>();
            TrainingLoss = new List<double>();
            TrainingMetrics = new List<double>();

            this.Optimizer = optimizer != null ? optimizer : throw new Exception("Need optimizer");
            this.Cost = cost != null ? cost : throw new Exception("Need cost");
            Metric = metric;
        }
        public void Add(Layer layer)
        {
            Layers.Add(layer);
        }


        public void Train(NDimensionArray x, NDimensionArray y, int numIterations, int batchSize)
        {
            //Initialise bacch loss and metric list for temporary holding of result
            List<double> batchLoss = new List<double>();
            List<double> batchMetrics = new List<double>();

            //Loop through till the end of specified iterations
            for (int i = 1; i <= numIterations; i++)
            {
                //Initialize local variables
                int currentIndex = 0;
                batchLoss.Clear();
                batchMetrics.Clear();

                //Loop untill the data is exhauted for every batch selected
                while (x.Next(currentIndex, batchSize))
                {
                    //Get the batch data based on the specified batch size
                    var xtrain = x.Slice(currentIndex, batchSize);
                    var ytrain = y.Slice(currentIndex, batchSize);

                    //Run forward for all the layers to predict the value for the training set
                    var ypred = Forward(xtrain);

                    //Find the loss/cost value for the prediction wrt expected result
                    var costVal = Cost.forward(ypred, ytrain);
                    batchLoss.AddRange(costVal.data);

                    //Find the metric value for the prediction wrt expected result
                    if (Metric != null)
                    {
                        var metric = Metric.Calculate(ypred, ytrain);
                        batchMetrics.AddRange(metric.data);
                    }

                    //Get the gradient of the cost function which is the passed to the layers during back-propagation
                    var grad = Cost.backward(ypred, ytrain);

                    //Run back-propagation accross all the layers
                    Backward(grad);

                    //Now time to update the neural network weights using the specified optimizer function
                    foreach (var layer in Layers)
                    {
                        Optimizer.Update(i, layer);
                    }

                    currentIndex = currentIndex + batchSize; ;
                }

                //Collect the result and fire the event
                double batchLossAvg = Math.Round(batchLoss.Average(), 2);

                double batchMetricAvg = Metric != null ? Math.Round(batchMetrics.Average(), 2) : 0;

                TrainingLoss.Add(batchLossAvg);

                if (batchMetrics.Count > 0)
                    TrainingMetrics.Add(batchMetricAvg);

                EpochEndEventArgs eventArgs = new EpochEndEventArgs(i, batchLossAvg, batchMetricAvg);
                BatchEnd?.Invoke(i, eventArgs);
            }
        }


        public NDimensionArray Predict(NDimensionArray x)
        {
            return Forward(x);
        }


        private NDimensionArray Forward(NDimensionArray x)
        {
            Layer lastLayer = null;

            foreach (var layer in Layers)
            {
                if (lastLayer == null)
                    layer.forward(x);
                else
                    layer.forward(lastLayer.output);

                lastLayer = layer;
            }

            return lastLayer.output;
        }

        private void Backward(NDimensionArray gradOutput)
        {
            var curGradOutput = gradOutput;
            for (int i = Layers.Count - 1; i >= 0; --i)
            {
                var layer = Layers[i];

                layer.backward(curGradOutput);
                curGradOutput = layer.inGrad;
            }
        }
    }

    public class EpochEndEventArgs
    {
        public EpochEndEventArgs(
            int epoch,
            double loss,
            double metric)
        {
            Epoch = epoch;
            Loss = loss;
            Metric = metric;
        }


        public int Epoch { get; }


        public double Loss { get; }

        public double Metric { get; }
    }
}