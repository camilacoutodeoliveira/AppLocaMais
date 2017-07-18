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
    public partial class BaixarLocacao : Form
    {
        public BaixarLocacao()
        {
            InitializeComponent();
        }

        private void btnPesq_Click(object sender, EventArgs e)
        {
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvLoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FileStream fs = new FileStream("Locacao.txt",FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            lblCodL.Text = dgvLoc.CurrentRow.Cells[0].Value.ToString();
            lblS.Text = dgvLoc.CurrentRow.Cells[3].Value.ToString();
            lblV.Text = dgvLoc.CurrentRow.Cells[6].Value.ToString();
            dataD.Text = dgvLoc.CurrentRow.Cells[2].Value.ToString();
            sr.Close();
        }

        private void btnFinal_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("Locacao.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            StringBuilder sb = new StringBuilder();

            FileStream fs1 = new FileStream("Veiculos.txt", FileMode.OpenOrCreate);
            StreamReader sr1 = new StreamReader(fs1);
            StringBuilder sb1 = new StringBuilder();
            double s, v, t, vt;
            int d;

            string linha;
            string[] dados;
            if (lblCodC.Text != "0" && lblV.Text != "0" && lblCodL.Text != "0")
            {
                dataE.Text = Convert.ToString(dtpE.Value);
                DateTime dE = Convert.ToDateTime(dtpE.Value);
                DateTime dD = Convert.ToDateTime(dataD.Text);

                if (dE >= dD)
                {
                    if (dD.Hour >= 8 && dD.Hour <= 18)
                    {
                        if (Convert.ToChar(cbbS.SelectedItem) == 'D' || Convert.ToChar(cbbS.SelectedItem) == 'M')
                        {
                            do
                            {
                                linha = sr.ReadLine();
                                if (linha != null)
                                {
                                    dados = linha.Split('|');
                                    if ((Convert.ToString(lblCodL.Text) == dados[0]))
                                    {
                                        s = Convert.ToDouble(dados[3]);
                                        lblS.Text = Convert.ToString(s);
                                        d = Convert.ToInt16(dados[4]);
                                        lblD.Text = Convert.ToString(d);
                                        TimeSpan date = dE - Convert.ToDateTime(dados[2]);
                                        int da = date.Days;
                                        double atraso;
                                        atraso = (da * 20);
                                        if (Convert.ToDateTime(dados[2]) < dE)
                                        {
                                            lblDa.Text = Convert.ToString(da);
                                            lblM.Text = Convert.ToString(atraso);
                                        }
                                        else
                                        {
                                            lblDa.Text = Convert.ToString(da);
                                            lblM.Text = Convert.ToString(atraso);
                                        }
                                        string linhaV;
                                        string[] dadosV;

                                        do
                                        {
                                            linhaV = sr1.ReadLine();
                                            if (linhaV != null)
                                            {
                                                dadosV = linhaV.Split('|');
                                                if (dadosV[0] == lblV.Text && lblCodL.Text == dados[0])
                                                {
                                                    v = Convert.ToDouble(dadosV[5]);
                                                    lblDiaria.Text = Convert.ToString(v);
                                                    t = (d * v) + s;
                                                    if (da > 0)
                                                    {
                                                        char status = Convert.ToChar(cbbS.SelectedItem);
                                                        vt = (t * 1.05) + atraso;
                                                        lblT.Text = Convert.ToString(vt);
                                                        linhaV = linhaV.Replace('A', status);                                                        MessageBox.Show("Extrato gerado com sucesso!");
                                                        MessageBox.Show("Extrato gerado com sucesso!");

                                                    }
                                                    else
                                                    {
                                                        char status = Convert.ToChar(cbbS.SelectedItem);
                                                        lblT.Text = Convert.ToString(t);
                                                        linhaV = linhaV.Replace('A', status);
                                                        MessageBox.Show("Extrato gerado com sucesso!");

                                                    }
                                                }
                                                sb1.AppendLine(linhaV);
                                            }
                                        } while (!sr1.EndOfStream);
                                    }
                                }

                            } while (!sr.EndOfStream);

                            sr.Close();
                            sr1.Close();

                            StreamWriter sw1 = new StreamWriter("Veiculos.txt");
                            sw1.Write(sb1);
                            sw1.Close();

                        }
                        else
                        {
                            MessageBox.Show("Escolha o novo Status do veículo!");
                            sr.Close();
                            sr1.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Horário inválido");
                        sr.Close();
                        sr1.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Data de entrega inválida");
                    sr.Close();
                    sr1.Close();
                }
            }
            else
            {
                MessageBox.Show("É preciso definir um cliente e uma locação para gerar o extrato!");
                sr.Close();
                sr1.Close();
            }  
        }

        private void btnP_Click(object sender, EventArgs e)
        {
            FileStream fsCR = new FileStream("Clientes.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsCR);

            string Nome = txtNomeC.Text;
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

        private void dgvCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FileStream fsCR = new FileStream("Clientes.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsCR);
            lblCodC.Text = dgvCliente.CurrentRow.Cells[0].Value.ToString();
            lblCLiente.Text = dgvCliente.CurrentRow.Cells[1].Value.ToString();
            sr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("Clientes.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            FileStream fs1 = new FileStream("Locacao.txt", FileMode.OpenOrCreate);
            StreamReader sr1 = new StreamReader(fs1);

            bool cond = false;
            DataTable dt = new DataTable();
            int codC = Convert.ToInt16(lblCodC.Text);
            dt.Columns.Add("Código da Locação");
            dt.Columns.Add("Data de Retirada");
            dt.Columns.Add("Previsão de Devolução");
            dt.Columns.Add("Seguro");
            dt.Columns.Add("Dias");
            dt.Columns.Add("Código do Cliente");
            dt.Columns.Add("Código do Veículo");
            string linhaL;
            string [] dadosL;
            string linhaC;
            string [] dadosC;
            do
            {
                linhaC = sr.ReadLine();
                if (linhaC != null)
                {
                    dadosC = linhaC.Split('|');
                    if (codC == Convert.ToInt16(dadosC[0]))
                    {
                        do
                        {
                            linhaL = sr1.ReadLine();
                            if (linhaL != null)
                            {
                                dadosL = linhaL.Split('|');
                                if (dadosL[5] == dadosC[0])
                                {
                                    dt.Rows.Add(linhaL.Split('|'));
                                    cond = true;
                                }
                            }
                        } while (!sr1.EndOfStream);
                    }
                }

            } while (!sr.EndOfStream);           

            dgvLoc.DataSource = dt;
            sr.Close();
            sr1.Close();
        }

        private void BaixarLocacao_Load(object sender, EventArgs e)
        {

        }
    }
}
