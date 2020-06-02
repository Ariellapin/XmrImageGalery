using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ImageGalery.ViewModels
{
    public class ChooseFileViewModal : BaseViewModel
    {
        public ChooseFileViewModal()
        {
            Title = "Choose 111";
            OpenWebCommand = new Command(async () => await OpenFile());
        }

        private async Task OpenFile() {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return ; // user canceled file picking

                string fileName = fileData.FileName;
                string contents = System.Text.Encoding.UTF8.GetString(fileData.DataArray);

                Console.WriteLine("File name chosen: " + fileName);
                Console.WriteLine("File data: " + contents);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception choosing file: " + ex.ToString());
            }
        }
        public ICommand OpenWebCommand { get; }
    }
}