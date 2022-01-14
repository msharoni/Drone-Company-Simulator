using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace BL
{
    
    internal class DroneSim
    {
        private int Delay = 500;
        private double DroneSpeed = 500;
        private double[] BatteryUsage;
        public DroneSim(BlApi.IBL bl,int DroneId,Action Update,Func<bool> stop)
        {
            BatteryUsage = bl.GetBatteryUsages();
            while(!stop())
            {
                BO.Drone drone = bl.DisplayDrone(DroneId);
                switch(drone.Status)
                {
                    case BO.DroneStatuses.maintenance:
                        if (drone.Battery >= 100)
                            bl.UnChargeDrone(DroneId, 0);
                        else
                        {
                            bl.UnChargeDrone(DroneId, 1);
                            bl.ChargeDrone(DroneId);
                        }
                        break;
                    case BO.DroneStatuses.Available:
                        try
                        {
                            bl.LinkDroneToParcel(DroneId);
                        }
                        catch
                        {
                            try
                            {
                                bl.ChargeDrone(DroneId);
                            }
                            catch
                            {
                                Thread.Sleep(Delay);
                            }
                        }
                        break;
                }
            }
        }
    }
}
