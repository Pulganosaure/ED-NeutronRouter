namespace EDNeutronRouterPlugin
{
    public class EDSystem
    {
        private double distance_left;
        private int jumps;
        private bool neutron_star;
        private string system;

        //init the System with his informations 
        public EDSystem(double distanceLeft, int jumpsCount, bool neutronStar, string systemName)
        {
            distance_left = distanceLeft;
            jumps = jumpsCount;
            neutron_star = neutronStar;
            system = systemName;
        }

        //return the System Name.
        public string GetSystemName()
        {
            return system;
        }

        //return the Number of jumps for this step.
        public int GetJumpsCount()
        {
            return jumps;
        }
        //Debug function;
        public string DataToString()
        {
            return distance_left.ToString() + jumps.ToString() + neutron_star.ToString() + system;
        }
    }
}
