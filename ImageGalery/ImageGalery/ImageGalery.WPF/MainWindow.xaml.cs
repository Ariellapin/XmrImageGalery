

using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace ImageGalery.WPF
{
    public partial class MainWindow : FormsApplicationPage
    {
        public MainWindow()
        {
            InitializeComponent();

            Forms.Init();
            LoadApplication(new ImageGalery.App());
        }
    }
}
