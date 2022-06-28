using System;
using System.ComponentModel.DataAnnotations;

namespace Mono.Models
{
    public class VehicleMakeRestModel
    {
        #region Properties

        public string Abrv { get; set; }
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        #endregion Properties
    }
}