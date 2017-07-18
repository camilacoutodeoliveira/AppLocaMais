using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace AppLocaMais
{
    public partial class fundo : Form
    {
        public fundo()
        {
            InitializeComponent();
            int codigo;
            codigo = Codigo(); 
            lblCod.Text = Convert.ToString(codigo);
        }
        public static int Codigo()
        {
            int codigo;

            FileStream fs = new FileStream("Clientes.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            int cont = 1;
            string linha;
            do
            {
                linha = sr.ReadLine();
                if (linha != null)
                {
                    cont++;
                }

            } while (linha != null);

            codigo = cont;
            sr.Close();
            return codigo;            
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            int cCliente = Codigo();

            FileStream fs = new FileStream("Clientes.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            string nome = txtNome.Text;
            string endereco = txtEnd.Text;
            string telefone = txtTel.Text;

            if (nome == "" && endereco == "" && telefone == "")
            {
                MessageBox.Show("Preencha todos os campos!");
                sw.Close();
            }
            else
            {
                sw.WriteLine(cCliente + "|" + nome + "|" + endereco + "|" + telefone);
                txtNome.Text = "";
                txtEnd.Text = "";
                txtTel.Text = "";
                sw.Close();
                this.Close();
                MessageBox.Show("Cadastrado com Sucesso!");

            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesq_Click(object sender, EventArgs e)
        {
            FileStream fsCR = new FileStream("Clientes.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsCR);
            bool cond =false;
            string Nome = txtNomeP.Text;
            DataTable dt = new DataTable();
            dt.Columns.Add("Código");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Endereço");
            dt.Columns.Add("Telefone");

            string linha;
            string[] dados;
            do
            {
                linha = sr.ReadLine();
                if (linha != null)
                {
                    dados = linha.Split('|');
                    if (dados[1] == Nome)
                    {
                        dt.Rows.Add(linha.Split('|'));
                        cond = true;
                    }
                }
            } while (!sr.EndOfStream);
            dgvCliente.DataSource = dt;
            sr.Close();
            if (cond == false)
            {
                MessageBox.Show("Cliente não cadastrado!");

            }
        }

        private void dgvCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
