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
    public partial class Veiculo : Form
    {
        public Veiculo()
        {
            InitializeComponent();
            int codigo;
            codigo = Codigo();
            lblCodV.Text = Convert.ToString(codigo);
        }
        public static int Codigo()
        {
            int codigo;

            FileStream fs = new FileStream("Veiculos.txt", FileMode.OpenOrCreate);
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
            int cVeiculo = Codigo();

            FileStream fs = new FileStream("Veiculos.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            string descricao = txtDesc.Text;
            string modelo = txtMod.Text;
            string cor = Convert.ToString(cbbCor.SelectedItem);
            string placa = txtPlaca.Text; 
            int qtdOcupantes = Convert.ToInt16(cbbOcup.SelectedItem);
            char status = Convert.ToChar(cbbStatus.SelectedItem);

            if (descricao == "" && modelo == "" && cor == "" && placa == ""  && qtdOcupantes == 0 && status != 'D' && status != 'M')
            {
                MessageBox.Show("Preencha todos os campos!");
                sw.Close();
            }
            else
            {
                double valorD = Convert.ToDouble(txtVal.Text);

                sw.WriteLine(cVeiculo + "|" + descricao + "|" + modelo + "|" + cor + "|" + placa + "|" + valorD + "|" + qtdOcupantes + "|" + status);
                qtdOcupantes = Convert.ToInt16(cbbOcup.SelectedItem);
                txtDesc.Text = "";
                txtMod.Text = "";
                cbbCor.SelectedItem = "";
                txtPlaca.Text = "";
                txtVal.Text = "";
                cbbStatus.SelectedItem = "";
                sw.Close();
                this.Close();
                MessageBox.Show("Cadastrado com Sucesso!");
            }
           
        }

        private void bntVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPesq_Click(object sender, EventArgs e)
        {
            FileStream fsCR = new FileStream("Veiculos.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsCR);

            DataTable dt = new DataTable();
            dt.Columns.Add("Código");
            dt.Columns.Add("Descrição");
            dt.Columns.Add("Modelo");
            dt.Columns.Add("Cor");
            dt.Columns.Add("Placa");
            dt.Columns.Add("Diária");
            dt.Columns.Add("Ocupantes");
            dt.Columns.Add("Status");
        
            string linha;
            string[] dados;
            do
            {
                linha = sr.ReadLine();
                if (linha != null)
                {
                    dados = linha.Split('|');
               
                    if (dados[7] == Convert.ToString(cbbVd.SelectedItem))
                    {
                        dt.Rows.Add(linha.Split('|'));
                    }                   
                }
            } while (!sr.EndOfStream);
            dgvVeiculo.DataSource = dt;
            sr.Close();

        }
    }
}
