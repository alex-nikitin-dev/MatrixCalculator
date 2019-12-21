using System;
using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixCalculatorTests
{
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void GetGaussTest()
        {
            var m = new Matrix("test", 2, 2);
            int nextItem = 0;
            for (int i = 0; i < m.CountRows; i++)
            {
                for (int j = 0; j < m.CountColumns; j++)
                {
                    m[i, j] = nextItem++;
                }
            }
            if (Math.Abs(m.Determinant() - (-2)) != decimal.Zero)
                Assert.Fail();
        }

        public bool IsExistInverseMatrixByModTest(decimal[,] array,decimal[,] resultArray,uint mod)
        {
            try
            {
                var m = new Matrix(array);
                var result = m.GetInverseMatrixByMod(mod);
                var rightResult = new Matrix(resultArray);
                return result.IsEqual(rightResult, (decimal)1E-5);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        [TestMethod()]
        public void GetInverseMatrixByModTest()
        {
            if (!IsExistInverseMatrixByModTest(new decimal[,] { { 15, 41, 29 }, { 20, 40, 17 }, { 7, 32, 23 } },
                new decimal[,] { { 0, 45, 26 }, { 36, 44, 12 }, { 1, 15, 2 } },
                47)) Assert.Fail();
            if (!IsExistInverseMatrixByModTest(new decimal[,] { { 25, 5, 12 }, { 13, 16, 10 }, { 20, 17, 15 } },
                new decimal[,] { { 22, 19, 20 }, { 9, 9, 18 }, { 9, 13, 5 } },
                26)) Assert.Fail();
            if (IsExistInverseMatrixByModTest(new decimal[,] { { 25, 5 }, { 13, 16 } },
                new decimal[,] { { } },
                35)) Assert.Fail();
            if (!IsExistInverseMatrixByModTest(new decimal[,] { { 6, 24, 1, 18 }, { 13, 16, 10, 5 }, { 20, 17, 15, 0 }, { 1, 3, 8, 16 } },
                new decimal[,] { { 16, 8, 2, 8 }, { 11, 9, 2, 5 }, { 8, 7, 4, 9 }, { 6, 5, 7, 6 } },
                19)) Assert.Fail();

            if (!IsExistInverseMatrixByModTest(new decimal[,] { 
                    { 357 ,   724, 137  ,374 ,  481, 220 }, 
                    { 53  ,  757 ,674  ,839  ,  878, 988 }, 
                    { 52   , 615 ,973,  140 ,   277, 910 }, 
                    { 994  ,  393, 825,  568,    483, 795 }, 
                    { 335 ,  230 ,102 , 767 ,   424 ,851 },
                    { 868  ,  390 ,402 ,1046,    475,  41 } },
                new decimal[,] { 
                    { 781 ,  413, 206, 832, 222, 149 },
                    { 524 ,  11 , 167 ,66  ,833 ,435 },
                    { 206  , 745 ,166 ,641, 570, 113 }, 
                    { 892 ,   76 , 677 ,587 ,576 ,391 }, 
                    { 35 ,  829, 293 ,232, 347 ,185 }, 
                    { 671 ,  963 ,723 ,183 ,524, 450} },
                1051)) Assert.Fail();
            //Assert.Fail();

            if (!IsExistInverseMatrixByModTest(new decimal[,] {{26, 52}, {86, 44}}, new decimal[,] {{130, 55}, {38, 49}},153)) Assert.Fail();
          //  if (IsExistInverseMatrixByModTest(new decimal[,] { { 32, 40 }, { 52, 124 } }, new decimal[,] { { 57, 153 }, { 68, 47 } },154)) Assert.Fail();
        }
    }
}