namespace SPA5BlackBoxReader
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageBin = new System.Windows.Forms.TabPage();
            this.tabPageDecEvent = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.labelFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelReadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelChngLangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelAboutProgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polskiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 443);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(846, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelFileToolStripMenuItem,
            this.labelInfoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(846, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageBin);
            this.tabControl.Controls.Add(this.tabPageDecEvent);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(12, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(822, 413);
            this.tabControl.TabIndex = 2;
            // 
            // tabPageBin
            // 
            this.tabPageBin.Location = new System.Drawing.Point(4, 22);
            this.tabPageBin.Name = "tabPageBin";
            this.tabPageBin.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBin.Size = new System.Drawing.Size(814, 387);
            this.tabPageBin.TabIndex = 0;
            this.tabPageBin.Text = "labelBin";
            this.tabPageBin.UseVisualStyleBackColor = true;
            // 
            // tabPageDecEvent
            // 
            this.tabPageDecEvent.Location = new System.Drawing.Point(4, 22);
            this.tabPageDecEvent.Name = "tabPageDecEvent";
            this.tabPageDecEvent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDecEvent.Size = new System.Drawing.Size(814, 387);
            this.tabPageDecEvent.TabIndex = 1;
            this.tabPageDecEvent.Text = "tabPageDecEvent";
            this.tabPageDecEvent.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(814, 387);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // labelFileToolStripMenuItem
            // 
            this.labelFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelReadToolStripMenuItem,
            this.labelChngLangToolStripMenuItem,
            this.labelCloseToolStripMenuItem});
            this.labelFileToolStripMenuItem.Name = "labelFileToolStripMenuItem";
            this.labelFileToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.labelFileToolStripMenuItem.Text = "labelFile";
            // 
            // labelReadToolStripMenuItem
            // 
            this.labelReadToolStripMenuItem.Name = "labelReadToolStripMenuItem";
            this.labelReadToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.labelReadToolStripMenuItem.Text = "labelRead";
            this.labelReadToolStripMenuItem.Click += new System.EventHandler(this.labelReadToolStripMenuItem_Click);
            // 
            // labelChngLangToolStripMenuItem
            // 
            this.labelChngLangToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.polskiToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.labelChngLangToolStripMenuItem.Name = "labelChngLangToolStripMenuItem";
            this.labelChngLangToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.labelChngLangToolStripMenuItem.Text = "labelChngLang";
            //this.labelChngLangToolStripMenuItem.Click += new System.EventHandler(this.labelChngLangToolStripMenuItem_Click);
            // 
            // labelCloseToolStripMenuItem
            // 
            this.labelCloseToolStripMenuItem.Name = "labelCloseToolStripMenuItem";
            this.labelCloseToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.labelCloseToolStripMenuItem.Text = "LabelClose";
            this.labelCloseToolStripMenuItem.Click += new System.EventHandler(this.labelCloseToolStripMenuItem_Click);
            // 
            // labelInfoToolStripMenuItem
            // 
            this.labelInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelAboutProgToolStripMenuItem});
            this.labelInfoToolStripMenuItem.Name = "labelInfoToolStripMenuItem";
            this.labelInfoToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.labelInfoToolStripMenuItem.Text = "labelInfo";
            // 
            // labelAboutProgToolStripMenuItem
            // 
            this.labelAboutProgToolStripMenuItem.Name = "labelAboutProgToolStripMenuItem";
            this.labelAboutProgToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.labelAboutProgToolStripMenuItem.Text = "labelAboutProg";
            // 
            // polskiToolStripMenuItem
            // 
            this.polskiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("polskiToolStripMenuItem.Image")));
            this.polskiToolStripMenuItem.Name = "polskiToolStripMenuItem";
            this.polskiToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.polskiToolStripMenuItem.Text = "Polski";
            this.polskiToolStripMenuItem.Click += new System.EventHandler(this.polskiToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("englishToolStripMenuItem.Image")));
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 465);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "SPA-5 Black Box Reader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageBin;
        private System.Windows.Forms.TabPage tabPageDecEvent;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripMenuItem labelFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelReadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelChngLangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelAboutProgToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polskiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
    }
}

