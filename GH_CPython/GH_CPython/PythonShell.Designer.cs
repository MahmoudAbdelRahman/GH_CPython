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
            this.PythonCanvas = new System.Windows.Forms.RichTextBox();
            this.console = new System.Windows.Forms.RichTextBox();
            this.Test = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.light = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PythonCanvas
            // 
            this.PythonCanvas.AcceptsTab = true;
            this.PythonCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PythonCanvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(36)))), ((int)(((byte)(63)))));
            this.PythonCanvas.BulletIndent = 10;
            this.PythonCanvas.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PythonCanvas.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PythonCanvas.Location = new System.Drawing.Point(2, 3);
            this.PythonCanvas.Name = "PythonCanvas";
            this.PythonCanvas.Size = new System.Drawing.Size(872, 410);
            this.PythonCanvas.TabIndex = 0;
            this.PythonCanvas.Text = "";
            this.PythonCanvas.WordWrap = false;
            this.PythonCanvas.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // console
            // 
            this.console.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.console.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.console.Location = new System.Drawing.Point(2, 419);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.Size = new System.Drawing.Size(872, 141);
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
            this.Test.Click += new System.EventHandler(this.Test_Click);
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
            // PythonShell
            // 
            this.AcceptButton = this.close;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 591);
            this.Controls.Add(this.light);
            this.Controls.Add(this.close);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.console);
            this.Controls.Add(this.PythonCanvas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(310, 393);
            this.Name = "PythonShell";
            this.ShowInTaskbar = false;
            this.Text = "Python Interface";
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        public System.Windows.Forms.RichTextBox PythonCanvas;
        private System.Windows.Forms.RichTextBox console;
        public System.Windows.Forms.Button Test;
        public System.Windows.Forms.Button close;
        public System.Windows.Forms.TextBox light;

    }
}