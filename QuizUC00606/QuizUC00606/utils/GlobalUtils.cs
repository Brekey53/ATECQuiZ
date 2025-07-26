using QuizUC00606.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuizUC00606.utils
{
    internal class GlobalUtils
    {
        public static string categoria = "";
        public static string caminho = @"G:\GitHub\C#\UC00606\ATECQuiZ\QuizUC00606\perguntas.xml";
        public static List<Pergunta> perguntas = new List<Pergunta>(); // Está global para evitar fazer tantos LoadXML() e declarar varias vezes a classe

        // Metodo para carregar as peguntas do XML para a classe Pergunta
        public static void LoadXML()
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
