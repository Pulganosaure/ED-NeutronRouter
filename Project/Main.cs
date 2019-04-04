using System;




namespace EDNeutronRouterPlugin
{
    public class EDNeutronRouterPlugin
    {
        private static RouteManager route = new RouteManager();

        public static string VA_DisplayName()
        {
            return "Neutron Router - v0.1.0+";
        }

        public static string VA_DisplayInfo()
        {
            return "Voice Attack Plugin to use ed neutron router easily with a VR headset";
        }
        public static void VA_StopCommand()
        {

        }

        public static Guid VA_Id()
        {
            return new Guid("{C16BEBA9-2017-4b7f-BFA2-55B7C667125B}");
        }

        public static void VA_Init1(dynamic vaProxy) { }

        public static void VA_Exit1(dynamic vaProxy) { }

        public static void VA_Invoke1(dynamic vaProxy)
        {
            try
            {
                switch (vaProxy.Context)
                {
                    case "setRoute":
                        route = new RouteManager();

                        string SystemName = vaProxy.GetText("System name");
                        string TargetSystem = vaProxy.GetText("Next system name");
                        decimal jumpRange = 0.0m;
                        if (vaProxy.GetDecimal("Jump range") != null)
                            jumpRange = vaProxy.GetDecimal("Jump range");
                        route.CalculateRoute(vaProxy, SystemName, TargetSystem, jumpRange, 60);
                        break;

                    case "nextSystem":
                        vaProxy.SetText("Next system", route.GetNextSystemName());
                        break;

                    case "previousSystem":
                        vaProxy.SetText("Previous system", route.GetPreviousSystemName());
                        break;

                    case "remainingWaypoints":
                        vaProxy.SetInt("Remaining waypoints", route.GetRemainingWaypoints());
                        break;
                    case "clearRoute":
                        vaProxy.WriteToLog("route cleared", "blue");
                        route = new RouteManager();
                        break;
                    case "remainingJumps":
                        vaProxy.SetInt("Remaining jumps", route.GetJumpsCount());
                        break;
                    case "website":
                        if (!String.IsNullOrEmpty(route.GetRouteUrl()))
                            System.Diagnostics.Process.Start("https://spansh.co.uk/plotter/results/" + route.GetRouteUrl());
                        else
                            vaProxy.WriteToLog("Route not set.", "red");
                        break;
                    case "getJumpRange":
                        vaProxy.SetDecimal("Jump range", EDDIJumpRange.getJumpRange(vaProxy));
                        break;
                    default:
                        vaProxy.WriteToLog("incorrect context", "red");
                        break;
                }
            }
            catch (Exception error)
            {
                vaProxy.WriteToLog("Error : " + error, "red");
            }

        }
    }
}