using QuizUC00606.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuizUC00606.utils
{
    internal class GlobalUtils
    {
        public static string categoria = "";
        public static string caminho = @"G:\GitHub\C#\UC00606\ATECQuiZ\QuizUC00606\perguntas.xml";
        public static string caminhoPontuacao = @"G:\GitHub\C#\UC00606\ATECQuiZ\QuizUC00606\ranking.txt";
        public static SoundPlayer playerEspetaculo = new SoundPlayer(@"G:\GitHub\C#\UC00606\ATECQuiZ\Imagens\espetaculo.wav");
        public static SoundPlayer playerEspera = new SoundPlayer(@"G:\GitHub\C#\UC00606\ATECQuiZ\Imagens\espera.wav");

        public static List<Pergunta> perguntas = new List<Pergunta>(); // Está global para evitar fazer tantos LoadXML() e declarar varias vezes a classe

        
    }
}
