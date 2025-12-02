namespace Transformese.Desktop.Views
{
    partial class ViewInscricoes
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            pnlTopoFiltros = new Guna.UI2.WinForms.Guna2Panel();
            lblTituloPagina = new Guna.UI2.WinForms.Guna2HtmlLabel();
            txtPesquisa = new Guna.UI2.WinForms.Guna2TextBox();
            pnlSeparator = new Guna.UI2.WinForms.Guna2Panel();
            pnlGridContainer = new Guna.UI2.WinForms.Guna2Panel();
            dgvTotalInscricoes = new Guna.UI2.WinForms.Guna2DataGridView();
            clnCandidato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            clnCidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            clnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            clnOng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            clnAcao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            pnlTopoFiltros.SuspendLayout();
            pnlGridContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTotalInscricoes).BeginInit();
            SuspendLayout();
            // 
            // pnlTopoFiltros
            // 
            pnlTopoFiltros.BackColor = System.Drawing.Color.Transparent;
            pnlTopoFiltros.BorderRadius = 10;
            pnlTopoFiltros.Controls.Add(txtPesquisa);
            pnlTopoFiltros.Controls.Add(lblTituloPagina);
            pnlTopoFiltros.CustomizableEdges = customizableEdges3;
            pnlTopoFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            pnlTopoFiltros.FillColor = System.Drawing.Color.White;
            pnlTopoFiltros.Location = new System.Drawing.Point(20, 20);
            pnlTopoFiltros.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            pnlTopoFiltros.Name = "pnlTopoFiltros";
            pnlTopoFiltros.ShadowDecoration.BorderRadius = 10;
            pnlTopoFiltros.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlTopoFiltros.ShadowDecoration.Depth = 10;
            pnlTopoFiltros.ShadowDecoration.Enabled = true;
            pnlTopoFiltros.Size = new System.Drawing.Size(940, 80);
            pnlTopoFiltros.TabIndex = 0;
            // 
            // lblTituloPagina
            // 
            lblTituloPagina.BackColor = System.Drawing.Color.Transparent;
            lblTituloPagina.Font = new System.Drawing.Font("Poppins ExtraBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblTituloPagina.ForeColor = System.Drawing.Color.FromArgb(22, 22, 22);
            lblTituloPagina.Location = new System.Drawing.Point(25, 25);
            lblTituloPagina.Name = "lblTituloPagina";
            lblTituloPagina.Size = new System.Drawing.Size(180, 30);
            lblTituloPagina.TabIndex = 0;
            lblTituloPagina.Text = "Inscrições Recebidas";
            // 
            // txtPesquisa
            // 
            txtPesquisa.BorderRadius = 10;
            txtPesquisa.CustomizableEdges = customizableEdges1;
            txtPesquisa.DefaultText = "";
            txtPesquisa.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
            txtPesquisa.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
            txtPesquisa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            txtPesquisa.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
            txtPesquisa.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            txtPesquisa.Font = new System.Drawing.Font("Segoe UI", 9F);
            txtPesquisa.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            txtPesquisa.Location = new System.Drawing.Point(600, 20);
            txtPesquisa.Name = "txtPesquisa";
            txtPesquisa.PlaceholderText = "Buscar por nome ou CPF...";
            txtPesquisa.SelectedText = "";
            txtPesquisa.ShadowDecoration.BorderRadius = 10;
            txtPesquisa.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtPesquisa.ShadowDecoration.Depth = 5;
            txtPesquisa.ShadowDecoration.Enabled = true;
            txtPesquisa.Size = new System.Drawing.Size(300, 40);
            txtPesquisa.TabIndex = 1;
            // 
            // pnlSeparator
            // 
            pnlSeparator.CustomizableEdges = customizableEdges5;
            pnlSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            pnlSeparator.Location = new System.Drawing.Point(20, 100);
            pnlSeparator.Name = "pnlSeparator";
            pnlSeparator.ShadowDecoration.CustomizableEdges = customizableEdges6;
            pnlSeparator.Size = new System.Drawing.Size(940, 20);
            pnlSeparator.TabIndex = 1;
            // 
            // pnlGridContainer
            // 
            pnlGridContainer.BackColor = System.Drawing.Color.Transparent;
            pnlGridContainer.BorderRadius = 10;
            pnlGridContainer.Controls.Add(dgvTotalInscricoes);
            pnlGridContainer.CustomizableEdges = customizableEdges7;
            pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlGridContainer.FillColor = System.Drawing.Color.White;
            pnlGridContainer.Location = new System.Drawing.Point(20, 120);
            pnlGridContainer.Name = "pnlGridContainer";
            pnlGridContainer.Padding = new System.Windows.Forms.Padding(20);
            pnlGridContainer.ShadowDecoration.BorderRadius = 10;
            pnlGridContainer.ShadowDecoration.CustomizableEdges = customizableEdges8;
            pnlGridContainer.ShadowDecoration.Depth = 10;
            pnlGridContainer.ShadowDecoration.Enabled = true;
            pnlGridContainer.Size = new System.Drawing.Size(940, 600);
            pnlGridContainer.TabIndex = 2;
            // 
            // dgvTotalInscricoes
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dgvTotalInscricoes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Poppins ExtraBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(136, 136, 136);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvTotalInscricoes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvTotalInscricoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTotalInscricoes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { clnCandidato, clnCidade, clnStatus, clnOng, clnAcao });
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvTotalInscricoes.DefaultCellStyle = dataGridViewCellStyle4;
            dgvTotalInscricoes.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvTotalInscricoes.GridColor = System.Drawing.Color.FromArgb(231, 229, 255);
            dgvTotalInscricoes.Location = new System.Drawing.Point(20, 20);
            dgvTotalInscricoes.Name = "dgvTotalInscricoes";
            dgvTotalInscricoes.RowHeadersVisible = false;
            dgvTotalInscricoes.Size = new System.Drawing.Size(900, 560);
            dgvTotalInscricoes.TabIndex = 0;
            dgvTotalInscricoes.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            dgvTotalInscricoes.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvTotalInscricoes.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            dgvTotalInscricoes.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            dgvTotalInscricoes.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            dgvTotalInscricoes.ThemeStyle.BackColor = System.Drawing.Color.White;
            dgvTotalInscricoes.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(231, 229, 255);
            dgvTotalInscricoes.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(100, 88, 255);
            dgvTotalInscricoes.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dgvTotalInscricoes.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            dgvTotalInscricoes.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            dgvTotalInscricoes.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTotalInscricoes.ThemeStyle.HeaderStyle.Height = 24;
            dgvTotalInscricoes.ThemeStyle.ReadOnly = false;
            dgvTotalInscricoes.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            dgvTotalInscricoes.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTotalInscricoes.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            dgvTotalInscricoes.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            dgvTotalInscricoes.ThemeStyle.RowsStyle.Height = 25;
            dgvTotalInscricoes.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
            dgvTotalInscricoes.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
            // 
            // clnCandidato
            // 
            clnCandidato.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            clnCandidato.HeaderText = "CANDIDATO";
            clnCandidato.Name = "clnCandidato";
            // 
            // clnCidade
            // 
            clnCidade.HeaderText = "CIDADE";
            clnCidade.Name = "clnCidade";
            // 
            // clnStatus
            // 
            clnStatus.HeaderText = "STATUS";
            clnStatus.Name = "clnStatus";
            // 
            // clnOng
            // 
            clnOng.HeaderText = "ONG RESP.";
            clnOng.Name = "clnOng";
            // 
            // clnAcao
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            clnAcao.DefaultCellStyle = dataGridViewCellStyle3;
            clnAcao.HeaderText = "AÇÃO";
            clnAcao.Name = "clnAcao";
            // 
            // ViewInscricoes
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ButtonFace;
            Controls.Add(pnlGridContainer);
            Controls.Add(pnlSeparator);
            Controls.Add(pnlTopoFiltros);
            Name = "ViewInscricoes";
            Padding = new System.Windows.Forms.Padding(20);
            Size = new System.Drawing.Size(980, 740);
            pnlTopoFiltros.ResumeLayout(false);
            pnlTopoFiltros.PerformLayout();
            pnlGridContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTotalInscricoes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTopoFiltros;
        private Guna.UI2.WinForms.Guna2TextBox txtPesquisa;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTituloPagina;
        private Guna.UI2.WinForms.Guna2Panel pnlSeparator;
        private Guna.UI2.WinForms.Guna2Panel pnlGridContainer;
        private Guna.UI2.WinForms.Guna2DataGridView dgvTotalInscricoes;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnCandidato;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnCidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnOng;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnAcao;
    }
}
