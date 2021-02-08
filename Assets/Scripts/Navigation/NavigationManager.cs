using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class NavigationManager
{
    public struct Route
    {
        public string RouteDescription;
        public bool CanTravel;
    }

    public static Dictionary<string, Route> RouteInformation = new Dictionary<string, Route>()
    {
        { "Overworld", new Route {RouteDescription = "The big bad world", CanTravel = true}},
        
        { "Village", new Route {RouteDescription = "A bustling Village", CanTravel = true}},
        
        { "Town", new Route {RouteDescription = "A town surrounded by walls", CanTravel = true}},
        
        { "Cave", new Route {RouteDescription = "A cave in the woods", CanTravel = false}},
        
        { "Shop", new Route{RouteDescription="The village shop", CanTravel=true}},

    };


    public static string GetRouteInfo(string destination) //Look at the dictionary above and look through it for the destination provided
    {
        return RouteInformation.ContainsKey(destination) ? RouteInformation[destination].RouteDescription : null;
    }

    public static bool CanNavigate(string destination) //Look at the destination and find if the player can travel there, which is default false
    {
        /*
        if (RouteInformation.ContainsKey(destination) && RouteInformation[destination].CanTravel != false)
            return true;
        else
            Debug.Log("You cannot travel to " + destination);
        */
        return RouteInformation.ContainsKey(destination) ? RouteInformation[destination].CanTravel : false; //this is a fancy version of above
    }


    public static void NavigateTo(string destination) //Actually move to the destination, add effects here
    {
        SceneManager.LoadScene(destination);
    }
}
