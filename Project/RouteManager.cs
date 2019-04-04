using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace EDNeutronRouterPlugin
{
    public class RouteManager
    {
        public List<EDSystem> SystemList = new List<EDSystem>();
        private int index = 0;
        private int totalJumps = 0;
        private bool isSet = false;
        private string routeUrl = "";

        public void openUrlonInternet()
        {
            
            System.Diagnostics.Process.Start(routeUrl);
        }

        //return the next SystemName.
        public string GetNextSystemName()
        {
            
            if (SystemList.Count > 0 && index < SystemList.Count -1 && index >= 0)
            {
                index = index + 1;
                totalJumps -= SystemList[index].GetJumpsCount();
                return SystemList[index].GetSystemName();
            }
            if (isSet)
                return SystemList[index].GetSystemName();
            return "";
        }

        public string GetRouteUrl()
        {
            return routeUrl;
        }
        //return the previous System name.
        public string GetPreviousSystemName()
        {
            if (SystemList.Count > 0 && index > 0 && index < SystemList.Count)
            {
                totalJumps += SystemList[index].GetJumpsCount();
                index = index - 1;
                return SystemList[index].GetSystemName();
            }
            if (isSet)
                return SystemList[index].GetSystemName();
            
            return "";
        }
        //return the number of jumps remaining.
        public int GetJumpsCount()
        {
            return (totalJumps - 1) < 0 ? totalJumps : totalJumps - 1;
        }
        public int GetRemainingWaypoints()
        {
            return (SystemList.Count - index - 1) < 0 ? SystemList.Count - index : totalJumps - index - 1;
        }


        //Return the Route.
        public void CalculateRoute(dynamic vaProxy,string currentSystem, string SystemTarget, decimal jumpDistance, int efficiency = 60)
        {
            if(vaProxy.GetBoolean("EDNR Debug") != null && vaProxy.GetBoolean("EDNR Debug")) //check Debug is on 
            {
                vaProxy.WriteToLog("Calculate Route Debug", "purple");
                vaProxy.WriteToLog("Current System : " + currentSystem, "yellow");
                vaProxy.WriteToLog("Target System : " + SystemTarget, "yellow");
                vaProxy.WriteToLog("Range : " + jumpDistance.ToString(), "yellow");
                vaProxy.WriteToLog("Efficency : " + efficiency.ToString(), "yellow");
            }

            //check if variables are correct.
            if (currentSystem == ""  || currentSystem == null)
            {
                vaProxy.WriteToLog("Error: Incorrect starting system value.", "red");

            }
            
            if (SystemTarget == null || SystemTarget == "")
            {
                vaProxy.WriteToLog("Error: Incorrect target system value.", "red");
                return;
            }
            if ( jumpDistance == 0 )
            {
                vaProxy.WriteToLog("Error: Incorrect jump range value.", "red");
                return;
            }

            JToken RouteData = NeutronRouterAPI.GetNewRoute(currentSystem, SystemTarget, jumpDistance, efficiency, vaProxy);
            if (RouteData == null)
                return;
            SystemList = NeutronRouterAPI.GetSystemList(RouteData);
            if (SystemList.Count == 0)
                return;
            SetJumpCount();
            if (vaProxy.GetBoolean("EDNR Debug") != null && vaProxy.GetBoolean("EDNR Debug")) //check Debug is on 
                vaProxy.WriteToLog(RouteData.ToString(),"purple");
            routeUrl = RouteData["job"].ToString();
            isSet = true;
            vaProxy.WriteToLog("route set", "green");
        }
 
        private void SetJumpCount()
        {
            totalJumps = 0;
            SystemList.ForEach(delegate (EDSystem system) {
                totalJumps += system.GetJumpsCount();
            });
            
        }
        private void SetRouteUrl(string url)
        {
            routeUrl = url;

        }
    }
}
