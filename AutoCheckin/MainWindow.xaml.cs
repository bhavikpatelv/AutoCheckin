using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
                image1.Source =  new BitmapImage(new Uri(myFile.FileName));
            }
            Bitmap bmpImage = new Bitmap(myFile.FileName);
            var ocr = new TesseractEngine(@"C:\Users\Bhavik Patel\Documents\visual studio 2015\Projects\AutoCheckin\AutoCheckin\tessdata", "eng", EngineMode.Default);
            var page = ocr.Process(bmpImage, PageSegMode.Auto);
            var text = page.GetText();
            FileInfo fi = new FileInfo(myFile.FileName);
            nameTextBox.Text = text;
        }
    }
}
