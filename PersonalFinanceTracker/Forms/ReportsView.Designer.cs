namespace PersonalFinanceTracker.Forms
{
    partial class ReportsView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsView));
            this.pnlReports = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblRemaining = new System.Windows.Forms.Label();
            this.lblTotalBudget = new System.Windows.Forms.Label();
            this.lblTotalExpenses = new System.Windows.Forms.Label();
            this.pnlChart1 = new System.Windows.Forms.Panel();
            this.pnlChart2 = new System.Windows.Forms.Panel();
            this.pnlReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlReports
            // 
            this.pnlReports.Controls.Add(this.btnRefresh);
            this.pnlReports.Controls.Add(this.lblRemaining);
            this.pnlReports.Controls.Add(this.lblTotalBudget);
            this.pnlReports.Controls.Add(this.lblTotalExpenses);
            this.pnlReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReports.Location = new System.Drawing.Point(0, 0);
            this.pnlReports.Name = "pnlReports";
            this.pnlReports.Size = new System.Drawing.Size(757, 46);
            this.pnlReports.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(600, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(111, 37);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblRemaining
            // 
            this.lblRemaining.AutoSize = true;
            this.lblRemaining.Location = new System.Drawing.Point(360, 13);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(72, 16);
            this.lblRemaining.TabIndex = 2;
            this.lblRemaining.Text = "Remaining";
            // 
            // lblTotalBudget
            // 
            this.lblTotalBudget.AutoSize = true;
            this.lblTotalBudget.Location = new System.Drawing.Point(193, 13);
            this.lblTotalBudget.Name = "lblTotalBudget";
            this.lblTotalBudget.Size = new System.Drawing.Size(84, 16);
            this.lblTotalBudget.TabIndex = 1;
            this.lblTotalBudget.Text = "Total Budget";
            // 
            // lblTotalExpenses
            // 
            this.lblTotalExpenses.AutoSize = true;
            this.lblTotalExpenses.Location = new System.Drawing.Point(18, 13);
            this.lblTotalExpenses.Name = "lblTotalExpenses";
            this.lblTotalExpenses.Size = new System.Drawing.Size(101, 16);
            this.lblTotalExpenses.TabIndex = 0;
            this.lblTotalExpenses.Text = "Total Expenses";
            // 
            // pnlChart1
            // 
            this.pnlChart1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlChart1.Location = new System.Drawing.Point(0, 46);
            this.pnlChart1.Name = "pnlChart1";
            this.pnlChart1.Size = new System.Drawing.Size(200, 470);
            this.pnlChart1.TabIndex = 1;
            // 
            // pnlChart2
            // 
            this.pnlChart2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart2.Location = new System.Drawing.Point(200, 46);
            this.pnlChart2.Name = "pnlChart2";
            this.pnlChart2.Size = new System.Drawing.Size(557, 470);
            this.pnlChart2.TabIndex = 2;
            // 
            // ReportsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlChart2);
            this.Controls.Add(this.pnlChart1);
            this.Controls.Add(this.pnlReports);
            this.Name = "ReportsView";
            this.Size = new System.Drawing.Size(757, 516);
            this.Load += new System.EventHandler(this.ReportsView_Load);
            this.pnlReports.ResumeLayout(false);
            this.pnlReports.PerformLayout();
            this.Load += new System.EventHandler(this.ReportsView_Load);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlReports;
        private System.Windows.Forms.Label lblRemaining;
        private System.Windows.Forms.Label lblTotalBudget;
        private System.Windows.Forms.Label lblTotalExpenses;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlChart1;
        private System.Windows.Forms.Panel pnlChart2;
    }
}
