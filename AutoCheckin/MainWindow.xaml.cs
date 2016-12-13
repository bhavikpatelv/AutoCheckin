using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tesseract;

namespace AutoCheckin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string tessaData = @"C:\Users\Bhavik Patel\Documents\visual studio 2015\Projects\AutoCheckin\AutoCheckin\tessdata";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myFile = new OpenFileDialog();
            myFile.InitialDirectory = "c:\\";
            if (myFile.ShowDialog() == true)
            {
                Bitmap mybmp = new Bitmap(myFile.FileName);
                image1.Source = new BitmapImage(new Uri(myFile.FileName));
                var ocr = new TesseractEngine(tessaData, "eng", EngineMode.Default);
                var page = ocr.Process(mybmp, PageSegMode.Auto);
                var text = page.GetText();
                var output = text.Substring(text.IndexOf('"')+1);
                output = Regex.Replace(output, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
                char[] RemoveList = { ',', ' ' };
                Regex regexLC = new Regex(@"[A-Z0-9]*,");
                Regex regexFS = new Regex(@", [A-Z0-9]*");
                Regex regexA = new Regex(@"\n(.*?)*\n");
                Regex regexZ = new Regex(@", [A-Z][A-Z] \d{5}");

                var address = regexA.Match(output).ToString().TrimStart();
                var lastCity = regexLC.Matches(output).Cast<Match>().ToList();
                var firstState = regexFS.Matches(output).Cast<Match>().ToList();
                var firstName = firstState.First().ToString().TrimStart(RemoveList);
                var state = firstState.Last().ToString().TrimStart(RemoveList);
                var lastName = lastCity.First().ToString().TrimEnd(RemoveList);
                var city = lastCity.Last().ToString().TrimEnd(RemoveList);
                var zipCode = regexZ.Match(output).ToString().Substring(5);

                lastNameTextBox.Text = lastName;
                firstNameTextBox.Text = firstName;
                addressTextBox.Text = address;
                cityTextBox.Text = city;
                stateTextBox.Text = state;
                zipCodeTextBox.Text = zipCode;
            }
        }
    }
}
