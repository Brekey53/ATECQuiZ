using QuizUC00606.forms;
using QuizUC00606.models;
using QuizUC00606.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        
        public XDocument doc = XDocument.Load(GlobalUtils.caminho);

        public Inicial()
        {
            InitializeComponent();
            LoadXML(); // Variavel global (na pasta utils -> GlobalUtils) para ler o XML

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

            GlobalUtils.playerEspetaculo.Play();
            
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
            MessageBox.Show("Escolha uma categoria no ecrã inicial e após isso o quiz irá começar.\nO quiz consiste em 15 perguntas totais em 3 dificuldades diferentes, sendo que tem 5 perguntas de cada dificuldade. Só se passa para a dificuldade seguinte caso consiga ter pelo menos 4 respostas certas!\nBoa sorte!" +
                "\n\nCaso pretenda fechar a aplicação pode faze-lo nas 'opções' e 'sair'.", "Instruções", MessageBoxButtons.OK);
        }

        // Função resultante da opção "Usar pré-Definido" para carregar o ficheiro base da aplicação
        private void usarPrédefinidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalUtils.caminho = @"G:\GitHub\C#\UC00606\ATECQuiZ\QuizUC00606\perguntas.xml";
            LoadXML(); // Fazer leitura do ficheiro após alteração
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
            LoadXML(); // Fazer leitura do ficheiro após alteração
            LoadComboBox(); // Limpar e carregar dados na ComboBox

            MessageBox.Show("Ficheiro lido com sucesso!", "Sucesso!", MessageBoxButtons.OK);
        }

        // Função resultante da opção "Exemplo XML" que mostra como o XML deve de ser estruturado para funcionar nesta aplicação
        private void exemploXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Para o quiz funcionar corretamente, o XML terá de ter este formato:\n\n" +
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

        private void txtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(GlobalUtils.caminhoPontuacao))
            {
                MessageBox.Show("Ficheiro não encontrado", "Erro!", MessageBoxButtons.OK);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = GlobalUtils.caminhoPontuacao,
                UseShellExecute = true 
            });


        }

        private void mensagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Verificar se o ficheiro existe
            if (!File.Exists(GlobalUtils.caminhoPontuacao))
            {
                MessageBox.Show("O ficheiro de pontuações não existe.");
                return;
            }

            var linhas = File.ReadAllLines(GlobalUtils.caminhoPontuacao);
            
            var listaRanking = new List<(string nome, int pontos, DateTime data)>();

            foreach (var linha in linhas)
            {
                string[] partes = linha.Split('|');
                string nome = partes[0];
                int pontos = int.Parse(partes[1]);
                DateTime data = DateTime.Parse(partes[2]);

                listaRanking.Add((nome, pontos, data));
                
            }
            // Ordenar por ano primeiro (mais velhos primeiro) e depois por pontos para quem tem mais pontos aparecer no topo
            var listaTop25 = listaRanking.OrderBy(x => x.data.Year).OrderByDescending(x => x.pontos).Take(25).ToList();

            // StringBuilder é mais otimizado pois cria um "buffer" de texto na memoria, ao contrario de várias linhas para fazer concatenações, em que cada linha vai alocar espaço na memória, podendo tornar o sistema lento
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("NOME:\t\tPONTUAÇÃO:\tDATA:");

            foreach (var item in listaTop25)
            {
                sb.AppendLine($"{item.nome}\t\t{item.pontos}/15\t{item.data:yyyy-MM-dd}");
            }

            MessageBox.Show(sb.ToString(), "Ranking - Top 25");


        }

        // Metodo para carregar as peguntas do XML para a classe Pergunta
        public void LoadXML()
        {

            XDocument doc = XDocument.Load(GlobalUtils.caminho);

            GlobalUtils.perguntas.Clear();

            foreach (var categoriaNode in doc.Descendants("categoria"))
            {
                string nomeCategoria = categoriaNode.Attribute("nome")?.Value ?? "Desconhecida";

                foreach (var nivelNode in categoriaNode.Elements("nivel"))
                {
                    int dificuldade = int.Parse(nivelNode.Attribute("numero")?.Value ?? "0");

                    foreach (var perguntaNode in nivelNode.Elements("pergunta"))
                    {
                        int id = int.Parse(perguntaNode.Element("id")?.Value ?? "0");
                        string texto = perguntaNode.Element("texto")?.Value ?? "";

                        List<string> respostas = perguntaNode.Element("respostas")?.Elements("resposta").Select(r => r.Value).ToList() ?? new List<string>();

                        int indexCorreta = int.Parse(perguntaNode.Element("respostaCorretaIndex")?.Value ?? "0");

                        GlobalUtils.perguntas.Add(new Pergunta(id, texto, nomeCategoria, respostas, dificuldade, indexCorreta));
                    }
                }
            }
        }
    }
}
