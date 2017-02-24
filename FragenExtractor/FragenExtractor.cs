using BS14Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FragenExtractor
{
    class FragenExtractor
    {
        static void Main(string[] args)
        {
            debug();
            return;

#if DEBUG
            args = new string[] { @"C:\Users\miles.sasportas\Dropbox\Ausbildungssachen\Moodle Klausuren\OuG 02\OuG 02 erweitert mit Netzplan\KA OuG 02 erweitert mit Netzplan.htm" };
#endif
            //check if there are arguments
            if(args.Length == 0)
            {
                System.Console.WriteLine("Bitte Pfad zum htm(l) dokument als Startparameter anfügen.");
                BS14Library.Console.PrintExitText();
                return;
            }            
                
            //read in the file
            var file = File.ReadAllText(args[0]);

            //get the main part wehere all infos are nested
            string mainInformation = System.IO.File.ReadAllText(@"C:\Users\miles.sasportas\Desktop\TestHTML.htm");  // TODO for testingHtmlParsing.GetElementValue(file, "div", "role=\"main\"");
            //almost 2 mins for the main infor to be parsed
        //TODO irgnore for now    string info = HtmlParsing.GetElementValue(mainInformation, "table", "class=\"generaltable generalbox quizreviewsummary\"");
            //ca 1 min 10 sekunden für den hier 
            var questions = HtmlParsing.GetElementValue(mainInformation, "form", HtmlParsingOptions.UnsureAttributeValues, "id", "class", "action", "method", "autocomplete");
            //this step rouhly takes 66 seconds


        }

        private static void debug()
        {
            var files = System.IO.Directory.GetFiles(@"C:\Users\miles.sasportas\Desktop\BS Html\quesions");
            var dir = @"C:\Users\miles.sasportas\Desktop\BS Html\quesions\aufgedroselt\";
            foreach (string file in files)
            {
                string html = System.IO.File.ReadAllText(file);
                string frage, answerBlock, richtigeAntwort;
                try
                {
                    //if it's a normal quesiton 
                    frage = HtmlParsing.GetElementValue(html, "div", "class=\"qtext\"");
                    answerBlock = HtmlParsing.GetElementValue(html, "div", "class=\"ablock\"");
                    richtigeAntwort = HtmlParsing.GetElementValue(html, "div", "class=\"rightanswer\"");
                }
                catch (Exception)
                {
                    //happens on atleast a dorpdown
                    frage = "Todo Dropdown"; answerBlock = "TODO Dropdown"; richtigeAntwort = "TODO Dropdown";                    
                }
                string f = System.IO.Path.GetFileNameWithoutExtension(file) + ".htm";
                File.WriteAllText(dir + f, "Fragen:\r\n\r\n" + frage + "\r\nAntworten\r\n\r\n" + answerBlock + "\r\nRichtige Antwort\r\n\r\n" + richtigeAntwort);
            }            
        }
    }
}
