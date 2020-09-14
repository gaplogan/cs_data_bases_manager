using System;
using System.IO;
using System.Windows.Forms;

// ImportReference: C:\Program Files\Microsoft SQL Server Compact Edition\v4.0\Desktop
using System.Data.SqlServerCe;

namespace SqlServerCE_Manager
{
    public partial class Form1 : Form
    {
        string dbPath;
        string dbPassword;
        string strConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dbPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyDataBase.sdf";
            dbPassword = "1234";
            strConnection = $@"DataSource = {dbPath}; Password='{dbPassword}'";

            // Criar o objeto para manipular a base de dados
            SqlCeEngine db = new SqlCeEngine(strConnection);

            // Verificar se a base de dados existe
            if (!File.Exists(dbPath))
            {
                try
                {
                    // Caso não exista cria a base de dados
                    db.CreateDatabase();
                    // Exibir mensagem de status
                    labelResultado.Text = "Base de dados criada com sucesso.";
                }
                catch (Exception ex)
                {
                    // Exibir mensagem de erro, caso aconteça algum
                    labelResultado.Text = "Erro : " + ex.Message;
                }
                
            }
            // Fechar a conexão com a base de dados
            db.Dispose();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            // Criar o objeto de conexão com a base de dados
            SqlCeConnection conexao = new SqlCeConnection();

            try
            {
                // Definir a string de conexão com a base de dados (também pode ser passada pelo do construtor)
                conexao.ConnectionString = strConnection;
                // Abrir a conexão com a base de dados
                conexao.Open();
                // Exibir mensagem de status
                labelResultado.Text = "Conectado";
            }
            catch (Exception ex)
            {
                // Exibir mensagem de erro, caso aconteça algum
                labelResultado.Text = "Erro : " + ex.Message;
            }
            finally
            {
                // Fechar a conexão com a base de dados
                conexao.Close();
            }
        }

        private void btnCriarTabela_Click(object sender, EventArgs e)
        {
            // Criar a conexão com a base de dados
            SqlCeConnection conexao = new SqlCeConnection(strConnection);

            // Criar o objeto para executar comandos sql
            SqlCeCommand comando = new SqlCeCommand();

            try
            {
                // Abrir a conexão com a base de dados
                conexao.Open();                
                // Definir a conexão onde os comandos serão executados
                comando.Connection = conexao;
                // Definir o comando a ser executado
                comando.CommandText = "CREATE TABLE pessoas ( id INT NOT NULL PRIMARY KEY, nome NVARCHAR(50), email NVARCHAR(50))";
                // Executar o comando definido anteriormente
                comando.ExecuteNonQuery();
                // Exibir o status
                labelResultado.Text = "Tabela Criada";                
            }
            catch (Exception ex)
            {
                // Exibir mensagem de erro, caso aconteça algum
                labelResultado.Text = "Erro : " + ex.Message;
            }
            finally
            {
                // Liberar os recursos utilizados pelo objeto comando
                comando.Dispose();
                // Fechar a conexão com a base de dados
                conexao.Close();
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            int id = new Random(DateTime.Now.Millisecond).Next(0, 1000);
            string nome = txtNome.Text;
            string email = txtEmail.Text;

            // Validar os campos
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email))
            {
                // Mensagem de aviso
                labelResultado.Text = "Preencha todos os campos";
                // Cancela a execução do comando
                return;
            }

            // Criar a conexão com a base de dados
            SqlCeConnection conexao = new SqlCeConnection(strConnection);

            // Criar o objeto para executar comandos sql
            SqlCeCommand comando = new SqlCeCommand();

            try
            {
                // Abrir a conexão com a base de dados
                conexao.Open();
                // Definir a conexão onde os comandos serão executados
                comando.Connection = conexao;
                // Definir o comando a ser executado
                comando.CommandText = "INSERT INTO pessoas VALUES(" + id + ",'" + nome + "','" + email + "')";
                // Executar o comando definido anteriormente, retorna o número de linhas afetadas
                int res = comando.ExecuteNonQuery();
                // Exibir o status
                labelResultado.Text = res + " linhas afetadas.";
            }
            catch (Exception ex)
            {
                // Exibir mensagem de erro, caso aconteça algum
                labelResultado.Text = "Erro : " + ex.Message;
            }
            finally
            {
                // Liberar os recursos utilizados pelo objeto comando
                comando.Dispose();
                // Fechar a conexão com a base de dados
                conexao.Close();
            }
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }
    }
}
