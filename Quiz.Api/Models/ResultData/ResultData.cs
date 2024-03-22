using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QuizApi.Models
{
    public class ResultData
    {
        public int Score { get; set; }

        public int MaxScore { get; set; }

        public string CorrectAnswer { get; set; }
    }
}
