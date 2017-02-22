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
    }
}
