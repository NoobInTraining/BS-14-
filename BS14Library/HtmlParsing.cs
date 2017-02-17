using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BS14Library
{
    /// <summary>
    /// Class utelising HTML code parsing 
    /// </summary>
    public class HtmlParsing
    {
        /// <summary>
        /// Get the Value behind from en XML Element (&lt;Element&gt;Value&lt;/Element&gt;)
        /// The functions finds Elemts with atleast passed amounts of attributes (and).
        /// </summary>
        /// <param name="html">The HTML code to be analysed</param>
        /// <param name="element">The name of the HTML Element</param>
        /// <param name="completeAttribute">Complete attributes (with the values for the attribues)</param>
        /// <returns>The Value of the Elemt</returns>
        /// <example>
        /// string mainPart = GetValue("div", "class=\"main\");
        /// </example>
        public static string GetElementValue(string html, string element, params string[] completeAttribute)
        {
            //replace any < that might have been passed
            Func<string, string> replaceGreaterSymbols = new Func<string, string>((input) =>
            {
                if (input.StartsWith("<"))
                    input = input.Remove(0);
                if (input.EndsWith(">"))
                    input = input.Remove(input.Length - 1);
                return input.Trim();
            });
            element = replaceGreaterSymbols(element);

            /*
             <HTML >
                <HTML >
                </HTML>
             </HTML>
             */

            #region [ create the regex ]
            //create the element
            StringBuilder regexBuilder = new StringBuilder("<");
            regexBuilder.Append(html);
            regexBuilder.Append(@"\s?");
            //initiate there might be following attributes
            if (completeAttribute.Length != 0)
            {                
                regexBuilder.Append("(");
                foreach (var s in completeAttribute)
                {
                    regexBuilder.Append("(");
                    regexBuilder.Append(s);
                    regexBuilder.Append(" ?)|");                    
                }
                //min/only (not sure) amount of those attributes
                regexBuilder.Append("){");
                regexBuilder.Append(completeAttribute.Length);
                regexBuilder.Append("}");
                regexBuilder.Append(@"\s?");
            }

            //close it and save the pattern for easy use lateron
            regexBuilder.Append(">");            
            var regexPattern = new Regex(regexBuilder.ToString(), RegexOptions.IgnoreCase);
            #endregion

            //Now we have the Regex which we are looking for, so we can go ahead and get that sweet seet first index
            if (!regexPattern.IsMatch(html))
                throw new Exception("Does not contain the Element in combination with the attributes");
            //TODO: Propper exeption



            //Split the HTML Document apropriatel, the first index is rubish, the 2nd index is the real text generally sping
            var t = regexPattern.Split(html);
            string data = t[1]; //TODO trycatch?


            return data;
        }
    }
}
