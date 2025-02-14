namespace PlacasAPI.DataProviders
{
    public class ConsultaPlacaConfiguration
    {
        public ConsultaPlacaConfiguration(string token)
        {
            Token = token;
        }

        public string Token { get; private set; }
    }
}
