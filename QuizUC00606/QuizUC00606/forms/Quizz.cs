using QuizUC00606.models;
using QuizUC00606.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QuizUC00606.forms
{
    public partial class Quizz : Form
    {
        private List<Pergunta> perguntasSelecionadas = new List<Pergunta>();
        private int indiceAtual = 0;
        private int counterTotal = 0;
        private int counterCertas = 0;
        private int dificuldadeAtual = 1;

        public XDocument doc = XDocument.Load(GlobalUtils.caminho);

        public Quizz()
        {
            InitializeComponent();
            //GlobalUtils.LoadXML(); atualmente obsoleto

            // verificar se existe perguntas suficientes neste XML, se não houver, mostra mensagem e volta para a pagina inicial
            if (!ValidarPerguntasDisponiveis())
            {
                this.Hide();

                // Leva para o Form Quizz
                using (var inicio = new Inicial())
                {
                    inicio.ShowDialog();
                }
                return;
            }

            
           
            SelecionarNovoConjuntoPerguntas(); // Filtro de perguntas para apenas ter a dificuldade e categoria pretendidas
            MostrarPergunta(); // Mostra as perguntas nos botões correspondentes
            
            // Carregando em algum dos botões leva para o metodo VerificarReposta, nesse metodos é que atribui valor aos botões.
            btn_A.Click += VerificarResposta;
            btn_B.Click += VerificarResposta;
            btn_C.Click += VerificarResposta;
            btn_D.Click += VerificarResposta;
        }

        //
        private bool ValidarPerguntasDisponiveis()
        {
            // // Este metodo não verifica se existem pelo menos 5 perguntas válidas por cada nivel
            //if (GlobalUtils.categoria == "Misto")
            //{
            //    return perguntasSelecionadas.Any(p => p.dificuldade >= dificuldadeAtual && p.dificuldade <= 3) && GlobalUtils.perguntas.Count >= 5;
            //}
            //else
            //{
            //    return perguntasSelecionadas.Any(p => p.dificuldade >= dificuldadeAtual && p.dificuldade <= 3 && p.categoria == GlobalUtils.categoria) && GlobalUtils.perguntas.Count >= 5;
            //}

            // Verificar com cada nivel disponivel
            for (int nivel = 1; nivel <= 3; nivel++)
            {
                int perguntasValidas;

                // Verificar por categoria (se for misto não filtra) e por quantidade de perguntas por nivel de dificuldade
                if (GlobalUtils.categoria == "Misto")
                {
                    perguntasValidas = GlobalUtils.perguntas.Count(p => p.dificuldade == nivel);
                }
                else
                {
                    perguntasValidas = GlobalUtils.perguntas.Count(p => p.dificuldade == nivel && p.categoria == GlobalUtils.categoria);
                }

                if (perguntasValidas < 5)
                {
                    // Se algum nível não tiver 5 ou mais perguntas, a validação falha e envia mensagem "personalizada" para ser mais facil de identificar o erro
                    MessageBox.Show("Pergunta da categoria: " + GlobalUtils.categoria + " apenas tem " + perguntasValidas + " perguntas válidas", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true; // Todos os níveis têm pelo menos 5 perguntas
        }


        // Metodo para filtrar as perguntas consoante a dificulade e a categoria se for selecionada alguma em especifico e ainda coloca as perguntas em aleatório
        private void SelecionarNovoConjuntoPerguntas()
        {
            Random aleatorio = new Random();

            if (GlobalUtils.categoria == "Misto")
            {
                perguntasSelecionadas = GlobalUtils.perguntas.Where(p => p.dificuldade == dificuldadeAtual).OrderBy(x => aleatorio.Next()).Take(5).ToList();
            }
            else
            {
                perguntasSelecionadas = GlobalUtils.perguntas.Where(p => p.dificuldade == dificuldadeAtual && p.categoria == GlobalUtils.categoria).OrderBy(x => aleatorio.Next()).Take(5).ToList();
            }
            indiceAtual = 0;
        }

        //public void LoadXML() // Tinha sido colocado aqui, mas houve a necessidade de coloca-lo nos globais para correr entre os Forms
        //{
        //    // Carrega apenas do caminho ESPECIFICADO
        //    XDocument doc = XDocument.Load(GlobalUtils.caminho);

        //    foreach (var categoriaNode in doc.Descendants("categoria"))
        //    {
        //        string nomeCategoria = categoriaNode.Attribute("nome")?.Value ?? "Desconhecida";

        //        foreach (var nivelNode in categoriaNode.Elements("nivel"))
        //        {
        //            int dificuldade = int.Parse(nivelNode.Attribute("numero")?.Value ?? "0");

        //            foreach (var perguntaNode in nivelNode.Elements("pergunta"))
        //            {
        //                int id = int.Parse(perguntaNode.Element("id")?.Value ?? "0");
        //                string texto = perguntaNode.Element("texto")?.Value ?? "";

        //                List<string> respostas = perguntaNode.Element("respostas")?.Elements("resposta").Select(r => r.Value).ToList() ?? new List<string>();

        //                int indexCorreta = int.Parse(perguntaNode.Element("respostaCorretaIndex")?.Value ?? "0");

        //                perguntas.Add(new Pergunta(id, texto,nomeCategoria,respostas, dificuldade, indexCorreta));
        //            }
        //        }
        //    }
        //}

        // Mostra a pergunta, colocando o texto da pergunta e as respostas nos respetivos textos de botões
        private void MostrarPergunta()
        {
            if (indiceAtual < perguntasSelecionadas.Count)
            {
                var pergunta = perguntasSelecionadas[indiceAtual];

                btn_pergunta.TextButton = pergunta.pergunta;


                btn_A.TextButton = pergunta.respostas[0];
                btn_B.TextButton = pergunta.respostas[1];
                btn_C.TextButton = pergunta.respostas[2];
                btn_D.TextButton = pergunta.respostas[3];

                listBox1.Items.Add(perguntasSelecionadas[indiceAtual].ToString()); // Teste para verificar se está tudo certo
            }
        }

        private void VerificarResposta(object sender, EventArgs e)
        {
            Button botaoClicado = sender as Button;
            int respostaEscolhida = -1;

            if (sender == btn_A)
                respostaEscolhida = 0;
            else if (sender == btn_B)
                respostaEscolhida = 1;
            else if (sender == btn_C)
                respostaEscolhida = 2;
            else if (sender == btn_D)
                respostaEscolhida = 3;

            Pergunta perguntaAtual = perguntasSelecionadas[indiceAtual];
            bool acertou = respostaEscolhida == perguntaAtual.indexRespostaCorreta;

            if (acertou)
            {
                counterCertas++;
                counterTotal++;
                MessageBox.Show("Resposta correta!", "✅", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Resposta errada!\nCorreta: {perguntaAtual.ObterRespostaCorreta()}", "❌", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            indiceAtual++;

            if (indiceAtual < perguntasSelecionadas.Count)
            {
                MostrarPergunta();
            }
            else
            {
                ProcessarFinalRonda();
            }
        }

        private void ProcessarFinalRonda()
        {
            if (counterCertas >= 4 && dificuldadeAtual < 3)
            {
                dificuldadeAtual++;
                MessageBox.Show($"Acertos: {counterCertas}/5 - Avançando para nível {dificuldadeAtual}!", "👏", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                counterCertas = 0;
                SelecionarNovoConjuntoPerguntas();

                if (perguntasSelecionadas.Count > 0)
                {
                    MostrarPergunta();
                }
                else
                {
                    MessageBox.Show("Não há mais perguntas para este nível", "Fim do Quiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                if (dificuldadeAtual == 3)
                {
                    MessageBox.Show($"Parabéns! Você completou todos os níveis!\nAcertos totais: {counterTotal}/15", "🏆 Fim do Jogo 🏆", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Fim do quiz! Acertos totais: {counterTotal}/15",
                                    "Fim do Quiz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Inicial formInicial = new Inicial();

                if (formInicial != null)
                {
                    formInicial.Show(); 
                }
                else
                {
                    new Inicial().Show();
                }

                this.Close();

            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Inicial formInicial = new Inicial();

            if (formInicial != null)
            {
                formInicial.Show();  
            }
            else
            {
                
                new Inicial().Show();
            }

            this.Close();

        }
    }
    
}
