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
    public partial class Locacao : Form
    {
        public Locacao()
        {
            InitializeComponent();
            int codigo;
            codigo = CodigoLocacao();
            lblCodL.Text = Convert.ToString(codigo);
        }
        public static int CodigoLocacao()
        {
            int codigo;

            FileStream fs = new FileStream("Locacao.txt", FileMode.OpenOrCreate);
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

        public static int VerificarCodigoVeiculo(int cVeiculo)
        {
            FileStream fs = new FileStream("Veiculos.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            string linha;
            string[] dados;

            bool v = false;

            do
            {
                linha = sr.ReadLine();
                if (linha != null)
                {
                    dados = linha.Split('|');
                    //Confirmar se o código existe e se o veículo encontrase disponivel para locação
                    if (dados[0] == (Convert.ToString(cVeiculo)) && dados[7] == Convert.ToString('D'))
                    {
                        v = true;
                        sr.Close();
                    }
                }
            } while (linha != null && v != true);
            sr.Close();
            if (v == true)
            {
                return cVeiculo;
            }
            else
            {
                return -1;
            }
        }
        public static int Dias(DateTime dR, DateTime dD)
        {
            TimeSpan date = dD - dR;
            int dia = date.Days;

            return dia;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            int cLoc = CodigoLocacao();

            FileStream fs = new FileStream("Locacao.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            DateTime dR = Convert.ToDateTime(dtpR.Value);           
            DateTime dD = Convert.ToDateTime(dtpE.Value);
            int dias;
            dias = Dias(dR,dD); 
            lblDias.Text = Convert.ToString(dias);
           
            double vSeguro;
            int cCliente;
            int cVeiculo;

            cCliente = Convert.ToInt16(lblCLiente.Text);
            cVeiculo = Convert.ToInt16(lblCodVeiculo.Text);                    
            vSeguro = Convert.ToDouble(cbbSeg.SelectedItem);

            if (dR.Hour >= 8 && dR.Hour <= 18)
            {
                if (dD.Hour >= 8 && dD.Hour <= 18)
                {
                    if (dD.Date >= dR.Date )
                    {
                        if (cCliente > 0)
                        {
                            if (cVeiculo > 0)
                            {
                                //É uma classe utilizada para concatenar variaveis , sem que tenhamos que 
                                //criar um outro arquivo                           
                                StringBuilder sb = new StringBuilder();

                                int vcv;
                                vcv = VerificarCodigoVeiculo(cVeiculo);
                                StreamReader sr = new StreamReader("Veiculos.txt");

                                if (vcv != -1)
                                {
                                    string linha;
                                    string[] dados;
                                    do
                                    {
                                        linha = sr.ReadLine();
                                        dados = linha.Split('|');
                                        if (linha != null)
                                        {
                                            if (Convert.ToString(cVeiculo) == dados[0])
                                            {
                                                //Utilizeo o replace para modificar o campo desejado
                                                linha = linha.Replace("D", "A");
                                            }
                                            //appendline grava a sequencia modificada e também as outras linhas em cache.
                                            sb.AppendLine(linha);
                                        }

                                    } while (!sr.EndOfStream);
                                    sr.Close();
                                    StreamWriter sw1 = new StreamWriter("Veiculos.txt");
                                    sw1.Write(sb);
                                    sw1.Close();
                                    this.Close();
                                    sw.WriteLine(cLoc + "|" + dR + "|" + dD + "|" + vSeguro + "|" + dias + "|" + cCliente + "|" + cVeiculo);
                                    MessageBox.Show("Cadastrado com Sucesso!");
                                }

                                else
                                {
                                    MessageBox.Show("Veículo indisponível!");
                                    sw.Close();
                                }

                                sw.Close();
                                sr.Close();
                            }
                            else
                            {
                                MessageBox.Show("Veículo inválido!");
                                sw.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cliente inválido!");
                            sw.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Data de entrega inferior a data de retirada!");
                        sw.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Data de entrega inválida!");
                    sw.Close();
                }
            }
            else
            {
                MessageBox.Show("Data de retirada inválida!");
                sw.Close();
            }          
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesqC_Click(object sender, EventArgs e)
        {

            FileStream fsCR = new FileStream("Clientes.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsCR);

            string Nome = txtNome.Text;
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
                    }
                }
            } while (!sr.EndOfStream);            
            dgvCliente.DataSource = dt;        
            sr.Close();
        }

        private void btnPesqV_Click(object sender, EventArgs e)
        {
            FileStream fsCR = new FileStream("Veiculos.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsCR);

            int Ocup = Convert.ToInt16(cbbOcup.SelectedItem);
            DataTable dt = new DataTable();
            dt.Columns.Add("Código");
            dt.Columns.Add("Descrição");
            dt.Columns.Add("Modelo");
            dt.Columns.Add("Cor");
            dt.Columns.Add("Placa");
            dt.Columns.Add("Diária");
            dt.Columns.Add("Ocupações");
            dt.Columns.Add("Status");

            string linha;
            string[] dados;
            do
            {
                linha = sr.ReadLine();
                if (linha != null)
                {
                    dados = linha.Split('|');
                    if (Convert.ToInt16(dados[6]) == Ocup)
                    {
                        dt.Rows.Add(linha.Split('|'));
                    }
                }
            } while (!sr.EndOfStream);
            dgvVeiculo.DataSource = dt;
            sr.Close();
        }

        private void dgvCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FileStream fsCR = new FileStream("Clientes.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsCR);
            lblCLiente.Text =dgvCliente.CurrentRow.Cells[0].Value.ToString();
            sr.Close();
        }

        private void dgvVeiculo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FileStream fsCR = new FileStream("Veiculos.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsCR);
            lblCodVeiculo.Text = dgvVeiculo.CurrentRow.Cells[0].Value.ToString();
            sr.Close();
        }

        private void lblDias_Click(object sender, EventArgs e)
        {
        }

        private void cbbSeg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblCLiente_Click(object sender, EventArgs e)
        {

        }

        private void Locacao_Load(object sender, EventArgs e)
        {
           
        }
    }
}
