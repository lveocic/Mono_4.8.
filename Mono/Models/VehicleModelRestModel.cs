using System;

namespace Mono.MVC.Models
{
    public class VehicleModelRestModel
    {
        #region Properties

        public string Abrv { get; set; }
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public string Name { get; set; }
        public VehicleMakeRestModel VehicleMake { get; set; }

        #endregion Properties
    }
}