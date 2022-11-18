using System.ComponentModel;
using DataTables;

namespace Editor_NET_Framework_Demo.Models
{
    public class HouseHoldModel
    {
        public int AmountPeople { get; set; }

        public int StandMeter{ get; set; }

        public int LpgConsumption { get; set; }

        public int CityGasConsumption { get; set; }

        public string CreatedDate { get; set; }

        public int PeriodeId { get; set; }

        public int UserId { get; set; }

        public int HouseCalculation { get; set; }
    }
}