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
                Console.WriteLine("Bitte Pfad zum htm(l) dokument als Startparameter anfügen.");
                BS14Library.Console.PrintExitText();
                return;
            }            
                
            //read in the file
            var file = File.ReadAllText(args[0]);

            //get the main part wehere all infos are nested
            string mainInformation = BS14Library.HtmlParsing.GetElementValue(file, "div", "role=\"main\"");
            //almost 2 mins for the main infor to be parsed
            string info = BS14Library.HtmlParsing.GetElementValue(mainInformation, "table", "class=\"generaltable generalbox quizreviewsummary\"");
            //ca 1 min 10 sekunden für den hier 
            string questions = BS14Library.HtmlParsing.GetElementValue(mainInformation, "form");



        }
    }
}
