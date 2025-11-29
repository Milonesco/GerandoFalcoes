using System;
using System.Drawing;
using System.Windows.Forms;
using Transformese.Desktop.Services;

namespace Transformese.Desktop
{
    public partial class Login : Form
    {
        // 1. Componentes
        private Panel panelImage;
        private Panel panelForm;
        private Label lblTitle;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;

        private readonly NavigationService _navigationService;

        public Login()
        {
            // Inicializa componentes (como se fosse o InitializeComponent() do Designer)
            InitializeCustomComponents();

            _navigationService = new NavigationService();

            // Conecta o evento do botão ao método de login
            btnLogin.Click += new EventHandler(this.btnLogin_Click);
        }

        private void InitializeCustomComponents()
        {
            // Desativa o auto-scaling padrão para controle manual do tamanho
            this.AutoScaleMode = AutoScaleMode.None;

            // --- 2. Configuração do Formulário Principal (Container) ---
            this.Text = "Transformese - Acesso";
            this.Size = new Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = AppTheme.Branco;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // --- 3. Criação dos Painéis de Layout (50% / 50%) ---

            // Painel da Imagem (Esquerda - 400x600)
            panelImage = new Panel();
            panelImage.BackColor = AppTheme.CinzaEscuro;
            panelImage.Dock = DockStyle.Left;
            panelImage.Width = 400;
            // TODO: Adicionar lógica para carregar a BackgroundImage aqui
            this.Controls.Add(panelImage);

            // Painel do Formulário (Direita - Ocupa o restante)
            panelForm = new Panel();
            panelForm.BackColor = AppTheme.Branco;
            panelForm.Dock = DockStyle.Fill;
            this.Controls.Add(panelForm);

            // --- 4. Componentes de Entrada no panelForm ---

            // Título
            lblTitle = new Label();
            lblTitle.Text = "ACESSO AO BACKOFFICE";
            lblTitle.Font = new Font("Poppins", 16, FontStyle.Bold);
            lblTitle.ForeColor = AppTheme.CinzaEscuro;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(50, 80);
            panelForm.Controls.Add(lblTitle);

            // Campo Usuário
            txtUsername = new TextBox();
            txtUsername.PlaceholderText = "Usuário";
            txtUsername.Size = new Size(300, 30);
            txtUsername.Location = new Point(50, 160);
            panelForm.Controls.Add(txtUsername);

            // Campo Senha
            txtPassword = new TextBox();
            txtPassword.PlaceholderText = "Senha";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(300, 30);
            txtPassword.Location = new Point(50, 220);
            panelForm.Controls.Add(txtPassword);

            // Botão Login
            btnLogin = new Button();
            btnLogin.Text = "ENTRAR";
            btnLogin.BackColor = AppTheme.AzulCiano;
            btnLogin.ForeColor = AppTheme.Branco;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Font = new Font("Poppins", 10, FontStyle.Bold);
            btnLogin.Size = new Size(300, 45);
            btnLogin.Location = new Point(50, 300);
            panelForm.Controls.Add(btnLogin);

            // Garante que o painel de imagem fique por cima (visual)
            panelImage.BringToFront();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Simulação de autenticação
            if (txtUsername.Text == "admin" && txtPassword.Text == "123")
            {
                // Navega para o Dashboard e fecha o formulário atual
                _navigationService.NavigateToDashboard(this);
            }
            else
            {
                MessageBox.Show("Credenciais inválidas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
