namespace WeatherApp.Views
{
    using Xamarin.Forms;

    public partial class CityListPage : ContentPage
    {
        private double _width = 0;
        private double _height = 0;

        public CityListPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (_width != width || _height != height)
            {
                _width = width;
                _height = height;
                //TODO reconfigure layout
            }
        }
    }
}
