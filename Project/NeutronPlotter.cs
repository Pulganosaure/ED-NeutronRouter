using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Threading;

namespace EDNeutronRouterPlugin
{
    public class NeutronRouterAPI
    {



        public static JToken GetNewRoute(string currentSystem, string SystemTarget, decimal jumpDistance, int efficiency, dynamic vaProxy)
        {
            JObject Routeresponse;
            var Route = PlotRoute(currentSystem, SystemTarget, jumpDistance, efficiency, vaProxy).Content;
            if (Route == null || Route == "")
            {
                vaProxy.WriteToLog("Error: cannot call spansh API.", "red");
                return null;
            }

            Routeresponse = JObject.Parse(Route);

            if (Routeresponse["error"] != null || Routeresponse == null)
            {

                vaProxy.WriteToLog("error: " + Routeresponse["error"].ToString(), "red");
                return null;
            }
            else
            {
                var job = Routeresponse["job"].ToString();
                JObject routeResult = GetRouteResults(job);

                while (routeResult["status"].ToString() == "queued")
                {
                    Thread.Sleep(1000);
                    routeResult = GetRouteResults(job);
                }
                return routeResult["result"];
             }
        }

        public static List<EDSystem> GetSystemList(JToken routeResult)
        {
            JArray Systems = routeResult["system_jumps"].ToObject<JArray>();
            List<EDSystem> SystemList = new List<EDSystem>();
            for (int i = 0; i < Systems.Count; i++)
            {
                var System = Systems[i];
                EDSystem test = new EDSystem(System["distance_left"].ToObject<double>(), Systems[i]["jumps"].ToObject<int>(), Systems[i]["neutron_star"].ToObject<bool>(), Systems[i]["system"].ToObject<string>());
                SystemList.Add(test);
            }

            return SystemList;
        }

        //get the job id from the spansh API.
        public static IRestResponse PlotRoute(string Position, string Destination, decimal range, int Efficiency, dynamic vaProxy)
        {
            var client = new RestClient("https://spansh.co.uk/api/");
            var request = new RestRequest("route");
            request.AddParameter("efficiency", 60)
                .AddParameter("range", 50)
                .AddParameter("from", Position)
                .AddParameter("to", Destination);
            var response = client.Get(request);
            return response;


        }
        //get the system List from the the spansh API
        public static JObject GetRouteResults(string job)
        {
            var client = new RestClient("https://spansh.co.uk/api/");
            var Jobrequest = new RestRequest("results/" + job);

            var response = client.Get(Jobrequest);
            return JObject.Parse(response.Content);
        }
    }
}
