using SLIES.Domain.Entities.ConfigurationE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.ConfigurationDTOs
{
    public class FrequentlyQuestionsDTOs
    {
        public int id { get; set; }
        public string pregunta { get; set; }
        public string respuesta { get; set; }
        public bool activo { get; set; }


        public static FrequentlyQuestionsDTOs CreateDTO(FrequentlyQuestionsE frequentlyQuestionsE)
        {
            FrequentlyQuestionsDTOs frequentlyQuestionsDTOs = new()
            {
                id = frequentlyQuestionsE.id_frequently_questions,
                pregunta = frequentlyQuestionsE.s_question,
                respuesta = frequentlyQuestionsE.s_answer,
                activo = frequentlyQuestionsE.byte_active
            };
            return frequentlyQuestionsDTOs;
        }

        public static FrequentlyQuestionsE CreateE(FrequentlyQuestionsDTOs frequentlyQuestionsDTOs)
        {
            FrequentlyQuestionsE frequentlyQuestionsE = new()
            {
                id_frequently_questions = frequentlyQuestionsDTOs.id,
                s_question = frequentlyQuestionsDTOs.pregunta,
                s_answer = frequentlyQuestionsDTOs.respuesta,
                byte_active = frequentlyQuestionsDTOs.activo
            };
            return frequentlyQuestionsE;
        }
    }
}
