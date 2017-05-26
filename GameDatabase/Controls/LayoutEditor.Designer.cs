namespace GameDatabase
{
    partial class LayoutEditor
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
            this.SuspendLayout();
            // 
            // LayoutEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "LayoutEditor";
            this.Size = new System.Drawing.Size(373, 366);
            this.SizeChanged += new System.EventHandler(this.LayoutEditor_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LayoutEditor_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LayoutEditor_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
