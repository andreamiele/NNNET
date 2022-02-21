using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NNNET
{
    public class NDimensionArray
    {
        public double[] data
        {
            get
            {
                return var;
            }
        }
        private double[] var; // hold data 
        public int[] shape { get; set; } // shape of the dataset (1D,2D or 3D)

        public int elmts
        {
            get
            {
                return shape.Aggregate((x, y) => x * y);
            }
        } // Nber of elmts in the array

        public NDimensionArray(params int[] shape)
        {
            this.shape = shape;
            var = new double[elmts];
        } // Constructor for NDimensionArray with shape as parameter

        public void Loader(params double[] datas)
        {
            var = datas;
        } // Load datas in a NDimensionArray
        public void Filler(double x)
        {
            for (int i = 0; i < this.elmts; i++)
            {
                var[i] = x;
            }
        } // Filler the NDimensionArray with a constant, x

        public double this[params int[] indices]
        {
            get
            {
                var st = GetSt();
                long j = 0;
                for (int i = 0; i < indices.Length; i++)
                {
                    j += indices[i] * st[i];
                }
                return var[j];
            }
            set
            {
                var st = GetSt();
                long j = 0;
                for (int i = 0; i < indices.Length; i++)
                {
                    j += indices[i] * st[i];
                }
                var[j] = value;
            }
        }

        public int[] GetSt()
        {
            int f = 1;
            var st = new int[shape.Length];
            for (int i = shape.Length - 1; i >= 0; --i)
            {
                st[i] = f;
                f *= shape[i];
            }
            return st;
        }

        public void Print(string title = "")
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine(title);
            }

            Console.WriteLine("---------{0}----------", string.Join(" x ", shape));
            for (int i = 0; i < shape[0]; i++)
            {
                for (int j = 0; j < shape[1]; j++)
                {
                    Console.Write(Math.Round(this[i, j], 2) + "  ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("-----------------------\n\n");
        }



        public static NDimensionArray operator +(NDimensionArray x, NDimensionArray y)
        {
            NDimensionArray z = new NDimensionArray(x.shape);

            for (int i = 0; i < x.elmts; i++)
            {
                z[i] = x[i] + y[i];
            }
            return z;
        } // Addition of two NDimensionArray
        public static NDimensionArray operator +(NDimensionArray a, double b)
        {
            NDimensionArray t_b = new NDimensionArray(a.shape);
            t_b.Filler(b);
            return a + t_b;
        }

        public static NDimensionArray operator +(double a, NDimensionArray b)
        {
            NDimensionArray t_a = new NDimensionArray(b.shape);
            t_a.Filler(a);
            return t_a + b;
        }

        public static NDimensionArray operator -(NDimensionArray x, NDimensionArray y)
        {
            NDimensionArray z = new NDimensionArray(x.shape);

            for (int i = 0; i < x.elmts; i++)
            {
                z[i] = x[i] - y[i];
            }
            return z;
        } // Substraction of two NDimensionArray
        public static NDimensionArray operator -(NDimensionArray a, double b)
        {
            NDimensionArray t_b = new NDimensionArray(a.shape);
            t_b.Filler(b);
            return a - t_b;
        }

        public static NDimensionArray operator -(double a, NDimensionArray b)
        {
            NDimensionArray t_a = new NDimensionArray(b.shape);
            t_a.Filler(a);
            return t_a - b;
        }

        public static NDimensionArray operator -(NDimensionArray a)
        {
            NDimensionArray r = new NDimensionArray(a.shape);

            for (int i = 0; i < a.elmts; i++)
            {
                r[i] = -a[i];
            }

            return r;
        }

        public static NDimensionArray operator *(NDimensionArray x, NDimensionArray y)
        {
            NDimensionArray z = new NDimensionArray(x.shape);

            for (int i = 0; i < x.elmts; i++)
            {
                z[i] = x[i] * y[i];
            }
            return z;
        } // Multiplication of two NDimensionArray
        public static NDimensionArray operator *(NDimensionArray a, double b)
        {
            NDimensionArray t_b = new NDimensionArray(a.shape);
            t_b.Filler(b);
            return a * t_b;
        }

        public static NDimensionArray operator *(double a, NDimensionArray b)
        {
            NDimensionArray t_a = new NDimensionArray(b.shape);
            t_a.Filler(a);
            return t_a * b;
        }

        public static NDimensionArray operator /(NDimensionArray x, NDimensionArray y)
        {
            NDimensionArray z = new NDimensionArray(x.shape);

            for (int i = 0; i < x.elmts; i++)
            {
                z[i] = x[i] / y[i];
            }
            return z;
        } // Division of two NDimensionArray
        public static NDimensionArray operator /(NDimensionArray a, double b)
        {
            NDimensionArray t_b = new NDimensionArray(a.shape);
            t_b.Filler(b);
            return a / t_b;
        }

        public static NDimensionArray operator /(double a, NDimensionArray b)
        {
            NDimensionArray t_a = new NDimensionArray(b.shape);
            t_a.Filler(a);
            return t_a / b;
        }

        public static NDimensionArray operator ==(NDimensionArray a, NDimensionArray b)
        {
            NDimensionArray r = new NDimensionArray(a.shape);

            for (int i = 0; i < a.elmts; i++)
            {
                r[i] = a[i] == b[i] ? 1 : 0;
            }

            return r;
        }

        public static NDimensionArray operator ==(NDimensionArray a, double b)
        {
            NDimensionArray t_b = new NDimensionArray(a.shape);
            t_b.Filler(b);
            return a == t_b;
        }

        public static NDimensionArray operator ==(double a, NDimensionArray b)
        {
            NDimensionArray t_a = new NDimensionArray(b.shape);
            t_a.Filler(a);
            return t_a == b;
        }
        public static NDimensionArray operator !=(NDimensionArray a, NDimensionArray b)
        {
            NDimensionArray r = new NDimensionArray(a.shape);

            for (int i = 0; i < a.elmts; i++)
            {
                r[i] = a[i] != b[i] ? 1 : 0;
            }

            return r;
        }

        public static NDimensionArray operator !=(NDimensionArray a, double b)
        {
            NDimensionArray t_b = new NDimensionArray(a.shape);
            t_b.Filler(b);
            return a != t_b;
        }

        public static NDimensionArray operator !=(double a, NDimensionArray b)
        {
            NDimensionArray t_a = new NDimensionArray(b.shape);
            t_a.Filler(a);
            return t_a != b;
        }
        public static NDimensionArray operator >(NDimensionArray a, NDimensionArray b)
        {
            NDimensionArray r = new NDimensionArray(a.shape);

            for (int i = 0; i < a.elmts; i++)
            {
                r[i] = a[i] > b[i] ? 1 : 0;
            }

            return r;
        }

        public static NDimensionArray operator >(NDimensionArray a, double b)
        {
            NDimensionArray t_b = new NDimensionArray(a.shape);
            t_b.Filler(b);
            return a > t_b;
        }

        public static NDimensionArray operator >(double a, NDimensionArray b)
        {
            NDimensionArray t_a = new NDimensionArray(b.shape);
            t_a.Filler(a);
            return t_a > b;
        }


        public static NDimensionArray operator >=(NDimensionArray a, NDimensionArray b)
        {
            NDimensionArray r = new NDimensionArray(a.shape);

            for (int i = 0; i < a.elmts; i++)
            {
                r[i] = a[i] >= b[i] ? 1 : 0;
            }

            return r;
        }

        public static NDimensionArray operator >=(NDimensionArray a, double b)
        {
            NDimensionArray t_b = new NDimensionArray(a.shape);
            t_b.Filler(b);
            return a >= t_b;
        }

        public static NDimensionArray operator >=(double a, NDimensionArray b)
        {
            NDimensionArray t_a = new NDimensionArray(b.shape);
            t_a.Filler(a);
            return t_a >= b;
        }


        public static NDimensionArray operator <(NDimensionArray a, NDimensionArray b)
        {
            NDimensionArray r = new NDimensionArray(a.shape);

            for (int i = 0; i < a.elmts; i++)
            {
                r[i] = a[i] < b[i] ? 1 : 0;
            }

            return r;
        }

        public static NDimensionArray operator <(NDimensionArray a, double b)
        {
            NDimensionArray t_b = new NDimensionArray(a.shape);
            t_b.Filler(b);
            return a < t_b;
        }

        public static NDimensionArray operator <(double a, NDimensionArray b)
        {
            NDimensionArray t_a = new NDimensionArray(b.shape);
            t_a.Filler(a);
            return t_a < b;
        }


        public static NDimensionArray operator <=(NDimensionArray a, NDimensionArray b)
        {
            NDimensionArray r = new NDimensionArray(a.shape);

            for (int i = 0; i < a.elmts; i++)
            {
                r[i] = a[i] >= b[i] ? 1 : 0;
            }

            return r;
        }

        public static NDimensionArray operator <=(NDimensionArray a, double b)
        {
            NDimensionArray t_b = new NDimensionArray(a.shape);
            t_b.Filler(b);
            return a <= t_b;
        }

        public static NDimensionArray operator <=(double a, NDimensionArray b)
        {
            NDimensionArray t_a = new NDimensionArray(b.shape);
            t_a.Filler(a);
            return t_a <= b;
        }

        public NDimensionArray Transpose()
        {
            Op op = new Op();
            return op.Transpose(this);
        }
        public bool Next(int start, int count)
        {
            start = shape[1] * start;
            count = shape[1] * count;
            if (start >= var.Length)
            {
                return false;
            }

            return true;
        }
        public NDimensionArray Slice(int start, int count)
        {
            start = shape[1] * start;
            count = shape[1] * count;

            var slicedData = var.Skip(start).Take(count).ToArray();

            NDimensionArray result = new NDimensionArray((slicedData.Length / shape[1]), shape[1]);
            result.Loader(slicedData);
            return result;
        }
    }
}