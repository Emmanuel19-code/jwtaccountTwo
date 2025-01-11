namespace jwtaccount_two.Domain.Contracts
{
    public class WeatherResponse
    {
        public required string Date {get;set;}
        public required string  Type {get;set;}
        public string Info {get;set;}
    }

    public class RequestWeather
    {
        public required string Date {get;set;}
    }

    public class WeatherInfo
    {
        public string Type { get; set; }
        public string Info { get; set; }
        public string Description {get;set;}
    }

}