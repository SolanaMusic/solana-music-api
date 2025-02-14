namespace SolanaMusicApi.Application.Factories.RedirectUrlFactory;

public class RedirectUrlFactory : IRedirectUrlFactory
{
    public string Create(string provider)
    {
        return provider.ToLower() switch
        {
            "google" => "GoogleResponse",
            _ => "ExternalResponse"
        };
    }
}
