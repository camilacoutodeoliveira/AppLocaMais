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
    public partial class Fidelidade : Form
    {
        public Fidelidade()
        {
            InitializeComponent();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnP_Click(object sender, EventArgs e)
        {

            FileStream fsCR = new FileStream("Clientes.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsCR);

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
                    }
                }
            } while (!sr.EndOfStream);
            dgvC.DataSource = dt;
            sr.Close();

        }

        private void dgvC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FileStream fsCR = new FileStream("Clientes.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fsCR);
            lblCod.Text = dgvC.CurrentRow.Cells[0].Value.ToString();
            lblC.Text = dgvC.CurrentRow.Cells[1].Value.ToString();
            sr.Close();
        }

        private void btnPontos_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("Clientes.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            FileStream fs1 = new FileStream("Locacao.txt", FileMode.OpenOrCreate);
            StreamReader sr1 = new StreamReader(fs1);
            bool cond = false;
            int pontos, total = 0;

            string linhaC;
            string[] dadosC;
            string linhaL;
            string[] dadosL;

            do
            {
                linhaC = sr.ReadLine();
                if (linhaC != null)
                {
                    dadosC = linhaC.Split('|');

                    if (dadosC[1] == lblC.Text)
                    {
                        do
                        {
                            linhaL = sr1.ReadLine();
                            if (linhaL != null)
                            {
                                dadosL = linhaL.Split('|');
                                if (dadosC[0] == dadosL[5])
                                {
                                    pontos = Convert.ToInt16(dadosL[4]) * 10;
                                    total = pontos + total;
                                    lblPontos.Text = Convert.ToString(total);
                                    cond = true;
                                }
                            }
                            if (total >= 500)
                            {
                                lblMsg.Text = "Parabéns você completou 500 pontos, por isso irá ganhar o Kit LocaMais!";
                            }
                            else if (cond == false)
                            {
                              lblMsg.Text = "\n\t Não foram encontradas locações";
                            }
                            else
                            {
                                lblMsg.Text = "Pontos fidelidade igual á " + total;
                            }

                        } while (!sr1.EndOfStream);
                    }
                }

            } while (!sr.EndOfStream);
            sr.Close();
            sr1.Close();
        }
    }
}
