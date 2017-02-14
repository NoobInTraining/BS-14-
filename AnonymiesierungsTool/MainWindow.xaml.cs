using System.Text.RegularExpressions;
using System.Windows;

namespace AnonymiesierungsTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Determing wether the item is allowed to be dropped or not
        /// </summary>
        private bool isAllowed;
        /// <summary>
        /// The filepath of the file that is droped
        /// </summary>
        private string filePath;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        #region [ Eventhandlers ]
        //Using Eventhandlers to first create the logic and then 


        /// <summary>
        /// actually grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_DragEnter(object sender, DragEventArgs e)
        {
            /*
                Schnelle erklärung zu dieser funktion:
                    - e.Data.GetData("Filename") gibt uns ein object zurück, welches wir auf ein typenloses Array casten.
                    - Von diesem Array, nehme wir das erste element (der filepath)
                    - Und giben diesen aus (ToString)
            */
            filePath = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
            //als nächsten schnappen wir uns die Daateiendung.
            string dateiEndung = System.IO.Path.GetExtension(filePath).ToLower();
            

            //überprüfen ob die Dateiendung stimmt
            if (!dateiEndung.Equals(".htm"))
            {
                isAllowed = false;
                //Tell the user that is it no accepted and exit
                MessageBox.Show("Please only drag and drop \".htm\" files", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
                isAllowed = true;

            //es erlauben            
            e.Effects = DragDropEffects.Copy; 
        }


        /// <summary>
        /// actually grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_Drop(object sender, DragEventArgs e)
        {
            //If the drop wasn't allowed
            if (!isAllowed)
                return;
            
            System.Diagnostics.Debugger.Break();
        }



        #endregion
    }
}
