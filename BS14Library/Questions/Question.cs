using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS14Library.Questions
{
    /// <summary>
    /// Class Representing a Moodle Quesion from the Berufschule 14 aka ITECH
    /// </summary>
    class Question
    {
        public Question(int questionNo, QuestionType type)
        {
            this.QuestionNumber = questionNo;
            this.QuestionType = type;
        }

        /// <summary>
        /// The Number of the Question
        /// </summary>
        public int QuestionNumber { get; private set; }

        /// <summary>
        /// The Type of the Question
        /// </summary>
        public QuestionType QuestionType { get; private set; }
    }
}
