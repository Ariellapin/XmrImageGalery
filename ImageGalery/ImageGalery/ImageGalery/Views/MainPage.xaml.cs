
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ImageGalery.Extensions;

namespace ImageGalery.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage//TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = "Main Page";
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking
                
                var img = GlobalVars.gallary.setImages(fileData.FilePath);
                imgMain.Source = ImageSource.FromFile(img.path);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception choosing file: " + ex.ToString());
                await DisplayAlert("Alert", "Exception choosing file: " + ex.ToString(), "OK");
            }
        }

        private void OnSwipe(object sender, SwipedEventArgs e)
        {
            btnChooseFile.Text = $"Swipe {e.Direction}";
            switch (e.Direction)
            {
                case SwipeDirection.Left:
                    // Handle the swipe
                    imgMain.Source = ImageSource.FromFile(GlobalVars.gallary.Next().path);
                    break;
                case SwipeDirection.Right:
                    imgMain.Source = ImageSource.FromFile(GlobalVars.gallary.Next(-1).path);
                    // Handle the swipe
                    break;
                case SwipeDirection.Up:
                    // Handle the swipe
                    imgMain.Source = ImageSource.FromFile(GlobalVars.gallary.Next(100).path);
                    break;
                case SwipeDirection.Down:
                    // Handle the swipe
                    imgMain.Source = ImageSource.FromFile(GlobalVars.gallary.SetDelete().path);  
                    break;
            }
        }

        private async void OnImageTapped(object sender, EventArgs e)
        {
            btnChooseFile.Text = " 1 Tapped";
            var answer = await DisplayAlert($"Delete", "Do you wan't to delete {} pictures ?", "Yes", "No");
            int deleted = GlobalVars.gallary.Delete(answer);
            if(deleted>0)
                await DisplayAlert("Deleted", $"Deleted {deleted} pictures ?", "OK");
        }

        private void OnImage2Tapped(object sender, EventArgs e)
        {
            btnChooseFile.Text = "2 Tapped";
        }

        private void OnImage3Tapped(object sender, EventArgs e)
        {
            btnChooseFile.Text = "3 Tapped";
        }

        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;
        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                Content.TranslationX = targetX.Clamp(-Content.Width * (currentScale - 1), 0);
                Content.TranslationY = targetY.Clamp(-Content.Height * (currentScale - 1), 0);

                // Apply scale factor.
                Content.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
            }
        }

    }
}