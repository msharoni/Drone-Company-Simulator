Locationusing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    partial class BL : IBL.IBL
    {
        List<DroneForList> Drones;
        DalObject.DalObject dalObject;
        Random r = new Random();
        public void AddDrone(int _Id, string _Model, int _MaxWeight, int _StationId)
        {
            dalObject.AddDrone(_Id, _Model, (IDAL.DO.WeightCategories)_MaxWeight);
            Drones.Add(new DroneForList { Id = _Id, Model, MaxWeight, r.Next(20, 41), DroneStatuses.maintenance, Station.Place, -1})
        }
    }
}
