namespace Web.NavigationRoutes
{
    using System.Web.Routing;

    public interface INavigationRouteFilter
    {
        bool  ShouldRemove(Route navigationRoutes);
    }
}
