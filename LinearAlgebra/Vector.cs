using System;

namespace LinearAlgebra
{
    public class Vector : Matrix
    {
        public Vector(string name, int countRows)
            : base(name, countRows, 1)
        {
        }
        public Vector(Vector preVector) :
            base(preVector)
        {
        }
        public static Vector MakeVector(Matrix matrix)
        {
            Vector result;
            if (matrix.CountColumns == 1)
            {
                result = new Vector(matrix.Name, matrix.CountRows);
                for (int i = 0; i < matrix.CountRows; i++)
                {
                    result[i] = matrix[i, 0];
                }
            }
            else
            {
                throw new ArgumentException("Unable to convert the matrix to a vector. The count of columns is greater than 1.");
            }
            return result;
        }
        protected new int CountRows
        {
            get => base.CountRows;
        }
        protected new int CountColumns
        {
            get => base.CountColumns;
        }

        public int Size
        {
            get => CountRows;
        }
        public decimal this[int i]
        {
            get => base[i, 0];
            set => base[i, 0] = value;
        }
        public decimal ScalarProduct(Vector vector)
        {
            if(CountRows != vector.CountRows) throw new ArgumentException("Невозможно получить скалярное произведение векторов: не совпадает количество координат");
            decimal result = 0;
            for (int i = 0; i < CountRows; i++)
            {
                result += this[i] * vector[i];
            }

            return result;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return MakeVector((a + (b as Matrix)));
        }
        public static Vector operator *(Vector a, decimal b)
        {
            return MakeVector((a as Matrix) * b);
        }
        public static Vector operator *(Matrix a, Vector b)
        {
            return MakeVector(a * (b as Matrix));
        }
        public bool IsEqual(Vector vector,decimal eps)
        {
            if (Size != vector.Size)
                return false;
            for (int i = 0; i < Size; i++)
            {
                    if (Math.Abs(this[i] - vector[i]) >= eps)
                        return false;
            }
            return true;
        }
        public double GetModule()
        {
            decimal result = 0;
            for (int i = 0; i < CountRows; i++)
            {
                result += this[i] * this[i];
            }

            return Math.Sqrt((double)result);
        }
    }
}
