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
    public partial class Funcionario : Form
    {
        public Funcionario()
        {
            InitializeComponent();
            int codigo;
            codigo = Codigo();
            lblCodF.Text = Convert.ToString(codigo);
        }
        public static int Codigo()
        {
            int codigo;

            FileStream fs = new FileStream("Funcionarios.txt", FileMode.OpenOrCreate);
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
            int cFunc = Codigo();
            FileStream func = new FileStream("Funcionarios.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(func);

            string nome = txtNome.Text;
            string tel = txtTel.Text;
            string cargo = txtCargo.Text;

            if (nome == "" && tel == "" && cargo == "" && tel == "")
            {
                MessageBox.Show("Preencha todos os campos!");
                sw.Close();
            }
            else
            {
                double salario = Convert.ToDouble(txtSal.Text);

                sw.WriteLine(cFunc + "|" + nome + "|" + tel + "|" + cargo + "|" + salario);
                txtNome.Text = "";
                txtTel.Text = "";
                txtSal.Text = "";
                txtCargo.Text = "";
                sw.Close();
                this.Close();
                MessageBox.Show("Cadastrado com Sucesso!");
            }
        }

        private void Funcionario_Load(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesq_Click(object sender, EventArgs e)
        {
            FileStream fsCR = new FileStream("Funcionarios.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsCR);
            bool cond = false;
            string Nome = txtNomeP.Text;
            DataTable dt = new DataTable();
            dt.Columns.Add("Código");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Telefone");
            dt.Columns.Add("Cargo");
            dt.Columns.Add("Salário");

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
            dgvFunc.DataSource = dt;
            sr.Close();
            if (cond == false)
            {
                MessageBox.Show("Funcionário não cadastrado!");
            }
        }
    }
}
