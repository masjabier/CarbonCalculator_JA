using System.ComponentModel;
using DataTables;

namespace Editor_NET_Framework_Demo.Models
{
    public class VehicleModel
    {
        //public int Id { get; set; }

        public int VehicleId{ get; set; }

        public int FuelId { get; set; }

        public int TransportationId { get; set; }

        public string CapacityId { get; set; }

        public int PeriodeId { get; set; }

        public int UserId { get; set; }
        public int TravelFrequency { get; set; }
        public string Per { get; set; }
        public int AmountPeople { get; set; }
        public int Mileage { get; set; }
        public string CreatedDate { get; set; }


    }
}