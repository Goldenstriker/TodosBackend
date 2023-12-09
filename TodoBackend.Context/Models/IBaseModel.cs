using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoBackend.Context.Models
{
    internal interface IBaseModel
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
