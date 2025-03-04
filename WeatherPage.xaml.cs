namespace weather__app;

using weather__app.models;
using weather__app.Services;

public partial class WeatherPage : ContentPage
{
    private bool isAnimating = true; // Flag to control animation loop
    public List<models.List> WeatherList;
    private double lat;
    private double lon;

    public WeatherPage()
    {
        InitializeComponent();
        WeatherList = new List<models.List>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        isAnimating = false; // Ensure animation starts when the page appears
        await StartImageAnimation();

        await GetLocation();
        await GetWeatherDataByLocation(lat, lon);


    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        isAnimating = false; // Stop animation when the page is closed
    }

    private async Task StartImageAnimation()
    {
        while (isAnimating) // Run only if the page is active
        {
            await ImgWeatherIcon.TranslateTo(0, -10, 1000, Easing.Linear); // Move up
            await ImgWeatherIcon.TranslateTo(0, 10, 1000, Easing.Linear);  // Move down
        }
    }

    

    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        lat = location.Latitude;
        lon = location.Longitude;

    }


    private async void OnLocationTapped(object sender, EventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(lat, lon);
    }

    public async Task GetWeatherDataByLocation(double lat, double lon)
    {
        var results = await ApiService.GetWeather(lat, lon);
        foreach (var item in results.list)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;

        LblCity.Text = results.city.name;
        LblDescription.Text = results.list[0].weather[0].description;
        LblTemperature.Text = results.list[0].main.temperature + "°C";
        LblHumadity.Text = results.list[0].main.humidity + "%";
        LblWind.Text = results.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source = results.list[0].weather[0].fullIconUrl;
    }

}