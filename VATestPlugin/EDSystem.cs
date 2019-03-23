namespace EDNeutronRouterPlugin
{
    public class EDSystem
    {
        private double distance_left;
        private int jumps;
        private bool neutron_star;
        private string system;

        public EDSystem(double distanceLeft, int jumpsCount, bool neutronStar, string systemName)
        {
            distance_left = distanceLeft;
            jumps = jumpsCount;
            neutron_star = neutronStar;
            system = systemName;
        }


        public string GetSystemName()
        {
            return system;
        }
        public int GetJumpsCount()
        {
            return jumps;
        }
        public string DataToString()
        {
            return distance_left.ToString() + jumps.ToString() + neutron_star.ToString() + system;
        }
    }
}
