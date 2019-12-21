using System;

namespace LinearAlgebra
{
    public class Matrix
    {
        #region .ctor

        readonly decimal[,] _matrix;
        readonly Vector _constantsVector;
        public Matrix(string name, int countRows, int countCols,bool needConstantsVector=false)
        {
            Name = name;
            _matrix = new decimal[countRows, countCols];
            CountColumns = countCols;
            CountRows = countRows;
            AllItemsCount = countCols * countRows;
            if(needConstantsVector) _constantsVector = new Vector(name + "_constantVector", countRows);
        }

        public Matrix(Matrix preMatrix):
            this(preMatrix.Name+"_copy",preMatrix.CountRows,preMatrix.CountColumns,preMatrix.IsConstantVectorExist)
        {
            for (int i = 0; i < CountRows; i++)
            {
                for (int j = 0; j < CountColumns; j++)
                {
                    this[i, j] = preMatrix[i, j];
                }

                if (IsConstantVectorExist) ConstantsVector[i] = preMatrix.ConstantsVector[i];
            }
        }

        public Matrix(decimal[,] array, bool needConstantsVector = false, string name = "someMatrix") :
            this(name, 
                array.GetLength(0),
                needConstantsVector ? array.GetLength(1) - 1: array.GetLength(1),
                needConstantsVector)
        {
            if (array.GetLength(0) == 0)
                return;

            for (int i = 0; i < CountRows; i++)
            {
                for (int j = 0; j < CountColumns; j++)
                {
                    this[i, j] = array[i, j];
                }
                if (IsConstantVectorExist) ConstantsVector[i] = array[i, array.GetLength(1) - 1];
            }
        }

      
        #endregion

        #region .get .set
        public int AllItemsCount { get; }

        public string Name { get; set; }

        public decimal this[int i, int j]
        {
            get => _matrix[i, j];
            set => _matrix[i, j] = value;
        }

        public Vector ConstantsVector { get => _constantsVector; }

        public bool IsSevrice { get; set; }
        public int CountRows { get; }

        public int CountColumns { get; }
        public static bool CancelOperation { get; set ; }

        public bool IsSquare => CountColumns == CountRows;

        protected bool IsConstantVectorExist => ConstantsVector != null;

        #endregion

        #region Algebraic operations
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.CountRows != b.CountRows || a.CountColumns != b.CountColumns)
            {
                throw new ArgumentException("Сложение матриц невозможно: не совпадают их размеры");
            }

            Matrix result = new Matrix("", a.CountRows, a.CountColumns);


            for (int i = 0; i < a.CountRows; i++)
            {
                for (int j = 0; j < a.CountColumns; j++)
                {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }

            return result;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.CountColumns != b.CountRows)
            {
                throw new ArgumentException("Произведение матриц невозможно: они не согласованы");
            }

            Matrix result = new Matrix("", a.CountRows, b.CountColumns);

