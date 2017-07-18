using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppLocaMais
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCdastrarCliente_Click(object sender, EventArgs e)
        {
            fundo cc = new fundo();
            cc.ShowDialog();            
        }

        private void btnCadFunc_Click(object sender, EventArgs e)
        {
            Funcionario funcionario = new Funcionario();
            funcionario.ShowDialog();
        }

        private void btnCadVei_Click(object sender, EventArgs e)
        {
            Veiculo _veiculo = new Veiculo();
            _veiculo.ShowDialog();
        }

        private void btnPesq_Click(object sender, EventArgs e)
        {
        }

        private void btnCadLoc_Click(object sender, EventArgs e)
        {
            Locacao _locacao = new Locacao();
            _locacao.ShowDialog();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void btnBaixa_Click(object sender, EventArgs e)
        {
            BaixarLocacao _BaixarLocacao = new BaixarLocacao();
            _BaixarLocacao.ShowDialog();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFid_Click(object sender, EventArgs e)
        {
            Fidelidade _fidelidade = new Fidelidade();
            _fidelidade.ShowDialog();
        }
    }
}
