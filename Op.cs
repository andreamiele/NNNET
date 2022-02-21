using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNNET
{
    public class Op
    {
        public NDimensionArray Random(params int[] shape)
        {
            Random r = new Random();
            NDimensionArray n = new NDimensionArray(shape);
            for (int i = 0; i < n.elmts; i++)
            {
                n[i] = r.NextDouble();
            }
            return n;
        } // Tensor with random datas

        public NDimensionArray Dot(NDimensionArray x, NDimensionArray y)
        {
            if (x.shape[1] != y.shape[0])
            {
                throw new Exception("Cannot proceed operations due to dimensions problems");
            }
            int m = x.shape[0];
            int q = y.shape[1];
            int n = x.shape[1];
            NDimensionArray z = new NDimensionArray(m, q);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    z[i, j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        z[i, j] += x[i, k] * y[k, j];
                    }
                }
            }
            return z;
        }

        public NDimensionArray Log(NDimensionArray a)
        {
            NDimensionArray res = new NDimensionArray(a.shape);
            for (int i = 0; i < a.elmts; i++)
            {
                res[i] = Math.Log(a[i]);
            }
            return res;
        }

        public NDimensionArray Exp(NDimensionArray a)
        {
            NDimensionArray res = new NDimensionArray(a.shape);
            for (int i = 0; i < a.elmts; i++)
            {
                res[i] = Math.Exp(a[i]);
            }
            return res;
        }
        public NDimensionArray Sqrt(NDimensionArray a)
        {
            NDimensionArray res = new NDimensionArray(a.shape);
            for (int i = 0; i < a.elmts; i++)
            {
                res[i] = Math.Sqrt(a[i]);
            }
            return res;
        }

        public NDimensionArray Square(NDimensionArray a)
        {
            NDimensionArray res = new NDimensionArray(a.shape);
            for (int i = 0; i < a.elmts; i++)
            {
                res[i] = a[i] * a[i];
            }
            return res;
        }

        // TODO : Add Pow(x,y) where x and y are tensors

        public NDimensionArray Transpose(NDimensionArray a)
        {
            NDimensionArray res = new NDimensionArray(a.shape);
            for (int i = 0; i < a.shape[0]; i++)
            {
                for (int j = 0; j < a.shape[0]; j++)
                {
                    res[i, j] = a[j, i];
                }
            }
            return res;
        }
        public NDimensionArray Clip(NDimensionArray x, double min, double max)
        {
            NDimensionArray result = new NDimensionArray(x.shape);
            for (int i = 0; i < x.elmts; i++)
            {
                result[i] = (x[i] < min) ? min : (x[i] > max) ? max : x[i];
            }

            return result;
        }


        public NDimensionArray Round(NDimensionArray x)
        {
            NDimensionArray result = new NDimensionArray(x.shape);
            for (int i = 0; i < x.elmts; i++)
            {
                result[i] = Math.Round(x[i]);
            }

            return result;
        }

        public NDimensionArray Abs(NDimensionArray x)
        {
            NDimensionArray result = new NDimensionArray(x.shape);
            for (int i = 0; i < x.elmts; i++)
            {
                result[i] = Math.Abs(x[i]);
            }

            return result;
        }


        public NDimensionArray Mean(NDimensionArray x, uint? axis = null)
        {
            NDimensionArray result = null;

            if (axis.HasValue)
            {
                List<double> meanValues = new List<double>();
                if (axis.Value == 1)
                {
                    result = new NDimensionArray(x.shape[0], 1);
                    for (int i = 0; i < x.shape[0]; i++)
                    {
                        double total = 0;
                        for (int j = 0; j < x.shape[1]; j++)
                        {
                            total += x[i, j];
                        }

                        meanValues.Add(total / x.shape[1]);
                    }

                }
                else if (axis.Value == 0)
                {
                    result = new NDimensionArray(1, x.shape[1]);
                    for (int i = 0; i < x.shape[1]; i++)
                    {
                        double total = 0;
                        for (int j = 0; j < x.shape[0]; j++)
                        {
                            total += x[i, j];
                        }

                        meanValues.Add(total / x.shape[0]);
                    }
                }



                result.Loader(meanValues.ToArray());
            }
            else
            {
                result = new NDimensionArray(1, 1);
                result.Loader(x.data.Average());
            }

            return result;
        }

    }

}