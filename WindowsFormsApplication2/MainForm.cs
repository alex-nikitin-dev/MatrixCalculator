using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LinearAlgebra;
namespace MatrixCalculator
{
    public partial class MainForm : Form
    {
        #region Interop

        [DllImport("user32.dll")]
        static extern bool LockWindowUpdate(IntPtr hWndLock);

        #endregion

        #region .ctor
        private readonly List<Matrix> _matrixes;
        private readonly TableLayoutPanel _tblAllMatrixes;
        private readonly Random _random;
        private char _serviceName;
        private readonly int _maxCountMatrixes = 5;

        public MainForm()
        {
            InitializeComponent();
            _matrixes = new List<Matrix>();
            _tblAllMatrixes = new TableLayoutPanel();

            grpMain.Controls.Add(_tblAllMatrixes);

            ResetMatrixArray();
            numRowCount.Value = 5;
            numColumnCount.Value = 5;
            _matrixes.Clear();
            _tblAllMatrixes.Controls.Clear();
            _random = new Random();
            ResetServiceName();
        }
        #endregion

        #region matrix collection
        private int GetIndexMatrix(string name)
        {
            for (var i = 0; i < _matrixes.Count; i++)
            {
                if (_matrixes[i].Name == name)
                    return i;
            }
            throw new ArgumentOutOfRangeException($"Невозможно найти совпадение имени матрицы");
        }

        bool IsExsistNameMatrix(char name)
        {
            return _matrixes.Any(matrix => !matrix.IsSevrice && char.Parse(matrix.Name) == name);
        }
        private char NextName()
        {
            char n = 'A';
            while (IsExsistNameMatrix(n))
            {
                n++;
            }
            return n;
        }

        private readonly char _startCharForServiceNames = (char)1000;
        private readonly char[] _forbiddenServiceNames = { ']','\\','[','/','*','+','-','^','0','9','8','7','6','5','4','3','2','1','%','#','@','\'','\"','&','!','.',',','~','='};

        private char NextServiceName()
        {
            _serviceName++;
            if (_forbiddenServiceNames.Contains(_serviceName)) NextServiceName();
            return _serviceName;
        }

        private void ResetServiceName()
        {
            _serviceName = _startCharForServiceNames;
        }
        void ResetMatrixArray()
        {
            _matrixes.Clear();
            ResetTableLayout();
        }

        void ResetTableLayout()
        {
            _tblAllMatrixes.Visible = false;
            _tblAllMatrixes.Controls.Clear();
            _tblAllMatrixes.ColumnCount = 0;
            _tblAllMatrixes.RowCount = 0;
            _tblAllMatrixes.RowStyles.Clear();
            _tblAllMatrixes.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            _tblAllMatrixes.Location = new Point(lblCountRow.Right + 20, lblCountRow.Top);
        }

        private void AddMatrix(int countRows, int countCols)
        {
            if (_matrixes.Count >= _maxCountMatrixes)
            {
                MessageBox.Show(@"Добавлено максимальное количество матриц", Application.ProductName);
                return;
            }
            _matrixes.Add(new Matrix(NextName().ToString(), countRows, countCols));
            var curMatrix = _matrixes[_matrixes.Count - 1];
            AddMatrixToTableLayout(curMatrix);
        }

