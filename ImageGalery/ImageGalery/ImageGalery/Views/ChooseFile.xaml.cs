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
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking

                FileInfo fi = new FileInfo(fileData.FilePath);
                var files = fi.Directory.GetFiles("*.jpg", SearchOption.AllDirectories).Select(f=>new {date=f.LastWriteTime,name=f.Name });
                Console.WriteLine(files.Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception choosing file: " + ex.ToString());
                await DisplayAlert("Alert", "Exception choosing file: " + ex.ToString(), "OK");
            }
        }
    }
}