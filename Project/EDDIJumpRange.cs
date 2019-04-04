using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EDNeutronRouterPlugin
{
    class EDDIJumpRange
    {
        public static decimal getJumpRange(dynamic vaProxy)
        {
            JObject shipMoitorData = getShipMonitorData();
            int buildId = (int)shipMoitorData["currentshipid"];
            JArray buildList = JArray.Parse(shipMoitorData["shipyard"].ToString());
            JToken currentBuild = GetCurrentBuildShip(buildId, buildList);
            decimal jumpRange = GetJumpRange(currentBuild);
            vaProxy.WriteToLog(jumpRange.ToString(), "purple");
            return jumpRange;
        }
        private static decimal GetJumpRange(JToken Build)
        {
            if (Build["maxjump"] == null)
                return 0.0m;
            return (decimal)Build["maxjump"];
        }

        private static JObject getShipMonitorData()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\EDDI\shipmonitor.json";
            string shipmonitor = System.IO.File.ReadAllText(path);

            return JObject.Parse(shipmonitor);
        }


        private static JToken GetCurrentBuildShip(int id, JArray BuildList)
        {
            for (int i = 0; i < BuildList.Count; i++)
            {
                if ((int)BuildList[i]["LocalId"] == id)
                    return BuildList[i];
            }
            return null;
        }
    }
}
