using QuizUC00606.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuizUC00606.utils;

namespace QuizUC00606
{
    public partial class Inicial : Form
    {
        public Inicial()
        {
            InitializeComponent();
            cbb_categoria.SelectedIndex = 0;
        }

        private void btn_iniciar_Click(object sender, EventArgs e)
        {
            

            if (cbb_categoria.SelectedItem == null)
            {
                MessageBox.Show("Tem de selecionar uma categoria!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GlobalUtils.categoria = cbb_categoria.SelectedItem.ToString();

            MessageBox.Show("Escolheu a categoria " + GlobalUtils.categoria, "Boa Sorte!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (GlobalUtils.categoria == "História")
            {
                GlobalUtils.categoria = GlobalUtils.categoria.Replace("História", "Historia");
            }

            this.Hide();

            using (var quizz = new Quizz())
            {
                quizz.ShowDialog();
            }


        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
