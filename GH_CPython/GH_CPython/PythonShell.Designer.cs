namespace GH_CPython
{
    partial class PythonShell
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PythonShell));
            this.console = new System.Windows.Forms.RichTextBox();
            this.Test = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.light = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PythonCanvas = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // console
            // 
            this.console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.console.BackColor = System.Drawing.SystemColors.Control;
            this.console.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.console.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.console.Location = new System.Drawing.Point(3, 3);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.Size = new System.Drawing.Size(866, 160);
            this.console.TabIndex = 1;
            this.console.Text = "";
            // 
            // Test
            // 
            this.Test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Test.Location = new System.Drawing.Point(2, 563);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(110, 26);
            this.Test.TabIndex = 2;
            this.Test.Text = "Test";
            this.Test.UseVisualStyleBackColor = true;
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.close.Location = new System.Drawing.Point(772, 563);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(102, 26);
            this.close.TabIndex = 3;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            // 
            // light
            // 
            this.light.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.light.BackColor = System.Drawing.Color.LimeGreen;
            this.light.Location = new System.Drawing.Point(744, 566);
            this.light.Name = "light";
            this.light.Size = new System.Drawing.Size(21, 20);
            this.light.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(2, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PythonCanvas);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.console);
            this.splitContainer1.Size = new System.Drawing.Size(872, 554);
            this.splitContainer1.SplitterDistance = 384;
            this.splitContainer1.TabIndex = 5;
            // 
            // PythonCanvas
            // 
            this.PythonCanvas.AcceptsTab = true;
            this.PythonCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PythonCanvas.BackColor = System.Drawing.SystemColors.Window;
            this.PythonCanvas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PythonCanvas.BulletIndent = 10;
            this.PythonCanvas.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PythonCanvas.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PythonCanvas.Location = new System.Drawing.Point(3, 3);
            this.PythonCanvas.Name = "PythonCanvas";
            this.PythonCanvas.Size = new System.Drawing.Size(866, 378);
            this.PythonCanvas.TabIndex = 0;
            this.PythonCanvas.Text = "";
            this.PythonCanvas.WordWrap = false;
            this.PythonCanvas.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // PythonShell
            // 
            this.AcceptButton = this.close;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 591);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.light);
            this.Controls.Add(this.close);
            this.Controls.Add(this.Test);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(310, 393);
            this.Name = "PythonShell";
            this.ShowInTaskbar = false;
            this.Text = "Gh_CPython";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        public System.Windows.Forms.RichTextBox console;
        public System.Windows.Forms.Button Test;
        public System.Windows.Forms.Button close;
        public System.Windows.Forms.TextBox light;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.RichTextBox PythonCanvas;

    }
}