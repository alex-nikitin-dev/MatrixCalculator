namespace MatrixCalculator
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtxtAnswer = new System.Windows.Forms.RichTextBox();
            this.btnCreateMartix = new System.Windows.Forms.Button();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.btnStopExpr = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExprCalc = new System.Windows.Forms.Button();
            this.txtExpr = new System.Windows.Forms.TextBox();
            this.btnMultiply = new System.Windows.Forms.Button();
            this.btnFillMatrix = new System.Windows.Forms.Button();
            this.btnTestSum = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCountRow = new System.Windows.Forms.Label();
            this.numColumnCount = new System.Windows.Forms.NumericUpDown();
            this.numRowCount = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressCalc = new System.Windows.Forms.ProgressBar();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_Guide = new System.Windows.Forms.ToolStripMenuItem();
            this.MM_About = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.numBitOfResult = new System.Windows.Forms.NumericUpDown();
            this.grpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numColumnCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRowCount)).BeginInit();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBitOfResult)).BeginInit();
            this.SuspendLayout();
            // 
            // rtxtAnswer
            // 
            this.rtxtAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtAnswer.Location = new System.Drawing.Point(12, 487);
            this.rtxtAnswer.Name = "rtxtAnswer";
            this.rtxtAnswer.Size = new System.Drawing.Size(1176, 178);
            this.rtxtAnswer.TabIndex = 0;
            this.rtxtAnswer.Text = "";
            // 
            // btnCreateMartix
            // 
            this.btnCreateMartix.Location = new System.Drawing.Point(17, 107);
            this.btnCreateMartix.Name = "btnCreateMartix";
            this.btnCreateMartix.Size = new System.Drawing.Size(173, 23);
            this.btnCreateMartix.TabIndex = 2;
            this.btnCreateMartix.Text = "добавить";
            this.btnCreateMartix.UseVisualStyleBackColor = true;
            this.btnCreateMartix.Click += new System.EventHandler(this.btnCreateMartix_Click);
            // 
            // grpMain
            // 
            this.grpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMain.Controls.Add(this.label3);
            this.grpMain.Controls.Add(this.numBitOfResult);
            this.grpMain.Controls.Add(this.btnStopExpr);
            this.grpMain.Controls.Add(this.label1);
            this.grpMain.Controls.Add(this.btnExprCalc);
            this.grpMain.Controls.Add(this.txtExpr);
            this.grpMain.Controls.Add(this.btnMultiply);
            this.grpMain.Controls.Add(this.btnFillMatrix);
            this.grpMain.Controls.Add(this.btnTestSum);
            this.grpMain.Controls.Add(this.btnClearAll);
            this.grpMain.Controls.Add(this.label2);
            this.grpMain.Controls.Add(this.lblCountRow);
            this.grpMain.Controls.Add(this.numColumnCount);
            this.grpMain.Controls.Add(this.numRowCount);
            this.grpMain.Controls.Add(this.btnCreateMartix);
            this.grpMain.Location = new System.Drawing.Point(12, 27);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(1176, 454);
            this.grpMain.TabIndex = 5;
            this.grpMain.TabStop = false;
            this.grpMain.Text = "Блок управления";
            // 
            // btnStopExpr
            // 
            this.btnStopExpr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStopExpr.Enabled = false;
            this.btnStopExpr.Location = new System.Drawing.Point(524, 420);
            this.btnStopExpr.Name = "btnStopExpr";
            this.btnStopExpr.Size = new System.Drawing.Size(157, 23);
            this.btnStopExpr.TabIndex = 16;
            this.btnStopExpr.Text = "Остановить расчет";
            this.btnStopExpr.UseVisualStyleBackColor = true;
            this.btnStopExpr.Click += new System.EventHandler(this.btnStopExpr_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 392);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(326, 28);
            this.label1.TabIndex = 15;
            this.label1.Text = "Пример: A^(-1)*2+A*B^2+C*(-100) или A+(B*(C+A))^(-1)\r\nA*det(A^(-1))^(-1), det(det" +
    "(D)*T(A^(-1)))*B^(-1)";
            // 
            // btnExprCalc
            // 
            this.btnExprCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExprCalc.Location = new System.Drawing.Point(348, 420);
            this.btnExprCalc.Name = "btnExprCalc";
            this.btnExprCalc.Size = new System.Drawing.Size(157, 23);
            this.btnExprCalc.TabIndex = 14;
            this.btnExprCalc.Text = "Расчитать выражение";
            this.btnExprCalc.UseVisualStyleBackColor = true;
            this.btnExprCalc.Click += new System.EventHandler(this.btnExprCalc_Click);
            // 
            // txtExpr
            // 
            this.txtExpr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtExpr.Location = new System.Drawing.Point(6, 423);
            this.txtExpr.MaxLength = 50;
            this.txtExpr.Name = "txtExpr";
            this.txtExpr.Size = new System.Drawing.Size(326, 20);
            this.txtExpr.TabIndex = 13;
            // 
            // btnMultiply
            // 
            this.btnMultiply.Location = new System.Drawing.Point(17, 276);
            this.btnMultiply.Name = "btnMultiply";
            this.btnMultiply.Size = new System.Drawing.Size(172, 23);
            this.btnMultiply.TabIndex = 12;
            this.btnMultiply.Text = "умножить 2 первые матрицы";
            this.btnMultiply.UseVisualStyleBackColor = true;
            this.btnMultiply.Visible = false;
            this.btnMultiply.Click += new System.EventHandler(this.btnMultiply_Click);
            // 
            // btnFillMatrix
            // 
            this.btnFillMatrix.Location = new System.Drawing.Point(17, 189);
            this.btnFillMatrix.Name = "btnFillMatrix";
            this.btnFillMatrix.Size = new System.Drawing.Size(172, 23);
            this.btnFillMatrix.TabIndex = 11;
            this.btnFillMatrix.Text = "заполнить все матрицы";
            this.btnFillMatrix.UseVisualStyleBackColor = true;
            this.btnFillMatrix.Click += new System.EventHandler(this.btnFillMatrix_Click);
            // 
            // btnTestSum
            // 
            this.btnTestSum.Location = new System.Drawing.Point(17, 234);
            this.btnTestSum.Name = "btnTestSum";
            this.btnTestSum.Size = new System.Drawing.Size(172, 23);
            this.btnTestSum.TabIndex = 10;
            this.btnTestSum.Text = "сложить две первые матрицы";
            this.btnTestSum.UseVisualStyleBackColor = true;
            this.btnTestSum.Visible = false;
            this.btnTestSum.Click += new System.EventHandler(this.btnTestSum_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(17, 151);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(172, 23);
            this.btnClearAll.TabIndex = 9;
            this.btnClearAll.Text = "очистить все";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "кол-во столбцов";
            // 
            // lblCountRow
            // 
            this.lblCountRow.AutoSize = true;
            this.lblCountRow.Location = new System.Drawing.Point(118, 35);
            this.lblCountRow.Name = "lblCountRow";
            this.lblCountRow.Size = new System.Drawing.Size(72, 13);
            this.lblCountRow.TabIndex = 6;
            this.lblCountRow.Text = "кол-во строк";
            // 
            // numColumnCount
            // 
            this.numColumnCount.Location = new System.Drawing.Point(17, 70);
            this.numColumnCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numColumnCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numColumnCount.Name = "numColumnCount";
            this.numColumnCount.Size = new System.Drawing.Size(90, 20);
            this.numColumnCount.TabIndex = 4;
            this.numColumnCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numRowCount
            // 
            this.numRowCount.Location = new System.Drawing.Point(17, 31);
            this.numRowCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRowCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRowCount.Name = "numRowCount";
            this.numRowCount.Size = new System.Drawing.Size(90, 20);
            this.numRowCount.TabIndex = 3;
            this.numRowCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 668);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressCalc
            // 
            this.progressCalc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressCalc.Location = new System.Drawing.Point(457, 565);
            this.progressCalc.MarqueeAnimationSpeed = 30;
            this.progressCalc.Name = "progressCalc";
            this.progressCalc.Size = new System.Drawing.Size(281, 31);
            this.progressCalc.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressCalc.TabIndex = 7;
            this.progressCalc.Visible = false;
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1200, 24);
            this.MainMenu.TabIndex = 8;
            this.MainMenu.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MM_Guide,
            this.MM_About});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // MM_Guide
            // 
            this.MM_Guide.Name = "MM_Guide";
            this.MM_Guide.Size = new System.Drawing.Size(221, 22);
            this.MM_Guide.Text = "Руководство пользователя";
            this.MM_Guide.Click += new System.EventHandler(this.MM_Guide_Click);
            // 
            // MM_About
            // 
            this.MM_About.Name = "MM_About";
            this.MM_About.Size = new System.Drawing.Size(221, 22);
            this.MM_About.Text = "О програме";
            this.MM_About.Click += new System.EventHandler(this.MM_About_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(782, 430);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "разрядность результата";
            // 
            // numBitOfResult
            // 
            this.numBitOfResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numBitOfResult.Location = new System.Drawing.Point(699, 423);
            this.numBitOfResult.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numBitOfResult.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBitOfResult.Name = "numBitOfResult";
            this.numBitOfResult.Size = new System.Drawing.Size(67, 20);
            this.numBitOfResult.TabIndex = 17;
            this.numBitOfResult.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 690);
            this.Controls.Add(this.progressCalc);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.rtxtAnswer);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Калькулятор матриц";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.grpMain.ResumeLayout(false);
            this.grpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numColumnCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRowCount)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBitOfResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtAnswer;
        private System.Windows.Forms.Button btnCreateMartix;
        private System.Windows.Forms.GroupBox grpMain;
        private System.Windows.Forms.NumericUpDown numColumnCount;
        private System.Windows.Forms.NumericUpDown numRowCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCountRow;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnTestSum;
        private System.Windows.Forms.Button btnFillMatrix;
        private System.Windows.Forms.Button btnMultiply;
        private System.Windows.Forms.Button btnExprCalc;
        private System.Windows.Forms.TextBox txtExpr;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnStopExpr;
        private System.Windows.Forms.ProgressBar progressCalc;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MM_Guide;
        private System.Windows.Forms.ToolStripMenuItem MM_About;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numBitOfResult;
    }
}