        private void AddButtonToTable(string name,Matrix curMatrix,EventHandler click,int row)
        {
            var btn = new Button();
            btn.Click += click;
            btn.Tag = curMatrix;
            btn.Size = new Size(180, btn.Height);
            btn.Text = name;
            btn.Dock = DockStyle.Fill;

            _tblAllMatrixes.Controls.Add(btn, _tblAllMatrixes.ColumnCount - 1, row);
            _tblAllMatrixes.RowStyles.Add(new RowStyle());
            _tblAllMatrixes.RowStyles[2].Height = btn.Height;
        }
        private void FillTableLayout()
        {
            bool add = false;
            foreach (var matrix in _matrixes)
            {
                if (!matrix.IsSevrice)
                {
                    AddMatrixToTableLayout(matrix);
                    add = true;
                }
            }
            _tblAllMatrixes.Visible = add;
        }
        private void AddMatrixToTableLayout(Matrix curMatrix)
        {
            LockWindowUpdate(grpMain.Handle);
            _tblAllMatrixes.AutoSize = false;
            _tblAllMatrixes.RowCount = 6;
            _tblAllMatrixes.ColumnCount++;

            var table = new TableLayoutPanel();
            for (int i = 0; i < curMatrix.CountRows; i++)
            {
                for (int j = 0; j < curMatrix.CountColumns; j++)
                {
                    var txtItem = new TextBox
                    {
                        Size = new Size(35, 20),
                        Tag = new MatrixItem(i, j, curMatrix),
                        Text = Math.Round(curMatrix[i, j], 2).ToString(CultureInfo.InvariantCulture)
                    };
                    txtItem.KeyPress += Txt_KeyPress;
                    txtItem.TextChanged += TxtItem_TextChanged;
                    table.Controls.Add(txtItem, j, i);
                }
            }
            table.Tag = curMatrix;
            _tblAllMatrixes.Controls.Add(table, _tblAllMatrixes.ColumnCount - 1, 0);
            _tblAllMatrixes.RowStyles.Add(new RowStyle());

            var lbl = new TextBox
            {
                Text = curMatrix.Name,
                Tag = curMatrix,
                ReadOnly = true
            };
            lbl.Size = new Size(20, lbl.Height);
            lbl.BorderStyle = BorderStyle.None;

            _tblAllMatrixes.Controls.Add(lbl, _tblAllMatrixes.ColumnCount - 1, 1);
            _tblAllMatrixes.RowStyles.Add(new RowStyle());
            _tblAllMatrixes.RowStyles[1].Height = lbl.Height;
            lbl.Anchor = AnchorStyles.None;

            AddButtonToTable(@"Определитель", curMatrix, BtnDeterm_Click, 2);
            AddButtonToTable(@"Обратная", curMatrix, BtnInverseMatrix_Click, 3);
            AddButtonToTable(@"Транспонировать", curMatrix, BtnTransposeMatrix_Click, 4);
            AddButtonToTable(@"Удалить", curMatrix, BtnDeleteMatrix_Click, 5);

            lbl.AutoSize = true;
            table.AutoSize = true;
            _tblAllMatrixes.MaximumSize = new Size(Width, Height);
            _tblAllMatrixes.AutoSizeMode = AutoSizeMode.GrowOnly;
            _tblAllMatrixes.AutoSize = true;
            _tblAllMatrixes.HorizontalScroll.Enabled = true;
           
            _tblAllMatrixes.Visible = true;

            LockWindowUpdate(IntPtr.Zero);
        }
        #endregion

        #region table matrix events
        private void BtnTransposeMatrix_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var curMartrix = (Matrix)btn.Tag;
            txtExpr.Text = @"T(" + curMartrix.Name + @")";
            ExprCalc();
        }

        private void BtnDeleteMatrix_Click(object sender, EventArgs e)
        {
            var btn = (Button) sender;
            var curMartrix = (Matrix) btn.Tag;
            _matrixes.Remove(curMartrix);

            LockWindowUpdate(grpMain.Handle);
            ResetTableLayout();
            FillTableLayout();
            LockWindowUpdate(IntPtr.Zero);
        }



