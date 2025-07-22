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
        
        public XDocument doc = XDocument.Load(GlobalUtils.caminho);

        public List<Pergunta> perguntas = new List<Pergunta>();


        public Quizz()
        {
            InitializeComponent();
            LoadXML();

            Pergunta p = perguntas[0];

            lbl_pergunta.Text = p.pergunta; 
        }

        public void LoadXML()
        {

            XDocument doc;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.perguntas)))
            {
                doc = XDocument.Load(stream);
            }

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

                        List<string> respostas = perguntaNode.Element("respostas")?
                            .Elements("resposta")
                            .Select(r => r.Value)
                            .ToList() ?? new List<string>();

                        int indexCorreta = int.Parse(perguntaNode.Element("respostaCorretaIndex")?.Value ?? "0");

                        perguntas.Add(new Pergunta(id, texto, nomeCategoria, respostas, indexCorreta, dificuldade));
                    }
                }
            }

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Form inicio = new Inicial();
            inicio.Show();
            this.Close();
            
            
        }
    }
}
