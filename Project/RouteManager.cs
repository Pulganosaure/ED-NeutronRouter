using System.Collections.Generic;

namespace EDNeutronRouterPlugin
{
    public class RouteManager
    {
        public List<EDSystem> SystemList = new List<EDSystem>();
        private int index = 0;
        private int totalJumps = 0;
        private bool isSet = false;


        //return the next SystemName.
        public string GetNextSystemName()
        {
            
            if (SystemList.Count > 0 && index < SystemList.Count -1 && index >= 0)
            {
                totalJumps -= SystemList[index].GetJumpsCount();
                index = index + 1;
                return SystemList[index].GetSystemName();
            }
            if (isSet)
                return SystemList[index].GetSystemName();
            return "";
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
            return totalJumps;
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
            if (currentSystem == ""  || currentSystem == null || SystemTarget == null || SystemTarget == "" || jumpDistance == 0 )
            {
                vaProxy.WriteToLog("incorrect route informations", "red");
                return;

            }
            SystemList = NeutronRouterAPI.GetNewRoute(currentSystem, SystemTarget, jumpDistance, efficiency, vaProxy);
            if (SystemList.Count == 0)
                return;
            CalculateNumberOfJumps();
            isSet = true;
            vaProxy.WriteToLog("route set", "green");
        }
        public void CalculateNumberOfJumps() //get the number of jumps for the route.
        {
            SystemList.ForEach(delegate (EDSystem system) {
                totalJumps += system.GetJumpsCount();
            });
        }

    }
}
