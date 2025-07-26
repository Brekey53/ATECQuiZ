using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizUC00606.models
{
    public class Pergunta
    {
        public int pergunta_id {  get; set; }
        public string pergunta { get; set; }
        public string categoria { get; set; }
        public List<string> respostas { get; set; } = new List<string>();
        public int dificuldade { get; set; }
        public int indexRespostaCorreta {  get; set; }

        public Pergunta(int pergunta_id, string pergunta, string categoria, List<string> respostas, int dificuldade, int indexRespostaCorreta)
        {
            this.pergunta_id = pergunta_id;
            this.pergunta = pergunta;
            this.categoria = categoria;
            this.respostas = respostas;
            this.dificuldade = dificuldade;
            this.indexRespostaCorreta = indexRespostaCorreta;
        }

        public string ObterRespostaCorreta()
        {
            if (indexRespostaCorreta >= 0 && indexRespostaCorreta < respostas.Count)
                return respostas[indexRespostaCorreta];
            return null;
        }

        // Apenas para teste!
        public override string ToString()
        {
            string respostasTexto = string.Join(", ", respostas);
            return $"ID: {pergunta_id} | Pergunta: {pergunta} | Categoria: {categoria} | Dificuldade: {dificuldade} | Respostas: [{respostasTexto}] | Correta: {indexRespostaCorreta}";
        }
    }
}
