using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Math;
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
                            bl.UnChargeDrone(DroneId, 50);
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
                    case BO.DroneStatuses.Delivery:
                        BO.Parcel parcel = bl.DisplayParcel(drone.Parcel.Id);
                        if (parcel.PickedUp == null)
                        {
                            if (bl.Distance(drone.CurrentLocation, bl.DisplayCustomer(parcel.Sender.Id).Location) < DroneSpeed)
                                bl.PickUpParcel(DroneId);
                            else
                            {
                                BO.Location location = LocationCalculater(drone.CurrentLocation, bl.DisplayCustomer(parcel.Sender.Id).Location, DroneSpeed);
                                bl.UpdateDroneLocation(DroneId, drone.Battery - (BatteryUsage[1] * DroneSpeed), location);
                            }
                        }
                        else if (parcel.Delivered == null)
                        {
                            if (bl.Distance(drone.CurrentLocation, bl.DisplayCustomer(parcel.Reciver.Id).Location) < DroneSpeed)
                                bl.DeliverParcel(DroneId);
                            else
                            {
                                BO.Location location = LocationCalculater(drone.CurrentLocation, bl.DisplayCustomer(parcel.Reciver.Id).Location, DroneSpeed);
                                int Weight = (int)drone.Parcel.Weight;
                                bl.UpdateDroneLocation(DroneId, drone.Battery - (BatteryUsage[Weight+1] * DroneSpeed), location);
                            }
                        }
                        break;
                    
                }
                Thread.Sleep(Delay);
                Update();
            }
        }
        private BO.Location LocationCalculater(BO.Location from, BO.Location to, double Speed)
        {
            double radian = PI / 180;
            double degree = 180 / PI;
            double la1 = from.Lattitude * radian;
            double lo1 = from.Longitude * radian;
            double Ad = Speed / 6376.5;
            BO.Location l = new BO.Location();
            var teth = Bearing(from, to);
            l.Lattitude = Asin(Sin(la1)* Cos(Ad) + Cos(la1)* Sin(Ad)* Cos(teth));
            l.Longitude = lo1 + Atan2(Sin(teth)* Sin(Ad)* Cos(la1), Cos(Ad) - Sin(la1)* Sin(l.Lattitude));
            l.Lattitude *= degree;
            l.Longitude *= degree;
            l.Longitude = (l.Longitude + 540) % 360 - 180;
            return l;
        }

        private double Bearing(BO.Location from, BO.Location to)
        {
            double radian = PI / 180;
            double la1 = from.Lattitude * radian;
            double lo1 = from.Longitude * radian;
            double la2 = to.Lattitude * radian;
            double lo2 = to.Longitude * radian;
            double deltaLo = lo2 - lo1;
            double X = Cos(la2) * Sin(deltaLo);
            double Y = Cos(la1) * Sin(la2) - Sin(la1) * Cos(la2) * Cos(deltaLo);
            return Atan2(X, Y);
        }

    }
}
