using QuizUC00606.forms;
using QuizUC00606.models;
using QuizUC00606.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QuizUC00606
{
    public partial class Inicial : Form
    {
        public SoundPlayer player = new SoundPlayer(@"C:\cometudoperdetudo\espetaculo.wav");
        public XDocument doc = XDocument.Load(GlobalUtils.caminho);

        public Inicial()
        {
            InitializeComponent();
            GlobalUtils.LoadXML(); // Variavel global (na pasta utils -> GlobalUtils) para ler o XML

            LoadComboBox(); // Limpar e atualizar as opções da comboBox
        }

        private void btn_iniciar_Click(object sender, EventArgs e)
        {
            

            if (cbb_categoria.SelectedItem == null)
            {
                MessageBox.Show("Tem de selecionar uma categoria!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Coloca a categoria na string global para depois filtrar as perguntas usando está variavel
            GlobalUtils.categoria = cbb_categoria.SelectedItem.ToString();

            //player.Play();
            
            MessageBox.Show("Escolheu a categoria " + GlobalUtils.categoria, "Boa Sorte!", MessageBoxButtons.OK);
            this.Hide();

            // Leva para o Form Quizz
            using (var quizz = new Quizz())
            {
                quizz.ShowDialog();
            }


        }

        // Função resultante da opção "Sair"
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Fechar completamente a aplicação
            Application.Exit();
        }


        // Função resultante da opção "Intruções"
        private void instrucoesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Escolha uma categoria no ecrã inicial e após isso o quiz irá começar.\nBoa sorte!" +
                "\n\nCaso pretenda fechar a aplicação pode faze-lo nas 'opções' e 'sair'.", "Instruções", MessageBoxButtons.OK);
        }

        // Função resultante da opção "Usar pré-Definido" para carregar o ficheiro base da aplicação
        private void usarPrédefinidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalUtils.caminho = @"G:\GitHub\C#\UC00606\ATECQuiZ\QuizUC00606\perguntas.xml";
            GlobalUtils.LoadXML(); // Fazer leitura do ficheiro após alteração
            LoadComboBox(); // Limpar e carregar dados na ComboBox

            MessageBox.Show("Ficheiro lido com sucesso!", "Sucesso!", MessageBoxButtons.OK);
        }

        // Função resultante da opção "Carregar novo..." para carregar um novo ficheiro XML para ter perguntas diferentes
        private void carregarNovoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Escolher o ficheiro XML";
            openFileDialog1.Filter = "Ficheiro XML|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                GlobalUtils.caminho = openFileDialog1.FileName;
            }
            GlobalUtils.LoadXML(); // Fazer leitura do ficheiro após alteração
            LoadComboBox(); // Limpar e carregar dados na ComboBox

            MessageBox.Show("Ficheiro lido com sucesso!", "Sucesso!", MessageBoxButtons.OK);
        }

        // Função resultante da opção "Exemplo XML" que mostra como o XML deve de ser estruturado para funcionar nesta aplicação
        private void exemploXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("O XML para funcionar corretamente terá de ter este formato:\n\n" +
                "<perguntas>\r\n  <categoria nome=\"Desporto\">\r\n    <nivel numero=\"1\">\r\n      <pergunta>\r\n        <id>1</id>\r\n        <texto>Quantos jogadores formam uma equipa titular de voleibol?</texto>\r\n        <respostas>\r\n          <resposta>5</resposta>\r\n          <resposta>6</resposta>\r\n          <resposta>7</resposta>\r\n          <resposta>8</resposta>\r\n        </respostas>\r\n        <respostaCorretaIndex>1</respostaCorretaIndex>\r\n      </pergunta>\n</perguntas>", "Instruções XML", MessageBoxButtons.OK);
        }

        // Limpar e carregar dados na ComboBox
        private void LoadComboBox()
        {

            var categorias = GlobalUtils.perguntas.Select(p => p.categoria).Distinct().ToList();
            categorias.Insert(categorias.Count, "Misto");

            cbb_categoria.DataSource = null;
            cbb_categoria.DataSource = categorias;

            cbb_categoria.SelectedIndex = 0;
        }
    }
}
