using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDMX
{
    class KDMXUtilss
    {
        public static object InDirectSunlight(Vessel vessel)
        {
            bool DirectSunlight = false; // No panels at all? Always return false

            foreach (Part p in vessel.parts)
            {
                foreach (ModuleDeployableSolarPanel c in p.FindModulesImplementing<ModuleDeployableSolarPanel>())
                {
                    DirectSunlight = true;
                    foreach (var body in FlightGlobals.fetch.bodies)
                    {
                        if (c.status.ToString().ToUpper() == "BLOCKED BY " + body.name.ToUpper())
                        {
                            // if blocked celestial body return false.
                            return false;
                        }
                    }
                }
            }
            return DirectSunlight;
        }
    }
}
