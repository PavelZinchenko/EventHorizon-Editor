﻿namespace GameDatabase
{
    partial class ComponentEditorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComponentEditorDialog));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.structDataEditor1 = new StructDataEditor();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 378);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(400, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // structDataEditor1
            // 
            this.structDataEditor1.AutoSize = true;
            this.structDataEditor1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;
            this.structDataEditor1.ContentAutoScroll = true;
            this.structDataEditor1.Data = null;
            this.structDataEditor1.Database = null;
            this.structDataEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.structDataEditor1.Exclusions = ((System.Collections.Generic.List<string>)(resources.GetObject("structDataEditor1.Exclusions")));
            this.structDataEditor1.Location = new System.Drawing.Point(0, 0);
            this.structDataEditor1.Margin = new System.Windows.Forms.Padding(5);
            this.structDataEditor1.Name = "structDataEditor1";
            this.structDataEditor1.Size = new System.Drawing.Size(400, 378);
            this.structDataEditor1.TabIndex = 1;
            this.structDataEditor1.Load += new System.EventHandler(this.ComponentEditorDialog_Load);
            // 
            // ComponentEditorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = global::GameDatabase.Properties.Settings.Default.EditorSize;
            this.Controls.Add(this.structDataEditor1);
            this.Controls.Add(this.statusStrip1);
            this.Location = global::GameDatabase.Properties.Settings.Default.EditorPosition;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ComponentEditorDialog";
            this.Text = "ComponentEditorDialog";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ComponentEditorDialog_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private StructDataEditor structDataEditor1;
    }
}