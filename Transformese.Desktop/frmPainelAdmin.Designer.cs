namespace Transformese.Desktop
{
    partial class frmPainelAdmin
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            btnLiberarAcesso = new Guna.UI2.WinForms.Guna2Button();
            chkSenha = new Guna.UI2.WinForms.Guna2CheckBox();
            txtSenhaAdmin = new Guna.UI2.WinForms.Guna2TextBox();
            txtEmailAdmin = new Guna.UI2.WinForms.Guna2TextBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            btnCancelar = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            guna2BorderlessForm1.ContainerControl = this;
            guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Poppins SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label2.ForeColor = System.Drawing.SystemColors.WindowFrame;
            label2.Location = new System.Drawing.Point(35, 128);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(317, 23);
            label2.TabIndex = 2;
            label2.Text = "Insira seus dados para criar um novo usuário!";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Poppins Black", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(31, 80);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(335, 48);
            label1.TabIndex = 3;
            label1.Text = "Painel Administrativo";
            // 
            // btnLiberarAcesso
            // 
            btnLiberarAcesso.BorderRadius = 8;
            btnLiberarAcesso.CustomizableEdges = customizableEdges3;
            btnLiberarAcesso.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            btnLiberarAcesso.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            btnLiberarAcesso.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            btnLiberarAcesso.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            btnLiberarAcesso.FillColor = System.Drawing.Color.FromArgb(0, 168, 157);
            btnLiberarAcesso.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnLiberarAcesso.ForeColor = System.Drawing.Color.White;
            btnLiberarAcesso.Location = new System.Drawing.Point(31, 408);
            btnLiberarAcesso.Name = "btnLiberarAcesso";
            btnLiberarAcesso.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnLiberarAcesso.Size = new System.Drawing.Size(335, 45);
            btnLiberarAcesso.TabIndex = 3;
            btnLiberarAcesso.Text = "Liberar Acesso";
            btnLiberarAcesso.Click += btnLiberarAcesso_Click;
            // 
            // chkSenha
            // 
            chkSenha.AutoSize = true;
            chkSenha.CheckedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            chkSenha.CheckedState.BorderRadius = 0;
            chkSenha.CheckedState.BorderThickness = 0;
            chkSenha.CheckedState.FillColor = System.Drawing.Color.FromArgb(94, 148, 255);
            chkSenha.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            chkSenha.Location = new System.Drawing.Point(35, 358);
            chkSenha.Name = "chkSenha";
            chkSenha.Size = new System.Drawing.Size(99, 26);
            chkSenha.TabIndex = 2;
            chkSenha.Text = "Exibir senha";
            chkSenha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            chkSenha.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
            chkSenha.UncheckedState.BorderRadius = 0;
            chkSenha.UncheckedState.BorderThickness = 0;
            chkSenha.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
            // 
            // txtSenhaAdmin
            // 
            txtSenhaAdmin.BorderRadius = 8;
            txtSenhaAdmin.CustomizableEdges = customizableEdges5;
            txtSenhaAdmin.DefaultText = "";
            txtSenhaAdmin.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
            txtSenhaAdmin.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
            txtSenhaAdmin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            txtSenhaAdmin.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            txtSenhaAdmin.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            txtSenhaAdmin.Font = new System.Drawing.Font("Segoe UI", 9F);
            txtSenhaAdmin.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            txtSenhaAdmin.Location = new System.Drawing.Point(31, 313);
            txtSenhaAdmin.Name = "txtSenhaAdmin";
            txtSenhaAdmin.PlaceholderText = "Digite sua senha";
            txtSenhaAdmin.SelectedText = "";
            txtSenhaAdmin.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtSenhaAdmin.Size = new System.Drawing.Size(331, 36);
            txtSenhaAdmin.TabIndex = 1;
            // 
            // txtEmailAdmin
            // 
            txtEmailAdmin.BorderRadius = 8;
            txtEmailAdmin.CustomizableEdges = customizableEdges7;
            txtEmailAdmin.DefaultText = "";
            txtEmailAdmin.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
            txtEmailAdmin.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
            txtEmailAdmin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            txtEmailAdmin.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            txtEmailAdmin.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            txtEmailAdmin.Font = new System.Drawing.Font("Segoe UI", 9F);
            txtEmailAdmin.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            txtEmailAdmin.Location = new System.Drawing.Point(31, 246);
            txtEmailAdmin.Name = "txtEmailAdmin";
            txtEmailAdmin.PlaceholderText = "example@example.com";
            txtEmailAdmin.SelectedText = "";
            txtEmailAdmin.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtEmailAdmin.Size = new System.Drawing.Size(335, 36);
            txtEmailAdmin.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Poppins", 10F);
            label3.Location = new System.Drawing.Point(35, 285);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(88, 25);
            label3.TabIndex = 5;
            label3.Text = "Sua senha:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Poppins", 10F);
            label4.Location = new System.Drawing.Point(31, 218);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(91, 25);
            label4.TabIndex = 6;
            label4.Text = "Seu e-mail:";
            // 
            // btnCancelar
            // 
            btnCancelar.BorderRadius = 8;
            btnCancelar.CustomizableEdges = customizableEdges1;
            btnCancelar.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            btnCancelar.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            btnCancelar.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            btnCancelar.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            btnCancelar.FillColor = System.Drawing.Color.FromArgb(236, 34, 98);
            btnCancelar.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnCancelar.ForeColor = System.Drawing.Color.White;
            btnCancelar.Location = new System.Drawing.Point(31, 498);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancelar.Size = new System.Drawing.Size(335, 45);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            // 
            // frmPainelAdmin
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(400, 600);
            Controls.Add(btnCancelar);
            Controls.Add(btnLiberarAcesso);
            Controls.Add(chkSenha);
            Controls.Add(txtSenhaAdmin);
            Controls.Add(txtEmailAdmin);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "frmPainelAdmin";
            Text = "frmPainelAdmin";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnLiberarAcesso;
        private Guna.UI2.WinForms.Guna2CheckBox chkSenha;
        private Guna.UI2.WinForms.Guna2TextBox txtSenhaAdmin;
        private Guna.UI2.WinForms.Guna2TextBox txtEmailAdmin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2Button btnCancelar;
    }
}