
namespace SampleApp
{
    partial class Form2
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
            this.pageConnectivityDesign1 = new SampleApp.PageConnectivityDesign();
            this.SuspendLayout();
            // 
            // pageConnectivityDesign1
            // 
            this.pageConnectivityDesign1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageConnectivityDesign1.Location = new System.Drawing.Point(0, 0);
            this.pageConnectivityDesign1.Name = "pageConnectivityDesign1";
            this.pageConnectivityDesign1.Size = new System.Drawing.Size(800, 450);
            this.pageConnectivityDesign1.TabIndex = 0;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pageConnectivityDesign1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private PageConnectivityDesign pageConnectivityDesign1;
    }
}