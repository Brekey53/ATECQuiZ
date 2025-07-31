using AxWMPLib;
using QuizUC00606.models;
using QuizUC00606.utils;
using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

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

        private void Quizz_Load(object sender, EventArgs e)
        {
            playerVideo.URL = @"G:\GitHub\C#\UC00606\ATECQuiZ\QuizUC00606\video.mp4";
            playerVideo.settings.autoStart = true;
            playerVideo.uiMode = "none";
            playerVideo.stretchToFit = true;

            playerVideo.settings.volume = 10; // Colocar o volume do video a 30%

            playerVideo.Ctlcontrols.play();
        }


        private void playerVideo_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 8) // Quando o vídeo termina o state fica como 8
            {
                playerVideo.Visible = false;
            }
        }


        public Quizz()
        {
            InitializeComponent();

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

                //listBox1.Items.Add(perguntasSelecionadas[indiceAtual].ToString()); // Teste para verificar se está tudo certo
            }
        }

        // metodo com a logica de verificação de resposta certa
        private async void VerificarResposta(object sender, EventArgs e)
        {
            // como entra neste metodo após um click no botão, é possivel fazer esta conversão de sender as Button, o botão clicado fica com o valor atribuido nem baixo e os restantes retornam null
            CyberButton botaoClicado = sender as CyberButton;
            int respostaEscolhida = -1;

            // atrubuir valores aos botões
            if (sender == btn_A)
            {
                respostaEscolhida = 0;
            }
            else if (sender == btn_B)
            {
                respostaEscolhida = 1;
            }
            else if (sender == btn_C)
            {
                respostaEscolhida = 2;
            }
            else if (sender == btn_D)
            {
                respostaEscolhida = 3;
            }

            // metodo e funcionalidades para colorir o botao carregado e dar um compaso de espera
            ColorirBotao(sender, e, Color.DarkOrange);
            GlobalUtils.playerEspera.Play();
            await Task.Delay(2000);

            Pergunta perguntaAtual = perguntasSelecionadas[indiceAtual];
            bool acertou = respostaEscolhida == perguntaAtual.indexRespostaCorreta;


            if (acertou)
            {
                // Caso a resposta esteja certa, entra neste if e aumenta a váriavel de respostas certas total e respostas certa por nivel
                counterCertas++;
                counterTotal++;
                ColorirBotao(sender, e, Color.ForestGreen);
                await Task.Delay(500);
                GlobalUtils.playerEspera.Stop();
                MessageBox.Show("Resposta correta!", "✅", MessageBoxButtons.OK);
            }
            else
            {
                // Entra aqui caso a resposta esteja errada
                ColorirBotao(sender, e, Color.Firebrick);
                await Task.Delay(500);
                GlobalUtils.playerEspera.Stop();
                MessageBox.Show($"Resposta errada!\nCorreta: {perguntaAtual.ObterRespostaCorreta()}", "❌", MessageBoxButtons.OK);
            }

            // aumenta o indice para passar para a proxima pergunta, quer a resposta anterior esteja certa ou errada
            indiceAtual++;
            ColorirBotao(sender, e, Color.SteelBlue);

            // verificar em que pergunta está, se nao for a quinta irá sempre entrar no metodo MostrarPergunta para mostrar a proxima pergunta, caso seja a quinta irá entrar no metodo ProcessarFinalRonda para saber se irá avançar na dificuldade ou acabar o jogo
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
            // Entra neste if caso a dificulade  seja menor que 3 (o maximo neste contexto), e se acertou mais de 4 perguntas
            if (counterCertas >= 4 && dificuldadeAtual < 3)
            {
                // Sobe a dificuldade e mostra quantas respostas estavam certas
                dificuldadeAtual++;
                MessageBox.Show($"Acertos: {counterCertas}/5 - Avançando para nível {dificuldadeAtual}!", "👏", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Dá reset ao counter das respostas certas por dificuldade 
                counterCertas = 0;

                // Entra no metodo para filtrar as perguntas, agora com o nivel de dificuldade acima
                SelecionarNovoConjuntoPerguntas();

                // Verificação extra no caso de os filtros de perguntas tenham encontrado a
                if (perguntasSelecionadas.Count >= 5)
                {
                    MostrarPergunta();
                }
                else
                {
                    MessageBox.Show("Não há mais perguntas para este nível", "Fim do Quiz", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            else
            {
                // Caso a dificuldade seja = a 3 entra no if e termina o jogo ou se não tiver acertado 4 ou mais perguntas
                if (dificuldadeAtual == 3)
                {
                    MessageBox.Show($"Parabéns! Você completou todos os níveis!\nAcertos totais: {counterTotal}/15", "🏆 Fim do Jogo 🏆", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    QuardarHighScore(counterTotal);
                }
                else
                {
                    MessageBox.Show($"Fim do quiz! Acertos totais: {counterTotal}/15", "🏆 Fim do Jogo 🏆", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    QuardarHighScore(counterTotal);
                }


                // Fecha a janela e abre a inicial
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

        // Fecha a janela e abre a inicial
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Tem a certeza que pretende sair?\n Se continuar o pregresso será perdido.\n", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
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

        // Metodo para guardar a pontuação num ficheiro
        private void QuardarHighScore(int respostasCorretas)
        {
            string username = PedirUsername();

            if (!string.IsNullOrWhiteSpace(username))
            {
                string linha = $"{username}|{respostasCorretas}|{DateTime.Now:yyyy-MM-dd}";

                FileInfo ficheiro = new FileInfo(GlobalUtils.caminhoPontuacao);
                if (!ficheiro.Exists)
                {
                    FileStream fstr = ficheiro.Create();
                    fstr.Close();

                    StreamWriter write = ficheiro.AppendText();
                    write.WriteLine(linha);
                    write.Close();

                }
                else
                {
                    StreamWriter write = ficheiro.AppendText();
                    write.WriteLine(linha);
                    write.Close();
                }

                    MessageBox.Show("Pontuação guardada com sucesso!", "Highscore");
            }
        }


        // Metodo para criar um form pequeno e customizavel apenas para pedir o username
        // https://stackoverflow.com/questions/11854971/how-do-i-programmatically-create-a-windows-form
        private string PedirUsername()
        {
            Form inputForm = new Form();

            inputForm.Width = 300;
            inputForm.Height = 150;
            inputForm.Text = "Parabéns!";
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.StartPosition = FormStartPosition.CenterScreen;
            inputForm.MinimizeBox = false;
            inputForm.MaximizeBox = false;
            inputForm.ControlBox = false;

            // Parametros da label
            Label lbl_text = new Label() { Left = 10, Top = 10, Text = "Insere o teu nome/username:", Font = new Font("Segoe UI", 12, FontStyle.Regular), AutoSize = true };
            TextBox tb_text = new TextBox() { Left = 10, Top = 37, Width = 260, Font = new Font("Segoe UI", 12, FontStyle.Regular), AutoSize = true };
            System.Windows.Forms.Button btn_ok = new System.Windows.Forms.Button() { Text = "OK", Left = 200, Width = 70, Top = 70, DialogResult = DialogResult.OK, Font = new Font("Segoe UI", 14, FontStyle.Regular), AutoSize = true };

            inputForm.Controls.Add(lbl_text);
            inputForm.Controls.Add(tb_text);
            inputForm.Controls.Add(btn_ok);
            inputForm.AcceptButton = btn_ok;

            return inputForm.ShowDialog() == DialogResult.OK ? tb_text.Text.Trim() : "Anonimo";
        }


        // Metodo para colorir o botao carregado
        private void ColorirBotao(object sender, EventArgs e, Color cor)
        {
            CyberButton botaoClicado = sender as CyberButton;

            // Se o clique no botao nao for nulo entra aqui
            if (botaoClicado != null)
            {
                botaoClicado.ColorBackground = cor;

            }

        }
        
    }
    
}
