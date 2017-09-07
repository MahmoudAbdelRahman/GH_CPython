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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PythonShell));
            this.console = new System.Windows.Forms.RichTextBox();
            this.Test = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PythonCanvas = new FastColoredTextBoxNS.FastColoredTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPythonFileItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageDataItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pythonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseInterpreterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pipInstallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pythonShellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PythonCanvas)).BeginInit();
            this.menuStrip1.SuspendLayout();
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
            this.console.Size = new System.Drawing.Size(363, 88);
            this.console.TabIndex = 1;
            this.console.Text = "";
            // 
            // Test
            // 
            this.Test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Test.Location = new System.Drawing.Point(2, 326);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(110, 26);
            this.Test.TabIndex = 2;
            this.Test.Text = "Test";
            this.Test.UseVisualStyleBackColor = true;
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.close.Location = new System.Drawing.Point(269, 326);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(102, 26);
            this.close.TabIndex = 3;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
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
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.console);
            this.splitContainer1.Size = new System.Drawing.Size(369, 317);
            this.splitContainer1.SplitterDistance = 219;
            this.splitContainer1.TabIndex = 5;
            // 
            // PythonCanvas
            // 
            this.PythonCanvas.AllowSeveralTextStyleDrawing = true;
            this.PythonCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PythonCanvas.AutoCompleteBrackets = true;
            this.PythonCanvas.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']'};
            this.PythonCanvas.AutoIndent = false;
            this.PythonCanvas.AutoIndentChars = false;
            this.PythonCanvas.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.PythonCanvas.BackBrush = null;
            this.PythonCanvas.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.PythonCanvas.CharHeight = 14;
            this.PythonCanvas.CharWidth = 8;
            this.PythonCanvas.CommentPrefix = "#";
            this.PythonCanvas.CurrentLineColor = System.Drawing.Color.Gainsboro;
            this.PythonCanvas.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PythonCanvas.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.PythonCanvas.FindEndOfFoldingBlockStrategy = FastColoredTextBoxNS.FindEndOfFoldingBlockStrategy.Strategy2;
            this.PythonCanvas.IsReplaceMode = false;
            this.PythonCanvas.LeftBracket = '(';
            this.PythonCanvas.LeftBracket2 = '[';
            this.PythonCanvas.Location = new System.Drawing.Point(3, 27);
            this.PythonCanvas.Name = "PythonCanvas";
            this.PythonCanvas.Paddings = new System.Windows.Forms.Padding(0);
            this.PythonCanvas.RightBracket = ')';
            this.PythonCanvas.RightBracket2 = ']';
            this.PythonCanvas.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.PythonCanvas.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("PythonCanvas.ServiceColors")));
            this.PythonCanvas.ShowFoldingLines = true;
            this.PythonCanvas.Size = new System.Drawing.Size(366, 193);
            this.PythonCanvas.TabIndex = 0;
            this.PythonCanvas.Zoom = 100;
            this.PythonCanvas.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.PythonCanvas_TextChanged);
            this.PythonCanvas.Load += new System.EventHandler(this.PythonCanvas_Load);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.manageDataToolStripMenuItem,
            this.pythonToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(369, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPythonFileItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openPythonFileItem
            // 
            this.openPythonFileItem.Image = global::GH_CPython.Properties.Resources.openFile;
            this.openPythonFileItem.Name = "openPythonFileItem";
            this.openPythonFileItem.Size = new System.Drawing.Size(165, 22);
            this.openPythonFileItem.Text = "Open Python File";
            this.openPythonFileItem.Click += new System.EventHandler(this.openPythonFileItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::GH_CPython.Properties.Resources.saveIcon24;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::GH_CPython.Properties.Resources.save_as_24;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem1_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Image = global::GH_CPython.Properties.Resources.reset24;
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // manageDataToolStripMenuItem
            // 
            this.manageDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageDataItem});
            this.manageDataToolStripMenuItem.Name = "manageDataToolStripMenuItem";
            this.manageDataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.manageDataToolStripMenuItem.Text = "Data";
            // 
            // manageDataItem
            // 
            this.manageDataItem.Image = global::GH_CPython.Properties.Resources.data_data_configuration_icon;
            this.manageDataItem.Name = "manageDataItem";
            this.manageDataItem.Size = new System.Drawing.Size(152, 22);
            this.manageDataItem.Text = "Manage Data";
            // 
            // pythonToolStripMenuItem
            // 
            this.pythonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseInterpreterToolStripMenuItem,
            this.pipInstallToolStripMenuItem,
            this.pythonShellToolStripMenuItem});
            this.pythonToolStripMenuItem.Name = "pythonToolStripMenuItem";
            this.pythonToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.pythonToolStripMenuItem.Text = "Python";
            // 
            // chooseInterpreterToolStripMenuItem
            // 
            this.chooseInterpreterToolStripMenuItem.Image = global::GH_CPython.Properties.Resources.Python_logo_24;
            this.chooseInterpreterToolStripMenuItem.Name = "chooseInterpreterToolStripMenuItem";
            this.chooseInterpreterToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.chooseInterpreterToolStripMenuItem.Text = "Choose Interpreter";
            this.chooseInterpreterToolStripMenuItem.Click += new System.EventHandler(this.chooseInterpreterToolStripMenuItem_Click);
            // 
            // pipInstallToolStripMenuItem
            // 
            this.pipInstallToolStripMenuItem.Enabled = false;
            this.pipInstallToolStripMenuItem.Image = global::GH_CPython.Properties.Resources.pip_icon;
            this.pipInstallToolStripMenuItem.Name = "pipInstallToolStripMenuItem";
            this.pipInstallToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.pipInstallToolStripMenuItem.Text = "Pip install";
            this.pipInstallToolStripMenuItem.Click += new System.EventHandler(this.pipInstallToolStripMenuItem_Click);
            // 
            // pythonShellToolStripMenuItem
            // 
            this.pythonShellToolStripMenuItem.Enabled = false;
            this.pythonShellToolStripMenuItem.Name = "pythonShellToolStripMenuItem";
            this.pythonShellToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.pythonShellToolStripMenuItem.Text = "Python shell";
            this.pythonShellToolStripMenuItem.Click += new System.EventHandler(this.pythonShellToolStripMenuItem_Click);
            // 
            // PythonShell
            // 
            this.AcceptButton = this.close;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 354);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.Test);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(310, 393);
            this.Name = "PythonShell";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gh_CPython";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PythonCanvas)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }



        #endregion

        public System.Windows.Forms.RichTextBox console;
        public System.Windows.Forms.Button Test;
        public System.Windows.Forms.Button close;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public FastColoredTextBoxNS.FastColoredTextBox PythonCanvas;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageDataToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem openPythonFileItem;
        public System.Windows.Forms.ToolStripMenuItem manageDataItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pythonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseInterpreterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pipInstallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pythonShellToolStripMenuItem;

    }
}