        private void BtnDeterm_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var curMartrix = (Matrix)btn.Tag;
            txtExpr.Text = @"det(" + curMartrix.Name + @")";
            ExprCalc();
        }

        private void BtnInverseMatrix_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var curMartrix = (Matrix)btn.Tag;
            txtExpr.Text = curMartrix.Name + @"^(-1)";
            ExprCalc();
        }

        #endregion

        #region Events
        private void MainFormLoad(object sender, EventArgs e)
        {

        }
        private void btnFillMatrix_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _matrixes.Count; i++)
            {
                if (!_matrixes[i].IsSevrice)
                {
                    FillMatrix(i);
                    ShowMatrix(i);
                }
            }
        }
        private void btnMultiply_Click(object sender, EventArgs e)
        {
            if (_matrixes.Count < 2)
            {
                return;
            }

            try
            {
                var sum = _matrixes[0] * _matrixes[1];
                ShowMatrix(sum);
            }
            catch (ArgumentOutOfRangeException err)
            {
                MessageBox.Show(err.Message, Application.ProductName);
            }
        }
        private void btnTestSum_Click(object sender, EventArgs e)
        {
            if (_matrixes.Count < 2)
            {
                return;
            }

            try
            {
                var sum = _matrixes[0] + _matrixes[1];
                ShowMatrix(sum);
            }
            catch (ArgumentOutOfRangeException err)
            {
                MessageBox.Show(err.Message, Application.ProductName);
            }
        }
        private void btnCreateMartix_Click(object sender, EventArgs e)
        {
            AddMatrix((int)numRowCount.Value, (int)numColumnCount.Value);
        }


        private void TxtItem_TextChanged(object sender, EventArgs e)
        {
            var txt = (TextBox)sender;
            var item = (MatrixItem)txt.Tag;

            decimal value;
            decimal.TryParse(txt.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value);

            item.Item = value;
        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar))
                && (e.KeyChar != 45)
                && (e.KeyChar != (char)Keys.Back)
                && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            rtxtAnswer.Clear();
            ResetMatrixArray();
        }
        #endregion

        #region Matrix operations
        private void ShowMatrix(Matrix matrix)
        {
            if (matrix == null) return;

            rtxtAnswer.Clear();
            var bitOfResults = (int) numBitOfResult.Value;
            for (int i = 0; i < matrix.CountRows; i++)
            {
                for (int j = 0; j < matrix.CountColumns; j++)
                {
                    rtxtAnswer.AppendText(Math.Round(matrix[i, j], bitOfResults).ToString(CultureInfo.InvariantCulture) + "\t");
                }
                rtxtAnswer.AppendText(Environment.NewLine);
            }
        }
        private void FillMatrix(int index)
        {
            if (_matrixes.Count <= index)
            {
                return;
            }

            for (int i = 0; i < _matrixes[index].CountRows; i++)
            {
                for (int j = 0; j < _matrixes[index].CountColumns; j++)
                {
                    _matrixes[index][i, j] = _random.Next(0, 10);
                }
            }
        }
        private void ShowMatrix(int index)
        {
            if (_matrixes.Count <= index)
            {
                return;
            }

            var table = (TableLayoutPanel)_tblAllMatrixes.GetControlFromPosition(index, 0);
            int countRows = _matrixes[index].CountRows;
            int countCols = _matrixes[index].CountColumns;

            for (int i = 0; i < countRows; i++)
            {
                for (int j = 0; j < countCols; j++)
                {
                    var txtItem = (TextBox)table.GetControlFromPosition(j, i);
                    txtItem.TextChanged -= TxtItem_TextChanged;
                    txtItem.Text = Math.Round(_matrixes[index][i, j], 2).ToString(CultureInfo.InvariantCulture);
                    txtItem.TextChanged += TxtItem_TextChanged;
                }
            }
        }
        #endregion

        #region Messages

        void ShowRText(string text, Color clr)
        {
            rtxtAnswer.Text = text;
            rtxtAnswer.SelectAll();
            rtxtAnswer.SelectionColor = clr;
            rtxtAnswer.SelectionLength = 0;
            // rtxtAnswer.ForeColor = old;
        }

        void ShowError(string text)
        {
            ShowRText(text, Color.Red);
        }
        void ShowPlainText(string text)
        {
            ShowRText(text, Color.Black);
        }
        #endregion

        #region parser's engine
        //Model: [LeftSubstring][LeftObject][OPERATION ^*+-][RightObject][RightSubstring]
        Objects GetObjects(string src, int lIndex, int rIndex,bool needLeft,bool needRight)
        {
            var res = new Objects();
           
            {
                var loperation = MatrixOperationis.FindAnyLastIndexOf(src, lIndex - 1);
                var leftObject = src.Substring(loperation + 1, lIndex - loperation - 1);
                res.LeftObject = leftObject;
                if (needLeft && (MatrixOperationis.IsOperation(res.LeftObject) || res.LeftObject == ""))
                {
                    throw new ArgumentException("Ошибка анализа матричного выражения: неверный формат строки.");
                }
                var leftOfleftObject = src.Substring(0, loperation + 1);
                res.LeftSubstring = leftOfleftObject;

                //унарный минус
                if (double.TryParse(res.LeftObject.Trim('(', ')'), out _) 
                    && res.LeftSubstring.Length > 0 
                    && res.LeftSubstring[res.LeftSubstring.Length -1] == '-')
                {
                    res.LeftObject = "-" + res.LeftObject;
                    res.LeftSubstring=res.LeftSubstring.TrimEnd('-');
                }
            }

           
            {
                var roperation = MatrixOperationis.FindAnyIndexOf(src, rIndex + 1);
                roperation = roperation == -1 ? src.Length : roperation;
                res.RightObject = src.Substring(rIndex + 1, roperation - rIndex - 1);
                if (needRight && (MatrixOperationis.IsOperation(res.RightObject) || res.RightObject == ""))
                {
                    throw new ArgumentException("Ошибка анализа матричного выражения: неверный формат строки.");
                }
                if (roperation != src.Length) res.RightSubstring = src.Substring(roperation);
                //унарный минус
                if (double.TryParse(res.RightObject.Trim('(', ')'), out _)
                    && res.RightSubstring.Length > 0
                    && res.RightSubstring[res.RightSubstring.Length - 1] == '-')
                {
                    res.RightObject = "-" + res.RightObject;
                    res.RightSubstring = res.RightSubstring.TrimEnd('-');
                }
            }

            return res;
        }

        void AddServiceMatrix(Matrix m)
        {
            m.Name = NextServiceName().ToString();
            m.IsSevrice = true;
            _matrixes.Add(m);
        }

        class Result
        {
            public Matrix MatrixObject { get; }
            public decimal? Digit { get;  }

            public Result(decimal? digit,Matrix matrixObject)
            {
                MatrixObject = matrixObject;
                Digit = digit;
            }
        }

        private Result Parse(string strSource)
        {
            var trimStrSource = strSource.Trim('(', ')');
            if (decimal.TryParse(trimStrSource, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal dAnsw))
            {
                return new Result(dAnsw, null);
            }

            if (trimStrSource.Length == 1)
            {
                var lastMatrixName = trimStrSource[0].ToString();
                var ind = GetIndexMatrix(lastMatrixName);
                if (ind > _matrixes.Count - 1)
                {
                    return null;
                }
                return new Result(null, _matrixes[ind]);
            }

            //if (trimStrSource.Length == 2)
            //{
            //    return new Result(null, null, "THIS_IS_VECTOR");
            //}

            var mo = new MatrixOperationis(strSource);
            var nextOperation = mo.GetNext();
            if (nextOperation != null)
            {
                var currentOperation = nextOperation.Value;
                var index = currentOperation.Value;
                var objects = GetObjects(strSource,
                    index,
                    index + currentOperation.Key.Name.Length - 1,
                    currentOperation.Key.NeedLeftOperand,
                    currentOperation.Key.NeedRightOperand);

                decimal? lDouble = null;
                decimal? rDouble = null;

                int leftIndexMatrix = -1;
                try
                {
                    Result lResult = Parse(objects.LeftObject);
                    lDouble = lResult.Digit;
                    if (lResult.MatrixObject != null)
                    {
                        leftIndexMatrix = GetIndexMatrix(lResult.MatrixObject.Name);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

                int rightIndexMatrix = -1;
                try
                {
                    Result rResult = Parse(objects.RightObject);
                    rDouble = rResult.Digit;
                    if (rResult.MatrixObject != null)
                    {
                        rightIndexMatrix = GetIndexMatrix(rResult.MatrixObject.Name);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

                string result = "";
                if (rightIndexMatrix != -1 && string.Equals(currentOperation.Key.Name, "det", StringComparison.InvariantCulture))
                {
                    var dres = _matrixes[rightIndexMatrix].Determinant();
                    result = dres.ToString(CultureInfo.InvariantCulture);
                    if (dres < 0)
                    {
                        result = "(" + result + ")";
                    }
                }
                else if (rightIndexMatrix != -1 && string.Equals(currentOperation.Key.Name, "T", StringComparison.InvariantCulture))
                {
                    var m = _matrixes[rightIndexMatrix].GetTransposedMatrix();
                    if (m != null)
                    {
                        AddServiceMatrix(m);
                        result = m.Name;
                    }
                }
                else if (rightIndexMatrix != -1 && string.Equals(currentOperation.Key.Name, "mod", StringComparison.InvariantCulture))
                {
                    var dres =  Vector.MakeVector(_matrixes[rightIndexMatrix]).GetModule();
                    result = dres.ToString(CultureInfo.InvariantCulture);
                }
                else if (rightIndexMatrix != -1 && string.Equals(currentOperation.Key.Name, "gauss", StringComparison.InvariantCulture))
                {
                    var m = _matrixes[rightIndexMatrix].GetGauss(false);
                    if (m != null)
                    {
                        AddServiceMatrix(m);
                        result = m.Name;
                    }
                }
                else if (rightIndexMatrix != -1 && string.Equals(currentOperation.Key.Name, "getroots", StringComparison.InvariantCulture))
                {
                    var inMatrix = _matrixes[rightIndexMatrix];
                    var soleMatrix = SystemOfLinearEquations.ParseMatrix(inMatrix);
                    var vectorRoots =  soleMatrix.GetRoots();
                    if (vectorRoots != null)
                    {
                        AddServiceMatrix(vectorRoots);
                        result = vectorRoots.Name;
                    }
                }
                else if (rightIndexMatrix != -1 && string.Equals(currentOperation.Key.Name, "getmatrix", StringComparison.InvariantCulture))
                {
                    var inMatrix = _matrixes[rightIndexMatrix];
                    var m = SystemOfLinearEquations.ParseMatrix(inMatrix);
                    if (m != null)
                    {
                        AddServiceMatrix(m);
                        result = m.Name;
                    }
                }
                else if (rightIndexMatrix != -1 && string.Equals(currentOperation.Key.Name, "getconsts", StringComparison.InvariantCulture))
                {
                    var inMatrix = _matrixes[rightIndexMatrix];
                    var soleMatrix = SystemOfLinearEquations.ParseMatrix(inMatrix);
                    var m = new Vector(soleMatrix.ConstantsVector);
                    AddServiceMatrix(m);
                    result = m.Name;
                }
                else if (rightIndexMatrix != -1 && leftIndexMatrix != -1 && string.Equals(currentOperation.Key.Name, "scal", StringComparison.InvariantCulture))
                {
                    var dres = Vector.MakeVector(_matrixes[leftIndexMatrix])
                        .ScalarProduct(Vector.MakeVector(_matrixes[rightIndexMatrix]));
                    result = dres.ToString(CultureInfo.InvariantCulture);
                    if (dres < 0)
                    {
                        result = "(" + result + ")";
                    }
                }
                else if (lDouble != null && rDouble != null)
                {
                    decimal dres = 0;
                    switch (currentOperation.Key.Name)
                    {
                        case "*":
                            dres = lDouble.Value * rDouble.Value;
                            break;
                        case "+":
                            dres = lDouble.Value + rDouble.Value;
                            break;
                        case "-":
                            dres = lDouble.Value - rDouble.Value;
                            break;
                        case "^":
                            dres = (decimal)Math.Pow((double)lDouble.Value, (double)rDouble.Value);
                            break;
                        case "/":
                            dres = lDouble.Value / rDouble.Value;
                            break;
                    }

                    result = dres.ToString(CultureInfo.InvariantCulture);

                    if (dres < 0)
                    {
                        result = "(" + result + ")";
                    }

                }
                else if (lDouble != null && rightIndexMatrix != -1)
                {
                    if (currentOperation.Key.Name == "*")
                    {
                        Matrix m = (lDouble.Value * _matrixes[rightIndexMatrix]);
                        AddServiceMatrix(m);
                        result = m.Name;
                    }
                    else
                    {
                        throw new ArgumentException("Запрещенные операции с матрицей: " + /*lDouble +*/ currentOperation.Key.Name
                            /*+_matrixes[rightIndexMatrix].Name*/);
                    }
                }
                else if (leftIndexMatrix != -1 && rDouble != null)
                {
                    if (currentOperation.Key.Name == "*" || currentOperation.Key.Name == "^")
                    {
                        Matrix m = null;
                        if (currentOperation.Key.Name == "*")
                        {
                            m = (_matrixes[leftIndexMatrix] * rDouble.Value);
                        }
                        else if (currentOperation.Key.Name == "^")
                        {
                            // ReSharper disable once CompareOfFloatsByEqualityOperator
                            if (rDouble == -1)
                            {
                                m = _matrixes[leftIndexMatrix].GetInverseMatrix();
                            }
                            else if (rDouble >= 0)
                            {
                                m = Matrix.Power(_matrixes[leftIndexMatrix], (int) rDouble.Value);
                            }
                            else if (rDouble < 0)
                            {
                                throw new ArgumentException(
                                    "Матрица может быть возведена только в (-1) степень или в степень >0");
                            }
                        }
                        


                        if (m != null)
                        {
                            AddServiceMatrix(m);
                            result = m.Name;
                        }
                    }
                    else if (string.Equals(currentOperation.Key.Name, "getinvbymod", StringComparison.InvariantCulture))
                    {
                        var curMatrix = _matrixes[leftIndexMatrix].GetInverseMatrixByMod((uint)rDouble);
                        AddServiceMatrix(curMatrix);
                        result = curMatrix.Name;
                    }
                    else
                    {
                        throw new ArgumentException("Запрещенные операции с матрицей: " +
                                                    /*_matrixes[leftIndexMatrix].Name +*/ currentOperation.Key.Name /*+ rDouble*/);
                    }
                }
                else if (leftIndexMatrix != -1 && rightIndexMatrix != -1)
                {
                    if (currentOperation.Key.Name == "*" || currentOperation.Key.Name == "+" || currentOperation.Key.Name == "-")
                    {
                        Matrix m = null;
                        if (currentOperation.Key.Name == "*")
                        {
                            m = _matrixes[leftIndexMatrix] * _matrixes[rightIndexMatrix];
                        }
                        else if (currentOperation.Key.Name == "+")
                        {
                            m = _matrixes[leftIndexMatrix] + _matrixes[rightIndexMatrix];
                        }
                        else if (currentOperation.Key.Name == "-")
                        {
                            m = _matrixes[leftIndexMatrix] - _matrixes[rightIndexMatrix];
                        }

                        if (m != null)
                        {
                            AddServiceMatrix(m);
                            result = m.Name;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Запрещенные операции с матрицей: " +
                                                    /* _matrixes[leftIndexMatrix].Name +*/ currentOperation.Key.Name
                            /* +_matrixes[rightIndexMatrix].Name*/);
                    }
                }

                var strR = objects.LeftSubstring + result + objects.RightSubstring;
                return Parse(strR);
            }

            throw new ArgumentException(@"Невозможно рассчитать выражение: строка имеет неверный формат");
        }

        void ResetServiceMatrixes()
        {
            for (var i = 0; i < _matrixes.Count; i++)
            {
                var matrix = _matrixes[i];
                if (char.Parse(matrix.Name) >= _startCharForServiceNames)
                {
                    _matrixes.Remove(matrix);
                    i--;
                }
            }
        }
        
        void EnableControls(bool enable)
        {
            LockWindowUpdate(grpMain.Handle);
            foreach (Control control in grpMain.Controls)
            {
                control.Enabled = enable;
            }
            progressCalc.Visible = !enable;
            btnStopExpr.Enabled = !enable;
            LockWindowUpdate(IntPtr.Zero);
        }
        void ExprCalc()
        {
            EnableControls(false);

            string strSource = txtExpr.Text;
            ResetServiceMatrixes();
            ShowPlainText("");

            backgroundWorker1.RunWorkerAsync(strSource);
        }

        private void btnExprCalc_Click(object sender, EventArgs e)
        {
            ExprCalc();
        }
        #endregion

        #region Asynchron
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            var arg = (string)e.Argument;

            try
            {
                e.Result = Parse(arg);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception exp)
            {
                e.Result = exp;
            }

            if (bw != null && bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        void FinishBackground(Result res,Exception exp)
        {
            ResetServiceMatrixes();
            if (res != null)
            {
                if (res.MatrixObject != null)
                {
                    ShowMatrix(res.MatrixObject);
                }
                else if (res.Digit != null)
                {
                    int bits = (int)numBitOfResult.Value;
                    rtxtAnswer.Text = Math.Round((double) res.Digit, bits).ToString(CultureInfo.InvariantCulture);
                }
            }
            else if (exp != null)
            {
                ShowError(exp.Message);
            }
            EnableControls(true);
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Result res = null;
            Exception exp = null;
            if (!e.Cancelled)
            {
                res = e.Result as Result;
                exp = e.Result as Exception;
            }
            FinishBackground(res,exp);
        }

        private void btnStopExpr_Click(object sender, EventArgs e)
        {
            Matrix.CancelBackground();
            backgroundWorker1.CancelAsync();
        }

        #endregion

        #region MainMenu
        private void MM_About_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void MM_Guide_Click(object sender, EventArgs e)
        {
            Process.Start("Guide.htm");
        }
        #endregion

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //_tblAllMatrixes.Width = Width;
        }
    }
}

