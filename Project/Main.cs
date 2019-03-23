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

        public static void VA_Init1(dynamic vaProxy){}

        public static void VA_Exit1(dynamic vaProxy){}

        public static void VA_Invoke1(dynamic vaProxy)
        {
            try
            {
                switch(vaProxy.Context)
                {
                    case "setRoute":
                        route = new RouteManager();

                        string SystemName = vaProxy.GetText("System name");
                        string TargetSystem = vaProxy.GetText("Next system name");
                        decimal jumpRange = 0.0m;
                        if(vaProxy.GetDecimal("Jump range") != null)
                            jumpRange = vaProxy.GetDecimal("Jump range");

                        route.CalculateRoute(vaProxy, SystemName, TargetSystem, jumpRange, 60);

                    
                        vaProxy.SetText("nextSystem", route.GetNextSystemName());
                        break;
                    case "nextSystem":

                            vaProxy.SetText("nextSystem", route.GetNextSystemName());
                        if (vaProxy.GetBoolean("EDNR Debug") != null && vaProxy.GetBoolean("EDNR Debug"))
                            vaProxy.WriteToLog(vaProxy.GetText("nextSystem"), "blue");

                        break;
                    case "previousSystem":
                        vaProxy.SetText("previousSystem", route.GetPreviousSystemName());
                        if (vaProxy.GetBoolean("EDNR Debug") != null && vaProxy.GetBoolean("EDNR Debug"))
                            vaProxy.WriteToLog(vaProxy.GetText("previousSystem"), "blue");

                        break;
                    case "clearRoute":
                        vaProxy.WriteToLog("route cleared", "blue");
                        route = new RouteManager();
                        break;
                    case "remainingJumps":
                        vaProxy.SetInt("Jumps Remaining", route.GetJumpsCount());
                        vaProxy.WriteToLog(route.GetJumpsCount().ToString(), "green");
                        break;
                    default:
                        vaProxy.WriteToLog("incorrect context", "red");
                        break;
                }
            }
            catch(Exception error)
            {
                vaProxy.WriteToLog("Error : " + error, "red");
            }
            
        }
    }
}

