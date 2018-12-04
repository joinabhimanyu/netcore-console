using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Model.BusinessEntities
{
    public class NameCSVModel : ICSVModel
    {
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
    }
}
