﻿namespace SQLMini.ModalWindow
{
    partial class DefTable
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgKolumny = new System.Windows.Forms.DataGridView();
            this.dgProcedury = new System.Windows.Forms.DataGridView();
            this.dgPowiazania = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgKolumny)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProcedury)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPowiazania)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgKolumny);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Kolumny";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgProcedury);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Procedury";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgPowiazania);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 424);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Powiazania";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgKolumny
            // 
            this.dgKolumny.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgKolumny.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgKolumny.Location = new System.Drawing.Point(3, 3);
            this.dgKolumny.Name = "dgKolumny";
            this.dgKolumny.Size = new System.Drawing.Size(786, 418);
            this.dgKolumny.TabIndex = 0;
            // 
            // dgProcedury
            // 
            this.dgProcedury.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProcedury.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgProcedury.Location = new System.Drawing.Point(3, 3);
            this.dgProcedury.Name = "dgProcedury";
            this.dgProcedury.Size = new System.Drawing.Size(786, 418);
            this.dgProcedury.TabIndex = 0;
            // 
            // dgPowiazania
            // 
            this.dgPowiazania.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPowiazania.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPowiazania.Location = new System.Drawing.Point(3, 3);
            this.dgPowiazania.Name = "dgPowiazania";
            this.dgPowiazania.Size = new System.Drawing.Size(786, 418);
            this.dgPowiazania.TabIndex = 0;
            // 
            // DefTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "DefTable";
            this.Text = "DefTable";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgKolumny)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProcedury)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPowiazania)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgKolumny;
        private System.Windows.Forms.DataGridView dgProcedury;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgPowiazania;
    }
}