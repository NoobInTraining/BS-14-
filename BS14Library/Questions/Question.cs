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
        #region [ Blaupause ]
        #endregion


        #region [ Constructors ]
        
        public Question(int questionNo, QuestionType type)
        {
            this.QuestionNumber = questionNo;
            this.QuestionType = type;
        }

        #endregion

        #region [ Props ]

        /// <summary>
        /// The Number of the Question
        /// </summary>
        public int QuestionNumber { get; private set; }

        /// <summary>
        /// The Type of the Question
        /// </summary>
        public QuestionType QuestionType { get; private set; }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Converts this class to an XML-Store Format
        /// </summary>
        /// <returns></returns>
        public string ToXML()
        {
            throw new NotImplementedException();
        }

        #region [ static Methods ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isPath"></param>
        /// <returns></returns>
        public Question FromXML(string filePath, bool isPath = true)
        {
            throw new NotImplementedException();
        }

        #endregion


        #endregion

    }
}
