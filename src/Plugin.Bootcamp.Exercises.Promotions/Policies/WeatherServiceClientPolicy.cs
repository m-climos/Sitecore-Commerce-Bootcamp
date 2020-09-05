using Sitecore.Commerce.Core;


namespace Plugin.Bootcamp.Exercises.Promotions
{
    class WeatherServiceClientPolicy : Policy
    {
        /* Student: Create a property to store the API key.
         * Create a constructor to initialize it to an empty
         * string. */
         public WeatherServiceClientPolicy()
        {
            this.OpenWeatherAPIKey = string.Empty;
        }
        
        public string OpenWeatherAPIKey { get; set; }
    }
}
