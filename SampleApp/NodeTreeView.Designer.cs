
namespace SampleApp
{
    partial class NodeTreeView
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
            this._treeView = new AdvTreeControls.TreeViewAdv();
            this.SuspendLayout();
            // 
            // _treeView
            // 
            this._treeView.BackColor = System.Drawing.SystemColors.Window;
            this._treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._treeView.Cursor = System.Windows.Forms.Cursors.Default;
            this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._treeView.DragDropMarkColor = System.Drawing.SystemColors.MenuHighlight;
            this._treeView.DragDropMarkWidth = 2F;
            this._treeView.FullRowSelect = true;
            this._treeView.LineColor = System.Drawing.SystemColors.ControlDark;
            this._treeView.Location = new System.Drawing.Point(0, 0);
            this._treeView.Model = null;
            this._treeView.Name = "_treeView";
            this._treeView.SelectedNode = null;
            this._treeView.Size = new System.Drawing.Size(600, 542);
            this._treeView.TabIndex = 0;
            this._treeView.Text = "treeViewAdv1";
            // 
            // NodeTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._treeView);
            this.Name = "NodeTreeView";
            this.Size = new System.Drawing.Size(600, 542);
            this.ResumeLayout(false);

        }

        #endregion

        private AdvTreeControls.TreeViewAdv _treeView;
    }
}