            for (int i = 0; i < a.CountRows; i++)
            {
                for (int j = 0; j < b.CountColumns; j++)
                {
                    result[i, j] = 0;

                    for (int k = 0; k < a.CountColumns; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return result;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.CountRows != b.CountRows || a.CountColumns != b.CountColumns)
            {
                throw new ArgumentException("Вычитание матриц невозможно: не совпадают их размеры");
            }
            
            return a + (-1) * b;
        }

        public static Matrix Power(Matrix a, int degree)
        {
            Matrix result = new Matrix("", a.CountRows, a.CountColumns);

            if (degree == 0)
            {
                for (int i = 0; i < a.CountRows; i++)
                {
                    for (int j = 0; j < a.CountColumns; j++)
                    {
                        if (i == j)
                        {
                            result[i, j] = 1;
                        }
                        else
                        {
                            result[i, j] = 0;
                        }
                    }
                }
                return result;
            }

            result = a;

            for (int i = 1; i < degree; i++)
            {
                result *= a;
            }

            return result;
        }
        public static Matrix operator *(decimal num, Matrix mtrx)
        {
            Matrix result = new Matrix("", mtrx.CountRows, mtrx.CountColumns);
            for (int i = 0; i < mtrx.CountRows; i++)
                for (int j = 0; j < mtrx.CountColumns; j++)
                    result[i, j] = mtrx[i, j] * num;
            return result;
        }

        public static Matrix operator *(Matrix mtrx, decimal num) // !
        {
            return num * mtrx;
        }

        public bool IsEqual(Matrix matrix,decimal eps)
        {
            if (CountRows != matrix.CountRows || CountColumns != matrix.CountColumns)
                return false;
            for (int i = 0; i < CountRows; i++)
            {
                for (int j = 0; j < CountColumns; j++)
                {
                    if (Math.Abs(this[i, j] - matrix[i, j]) >= eps)
                        return false;
                }
            }
            return true;
        }

        #endregion

        #region GAUS DEVELOPING

        public Matrix GetGauss(bool forDeterminant)
        {
            var result = new Matrix(this);
            result.Name += "_gaussed";
            for (int k = 0; k < CountColumns - 1; k++)
            {
                var absOfMax = Math.Abs(result[k, k]);
                var indexOfMax = k;
                for (int i = k+1; i < CountRows; i++)
                {
                    if (Math.Abs(result[i, k]) > absOfMax)
                    {
                        absOfMax = Math.Abs(result[i, k]);
                        indexOfMax = i;
                    }
                }

                if (absOfMax == decimal.Zero) continue;

               
                for (var j = 0; j < CountColumns; j++)
                {
                    var old = result[k, j];
                    result[k, j] = result[indexOfMax, j];
                    if (forDeterminant) result[k, j] *= -1;
                    result[indexOfMax, j] = old;
                }

                if (IsConstantVectorExist)
                {
                    var old = result.ConstantsVector[k];
                    result.ConstantsVector[k] = result.ConstantsVector[indexOfMax];
                    if (forDeterminant) result.ConstantsVector[k] *= -1;
                    result.ConstantsVector[indexOfMax] = old;
                }

                for (var i = k+1; i < CountRows; i++)
                {
                    var koef = result[i, k] / result[k, k] * (-1);
                    for (int j = k; j < CountColumns; j++)
                    {
                        result[i, j] += koef * result[k, j];
                        if (Math.Abs(result[i, j]) == decimal.Zero)
                            result[i, j] = 0;
                    }

                    if (IsConstantVectorExist)
                    {
                        result.ConstantsVector[i] += koef * result.ConstantsVector[k];
                        if (Math.Abs(result.ConstantsVector[i]) == decimal.Zero)
                            result.ConstantsVector[i] = 0;
                    }
                }
            }
            return result;
        }
        protected decimal DeterminantByGauss()
        {
            VerifyDetermenantPossibility();
            var gaussed = GetGauss(true);
            decimal result = 1;
            for (int i = 0; i < CountRows; i++)
            {
                result *= gaussed[i, i];
            }
            
            return result;
        }
        protected Matrix GetInverseMatrixByGauss()
        {
            var determinant =  DeterminantByGauss();
            VerifyInversePossibility(determinant);
            var inversedMatrix = (1 / DeterminantByGauss()) * MatrixOfAlgebraicComplementsByGauss().GetTransposedMatrix();
            inversedMatrix.Name = Name + "_inversed";
            return inversedMatrix;
        }
        protected Matrix MatrixOfAlgebraicComplementsByGauss()
        {
            var matrixOfAlgebraicComplements = new Matrix(Name + "_matrixOfAlgebraicComplements", CountRows, CountColumns);
            for (int i = 0; i < CountRows; i++)
                for (int j = 0; j < CountColumns; j++)
                {
                    matrixOfAlgebraicComplements[i, j] =
                        GetMatrixOfMinors(i, j).DeterminantByGauss() * (decimal) Math.Pow(-1, i + j);
                }
            return matrixOfAlgebraicComplements;
        }
        #endregion

        #region Specific matrix operations By Row

        private void VerifyDetermenantPossibility()
        {
            if (!IsSquare)
            {
                throw new ArgumentException("Определение детерминанта невозможно: матрица не квадратная");
            }

            if (CancelOperation)
            {
                CancelOperation = false;
                throw new OperationCanceledException("Отмена фоновой операции вычисления детерменанта");
            }
        }

        protected decimal DeterminantByRow()
        {
            VerifyDetermenantPossibility();
            if (AllItemsCount == 1) return this[0, 0];
            if (AllItemsCount == 4) return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
            decimal result = 0;
            for (int j = 0, k = -1; j < CountRows; j++)
            {
                k *= -1;
                result += k * this[0, j] * GetMatrixOfMinors(0, j).DeterminantByRow();
            }
            return result;
        }

        protected Matrix MatrixOfAlgebraicComplementsByRow()
        {
            var matrixOfAlgebraicComplements = new Matrix(Name + "_matrixOfAlgebraicComplements", CountRows, CountColumns);
            for (int i = 0; i < CountRows; i++)
                for (int j = 0; j < CountColumns; j++)
                    matrixOfAlgebraicComplements[i, j] = GetMatrixOfMinors(i, j).DeterminantByRow() * (int)Math.Pow(-1, i + j);

            return matrixOfAlgebraicComplements;
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void VerifyInversePossibility(decimal value)
        {
            if (Math.Abs(value) == decimal.Zero)
            {
                throw new ArgumentException("Невозможно получить обратную матрицу методом алгебраических дополнений, т.к. определитель = 0");
            }
        }
        protected Matrix GetInverseMatrixByRow()
        {
            var determinant =  DeterminantByRow();
            VerifyInversePossibility(determinant);
            var inversedMatrix = (1 / determinant) * MatrixOfAlgebraicComplementsByRow().GetTransposedMatrix();
            inversedMatrix.Name = Name + "_inversed";
            return inversedMatrix;
        }
        #endregion

        #region Specific matrix operations universal

        public Matrix GetInverseMatrix()
        {
            return GetInverseMatrixByGauss();
        }

        public decimal Determinant()
        {
            return DeterminantByGauss();
        }

        public Matrix GetTransposedMatrix()
        {
            var transposedMatrix = new Matrix(Name + "_transposed", CountColumns, CountRows);
            for (int i = 0; i < CountRows; i++)
            {
                for (int j = 0; j < CountColumns; j++)
                {
                    transposedMatrix[j, i] = this[i, j];
                }
            }

            return transposedMatrix;
        }
        protected Matrix GetMatrixOfMinors(int p, int q)
        {
            var theMatrixOfMinors = new Matrix($"{Name}_minor", CountRows - 1, CountColumns - 1);

            for (int i = 0, newI = 0; i < CountRows; i++)
            {
                for (int j = 0, newJ = 0; j < CountColumns; j++)
                {
                    if (i != p && q != j)
                    {
                        theMatrixOfMinors[newI, newJ++] = this[i, j];
                    }
                }
                if (i != p) newI++;
            }
            return theMatrixOfMinors;
        }

        public Matrix GetInverseMatrixByMod(uint n)
        {
            var det = DeterminantByGauss();
            if (Math.Abs(det) == decimal.Zero)
                throw new ArgumentException("Unable to calculate the inverse matrix by mod: determinant equals zero");

            det %= n;
            det = Math.Round(det);
            if (det < 0) det += n; //for (int i = 1; det < 0; det += n * i++);
            if (Math.Abs(det) == decimal.Zero)
                throw new ArgumentException("Unable to calculate the inverse matrix by mod: determinant equals zero");
            if (Math.Abs(n % det) == decimal.Zero)
                throw new ArgumentException("Unable to calculate the inverse matrix by mod: n % det = 0");

            var result = MatrixOfAlgebraicComplementsByGauss().GetTransposedMatrix();
           
            for (int i = 0; i < result.CountRows; i++)
            {
                for (int j = 0; j < result.CountColumns; j++)
                {
                    result[i, j] = Math.Round(result[i, j]);

                    var hasNewItemSet = false;
                    for (int p = 0; p < n; p++)
                    {
                        var newItem = (n * p + result[i, j]);

                        if (Math.Abs(newItem % det)== decimal.Zero)
                        {
                            result[i, j] = (newItem / det) % n;
                            if (result[i, j] < 0) result[i, j] += n; //for (int k = 1; result[i, j] < 0; result[i, j] += n * k++) ;
                            hasNewItemSet = true;
                            break;
                        }
                    }
                    if (!hasNewItemSet)
                        throw new ArgumentException("Unable to calculate the inverse matrix by mod: the original matrix is not suitable for this operation");
                }
            }
            result.Name += "_invmode";
            return result;
        }
        #endregion

        #region Acync
        public static void CancelBackground()
        {
            CancelOperation = true;
        }
        #endregion
    }
}
