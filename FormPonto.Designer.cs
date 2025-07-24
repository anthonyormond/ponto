namespace ControledePonto
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
            textBoxSite = new TextBox();
            ButtonAcessar = new Button();
            textBoxLogin = new TextBox();
            textBoxSenha = new TextBox();
            dateTimePickerInicio = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            dateTimePickerFim = new DateTimePicker();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // buttonAnalise
            // 
            buttonAnalise.Location = new Point(117, 218);
            buttonAnalise.Name = "buttonAnalise";
            buttonAnalise.Size = new Size(75, 23);
            buttonAnalise.TabIndex = 0;
            buttonAnalise.Text = "Importar";
            buttonAnalise.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(25, 256);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(763, 294);
            textBox1.TabIndex = 1;
            // 
            // textBoxSite
            // 
            textBoxSite.Location = new Point(25, 36);
            textBoxSite.Name = "textBoxSite";
            textBoxSite.Size = new Size(426, 23);
            textBoxSite.TabIndex = 2;
            // 
            // ButtonAcessar
            // 
            ButtonAcessar.Location = new Point(25, 218);
            ButtonAcessar.Name = "ButtonAcessar";
            ButtonAcessar.Size = new Size(75, 23);
            ButtonAcessar.TabIndex = 4;
            ButtonAcessar.Text = "Acessar";
            ButtonAcessar.UseVisualStyleBackColor = true;
            ButtonAcessar.Click += ButtonAcessar_Click;
            // 
            // textBoxLogin
            // 
            textBoxLogin.Location = new Point(25, 82);
            textBoxLogin.Name = "textBoxLogin";
            textBoxLogin.Size = new Size(292, 23);
            textBoxLogin.TabIndex = 5;
            // 
            // textBoxSenha
            // 
            textBoxSenha.Location = new Point(25, 131);
            textBoxSenha.Name = "textBoxSenha";
            textBoxSenha.Size = new Size(292, 23);
            textBoxSenha.TabIndex = 6;
            textBoxSenha.UseSystemPasswordChar = true;
            // 
            // dateTimePickerInicio
            // 
            dateTimePickerInicio.Format = DateTimePickerFormat.Short;
            dateTimePickerInicio.Location = new Point(25, 180);
            dateTimePickerInicio.Name = "dateTimePickerInicio";
            dateTimePickerInicio.Size = new Size(141, 23);
            dateTimePickerInicio.TabIndex = 7;
            dateTimePickerInicio.Value = new DateTime(2025, 7, 23, 18, 11, 50, 0);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 18);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 8;
            label1.Text = "Site rhid.com.br";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 64);
            label2.Name = "label2";
            label2.Size = new Size(47, 15);
            label2.TabIndex = 9;
            label2.Text = "Usuário";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 113);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 10;
            label3.Text = "Senha";
            // 
            // dateTimePickerFim
            // 
            dateTimePickerFim.Format = DateTimePickerFormat.Short;
            dateTimePickerFim.Location = new Point(172, 180);
            dateTimePickerFim.Name = "dateTimePickerFim";
            dateTimePickerFim.Size = new Size(145, 23);
            dateTimePickerFim.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(25, 162);
            label4.Name = "label4";
            label4.Size = new Size(65, 15);
            label4.TabIndex = 12;
            label4.Text = "Data Inicial";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(172, 162);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 13;
            label5.Text = "Data Final";
            // 
            // FormPonto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 567);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(dateTimePickerFim);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dateTimePickerInicio);
            Controls.Add(textBoxSenha);
            Controls.Add(textBoxLogin);
            Controls.Add(ButtonAcessar);
            Controls.Add(textBoxSite);
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
        private TextBox textBoxSite;
        private Button ButtonAcessar;
        private TextBox textBoxLogin;
        private TextBox textBoxSenha;
        private DateTimePicker dateTimePickerInicio;
        private Label label1;
        private Label label2;
        private Label label3;
        private DateTimePicker dateTimePickerFim;
        private Label label4;
        private Label label5;
    }
}