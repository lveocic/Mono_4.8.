using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono.Service.Models.Common
{
    public interface IVehicleMake
    {
        #region Properties

        string Abrv { get; set; }
        Guid Id { get; set; }
        string Name { get; set; }

        #endregion Properties
    }
}