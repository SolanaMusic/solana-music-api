namespace SolanaMusicApi.Application.Factories.RedirectUrlFactory;

public interface IRedirectUrlFactory
{
    string Create(string provider);
}
