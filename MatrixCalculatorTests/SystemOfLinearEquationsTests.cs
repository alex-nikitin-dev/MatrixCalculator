using LinearAlgebra;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixCalculatorTests
{
    [TestClass()]
    public class SystemOfLinearEquationsTests
    {
        [TestMethod()]
        public void GetRootsTest()
        {
            decimal[,] matrix = { { 2, -2, 1, -3 }, { 1, 3, -2, 1 }, { 3, -1, -1, 2 } };
            var matrixObj = new Matrix(matrix);
            var sole = SystemOfLinearEquations.ParseMatrix(matrixObj);
            var rightResult = Vector.MakeVector(new Matrix(new decimal[,] { { -7m / 5 }, { -2 }, { -21m / 5 } }));
            var result = sole.GetRoots();
            if (!result.IsEqual(rightResult,1E-10m))
                Assert.Fail();
        }
    }
}