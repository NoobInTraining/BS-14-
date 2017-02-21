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
        #region [ private fields ]

        /// <summary>
        /// This string contains all character on my keyboard. --> does the same thing as . but . is bugged someimtes.
        /// </summary>
        static private string regexPatternAllCharacters = "\\w\\d\\s.:|<>\\-_,;#'*+~`´?ß\\\\})(/{&%$\"!°^";
       
        #endregion

        #region [ static methhods ]
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
        /// //TODO: Propper erordcumentation
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
            
            #region [ create the regex ]
            //create the element
            StringBuilder regexBuilder = new StringBuilder("<");
            regexBuilder.Append(element);
            if(completeAttribute.Length != 0)
                regexBuilder.Append(@"\s+");
            //initiate there might be following attributes
            if (completeAttribute.Length != 0)
            {                
                regexBuilder.Append("(");
                foreach (var s in completeAttribute)
                {
                    regexBuilder.Append("(");
                    regexBuilder.Append(s);
                    regexBuilder.Append(" *)");
                    regexBuilder.Append("|");
                }
                //remove the last |
                regexBuilder.Remove(regexBuilder.Length - 1, 1);
                //min/only (not sure) amount of those attributes
                regexBuilder.Append("){");
                regexBuilder.Append(completeAttribute.Length);
                regexBuilder.Append("}");
                regexBuilder.Append(@"\s?");
            }

            //close it and save the pattern for easy use lateron
            regexBuilder.Append(">");            
            var regexPatternIgnoreCase = new Regex(regexBuilder.ToString(), RegexOptions.IgnoreCase);            
            #endregion

            //Now we have the Regex which we are looking for, so we can go ahead and get that sweet seet first index
            if (!regexPatternIgnoreCase.IsMatch(html))
                throw new Exception("Does not contain the Element in combination with the attributes");
                //TODO: Propper exeption            
                       
            //we take everything after the opnening tag
            string evrythingAfter = Regex.Replace(html, ".*" + regexBuilder.ToString(), "", RegexOptions.Singleline);
            //Now we check if the rest has another opening tag
            if (!evrythingAfter.Contains("<" + element))            
                //if it doesn't then we can just replace everyting after the closing tag 
                return Regex.Replace(evrythingAfter, "</" + element + ">.*", "", RegexOptions.Singleline);

            //now we know there are more closing tags, we have to find the right closing tag
            int closeAmount = 0;
            //as soon as the clsoe amount is -1, we know where the end is 
            var splittedByGreater = evrythingAfter.Split('<').ToList();
            for (int i = 0; i < splittedByGreater.Count; i++)
            {
                if (splittedByGreater[i].StartsWith(element))
                    closeAmount++;
                else if (splittedByGreater[i].StartsWith("/" + element))
                    closeAmount--;
                //now that we found the first closing tag which is to much we can build the value
                if(closeAmount == -1)
                {
                    //And the starts at index 0 and goes to index i - 1, becuase index i was the closing one 
                    StringBuilder value = new StringBuilder();
                    for (int j = 0; j < i; j++)                    
                        value.Append(splittedByGreater[j] == ""? "" : "<" + splittedByGreater[j]);
                    return value.ToString();                    
                }
            }

            throw new Exception("Did not find Value"); //Todo propper exception
        }

        /// <summary>
        /// Get the Value behind from en XML Element (&lt;Element&gt;Value&lt;/Element&gt;)
        /// The functions finds Elemts with atleast passed amounts of attributes (and).
        /// </summary>
        /// <param name="html">The HTML code to be analysed</param>
        /// <param name="element">The name of the HTML Element</param>
        /// <param name="options">The Options to parse the element</param>
        /// <param name="attributes">The attributes further specified by the HtmlParsingOptions</param>
        /// <returns>The Value of the Elemt</returns>
        /// <example>
        /// string mainPart = GetValue("div", "class=\"main\");
        /// </example>
        /// //TODO: Propper erordcumentation
        public static HTMLObject[] GetElementValue(string html, string element, HtmlParsingOptions options, params string[] attributes)
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

            switch (options)
            {
                case HtmlParsingOptions.UnsureAttributeValues: return parseUnsureAttributes(html, element, attributes);
                default: throw new NotImplementedException("This option has not been implemented yet");
            }
        }

        /// <summary>
        /// Helper method for public static HTMLObject[] GetElementValue(string html, string element, HtmlParsingOptions options, params string[] attributes)
        /// </summary>
        private static HTMLObject[] parseUnsureAttributes(string html, string element, string[] incompleteAttributes)
        {
            #region [ create the regex ]
            //create the element
            StringBuilder regexBuilder = new StringBuilder("<");
            regexBuilder.Append(element);
            if (incompleteAttributes.Length != 0)
                regexBuilder.Append(@"\s+");
            //initiate there might be following attributes
            if (incompleteAttributes.Length != 0)
            {
                regexBuilder.Append("(");
                foreach (var s in incompleteAttributes)
                {
                    regexBuilder.Append("(");
                    regexBuilder.Append(s);
                    regexBuilder.Append("=\"[");
                    regexBuilder.Append(regexPatternAllCharacters);
                    regexBuilder.Append("]+\" *)");
                    regexBuilder.Append("|");
                }
                //remove the last |
                regexBuilder.Remove(regexBuilder.Length - 1, 1);
                //min/only (not sure) amount of those attributes
                regexBuilder.Append("){");
                regexBuilder.Append(incompleteAttributes.Length);
                regexBuilder.Append("}");
                regexBuilder.Append(@"\s?");
            }

            //close it and save the pattern for easy use lateron
            regexBuilder.Append(">");
            string regexPattern = regexBuilder.ToString();
            var regexPatternIgnoreCase = new Regex(regexBuilder.ToString(), RegexOptions.IgnoreCase);
            #endregion

            //Now we have the Regex which we are looking for, so we can go ahead and get that sweet seet first index
            if (!regexPatternIgnoreCase.IsMatch(html))
                throw new Exception("Does not contain the Element in combination with the attributes");
            //TODO: Propper exeption  

            //get the matching attribut sentencens
            var matches = Regex.Matches(html, regexPattern);

            HTMLObject[] response = new HTMLObject[matches.Count];
            int index = 0;
            //Now we can parse out the  attributes
            foreach (Match match in matches)
            {
                //get the tags of that element
                string tagWithAttributes = match.Value;

                //instantiate the new objec
                response[index] = new HTMLObject{ Tag = tagWithAttributes, Element = element };
                var theObject = response[index];                                
                
                //now that we have the tags with attributes it is easy to filter out the tags
                foreach (var passedAttribte in incompleteAttributes)
                {
                    string value = Regex.Replace(tagWithAttributes, ".+" + passedAttribte + "=\"","", RegexOptions.IgnoreCase);
                    value = Regex.Replace(value, "\".+", "", RegexOptions.Singleline);
                    theObject.Attributes.Add(passedAttribte, value);
                }

                string[] paramsForFunc = new string[theObject.Attributes.Count];
                //now we can build an array to call the first method here to get the specific value of this tag
                int i = 0;
                foreach (var item in theObject.Attributes)                
                    paramsForFunc[i++] = item.Key + "=\"" + item.Value +"\"";

                //now we can call the main function
                theObject.Value = GetElementValue(html, element, paramsForFunc);
                
                index++;
            }

            //now that we are finished we can return the object
            return response;      
        }

        #endregion

        #region [ nested classes ]

        /// <summary>
        /// Class representing an HTML object
        /// </summary>
        public class HTMLObject
        {
            /// <summary>
            /// The complete HTML tag of 
            /// </summary>
            public string Tag { get; internal set; }

            /// <summary>
            /// The Element (e.g. div) of the HTML Tag
            /// </summary>
            public string Element { get; internal set; }

            /// <summary>
            /// Attributes with their corresponding Value
            /// </summary>
            public Dictionary<string, string> Attributes { get; internal set; }

            /// <summary>
            /// The Value of the HTML Object
            /// </summary>
            public string Value { get; internal set; }

            internal HTMLObject()
            {
                Tag = "";
                Attributes = new Dictionary<string, string>();
                Value = "";
            }

        }

        #endregion
    }

    /// <summary>
    /// Options to use when parsing HTML code
    /// </summary>
    public enum HtmlParsingOptions
    {
        /// <summary>
        /// if you are not sure if the element has attribues
        /// </summary>
        UnsureIfAttrbuteExists,
        /// <summary>
        /// If you know what attributes it has, but not wich values the individual attribute has.
        /// </summary>
        UnsureAttributeValues
    }
}
