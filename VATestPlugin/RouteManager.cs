using System.Collections.Generic;

namespace EDNeutronRouterPlugin
{
    public class RouteManager
    {
        public List<EDSystem> SystemList = new List<EDSystem>();
        private int index = 0;
        private int totalJumps = 0;

        public string GetNextSystemName()
        {
            if (SystemList.Count > 0 && index < SystemList.Count && index >= 0)
            {
                totalJumps -= SystemList[index].GetJumpsCount();
                index = index + 1;
                return SystemList[index].GetSystemName();
            }
            return "";
        }
        public string GetPreviousSystemName()
        {
            if (SystemList.Count > 0 && index > 0 && index < SystemList.Count)
            {
                totalJumps += SystemList[index].GetJumpsCount();
                index = index - 1;
                return SystemList[index].GetSystemName();
            }
            return "";
        }
        public void CalculateRoute(dynamic vaProxy,string currentSystem, string SystemTarget, decimal jumpDistance, int efficiency = 60)
        {
            if(vaProxy.GetBoolean("EDNR Debug") != null && vaProxy.GetBoolean("EDNR Debug"))
            {
                vaProxy.WriteToLog("Calculate Route Debug", "purple");
                vaProxy.WriteToLog("Current System : " + currentSystem, "yellow");
                vaProxy.WriteToLog("Target System : " + SystemTarget, "yellow");
                vaProxy.WriteToLog("Range : " + jumpDistance.ToString(), "yellow");
                vaProxy.WriteToLog("Efficency : " + efficiency.ToString(), "yellow");
            }

            if (currentSystem == ""  || currentSystem == null || SystemTarget == null || SystemTarget == "" || jumpDistance == 0 )
            {
                vaProxy.WriteToLog("incorrect route informations", "red");
                return;

            }
            SystemList = NeutronRouterAPI.GetNewRoute(currentSystem, SystemTarget, jumpDistance, efficiency, vaProxy);
            CalculateNumberOfJumps();
            vaProxy.WriteToLog("route set  ::" + totalJumps.ToString(), "green");
        }
        public void CalculateNumberOfJumps()
        {
            SystemList.ForEach(delegate (EDSystem system) {
                totalJumps += system.GetJumpsCount();
            });
        }
    }
}
