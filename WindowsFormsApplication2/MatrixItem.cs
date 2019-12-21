using LinearAlgebra;
namespace MatrixCalculator
{
    public class MatrixItem
    {
        readonly  Matrix _ref;
        readonly int _indexI;
        readonly int _indexJ;
        public MatrixItem(int i,int j, Matrix val)
        {
            _indexI = i;
            _indexJ = j;
            _ref = val;
        }
        public decimal Item
        {
            set
            {
                _ref[_indexI, _indexJ] = value;
            }
            get
            {
                return _ref[_indexI, _indexJ];
            }
        }
    }
}
