﻿namespace ControledePonto
{
    partial class FormPonto
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
            buttonAnalise = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // buttonAnalise
            // 
            buttonAnalise.Location = new Point(25, 34);
            buttonAnalise.Name = "buttonAnalise";
            buttonAnalise.Size = new Size(75, 23);
            buttonAnalise.TabIndex = 0;
            buttonAnalise.Text = "Analisar ";
            buttonAnalise.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(25, 63);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(763, 366);
            textBox1.TabIndex = 1;
            // 
            // FormPonto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(buttonAnalise);
            Name = "FormPonto";
            Text = "Analise de Horas";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAnalise;
        private TextBox textBox1;
    }
}