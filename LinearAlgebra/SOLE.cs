using System;

namespace LinearAlgebra
{
    public class SystemOfLinearEquations:Matrix
    {
        public SystemOfLinearEquations(string name, int countRows) : 
            base(name, countRows, countRows, true)
        {
        }

        public SystemOfLinearEquations(SystemOfLinearEquations preMatrix) : base(preMatrix)
        {
        }
        /// <summary>
        /// Parse matrix
        /// </summary>
        /// <param name="inMatrix">enter matrix must be n*(n+1) where the last column is a vector of constants</param>
        /// <returns></returns>
        public static SystemOfLinearEquations ParseMatrix(Matrix inMatrix)
        {
            if(inMatrix.CountRows != inMatrix.CountColumns - 1) 
                throw new ArgumentException("enter matrix must be n*(n+1) where the last column is a vector of constants");
            var result = new SystemOfLinearEquations(inMatrix.Name + "_sole", inMatrix.CountRows);
            for (int i = 0; i < result.CountRows; i++)
            {
                for (int j = 0; j < result.CountRows; j++)
                {
                    result[i, j] = inMatrix[i, j];
                }
            }

            int constatsIndex = inMatrix.CountColumns - 1;
            for (int i = 0; i < result.CountRows; i++)
            {
                result.ConstantsVector[i] = inMatrix[i, constatsIndex];
            }
            return result;
        }

        public Vector GetRoots()
        {
            Vector result = new Vector(Name + "_results", CountRows);
            var gaussed =  GetGauss(false);
            int n = gaussed.CountRows;
            if (gaussed[n-1, n-1] == decimal.Zero)
            {
                if (gaussed.ConstantsVector[n-1] == decimal.Zero)
                {
                    throw new ArgumentException("There are infinite set of solutions");
                }

                throw  new ArgumentException("There are no solutions");
            }

            for (int i = n - 1; i >= 0; i--)
            {
                decimal summ = 0;

                for (int j = i + 1; j < n; j++)
                {
                    summ += gaussed[i, j] * result[j];
                }

                result[i] = (gaussed.ConstantsVector[i] - summ) / gaussed[i, i];
            }

            return result;
        }
    }
}
