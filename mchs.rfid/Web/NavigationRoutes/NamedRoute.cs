namespace Web.NavigationRoutes
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class NamedRoute : Route
    {
         string _name;
         string _displayName;
         List<NamedRoute> _childRoutes = new List<NamedRoute>();

        public NamedRoute(string name, string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            this._name = name;
        }

        public NamedRoute(string name, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
                          IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
            this._name = name;
        }

        public NamedRoute(string name, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
                          RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            this._name = name;
        }

        public NamedRoute(string name, string displayName, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
                          RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            this._name = name;
            this._displayName = displayName;
        }

        public NamedRoute(string name, string displayName, string url, MvcRouteHandler routeHandler) : base(url, routeHandler)
        {
            this._name = name;
            this._displayName = displayName;
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public string DisplayName
        {
            get { return this._displayName ?? this._name; }
            set { this._displayName = value; }
        }
        public List<NamedRoute> Children { get { return this._childRoutes; } }
        public bool IsChild { get; set; }
    }
}