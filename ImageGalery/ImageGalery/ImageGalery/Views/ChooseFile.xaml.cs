using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImageGalery.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ChooseFile : ContentPage
    {
        public ChooseFile()
        {
            InitializeComponent();
            //OpenFile().Wait();
        }

        

        private async void OpenFile(object sender, EventArgs e)
        {
            
        }
    }
}