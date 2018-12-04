using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Model.BusinessEntities
{
    public class AddressCSVModel: ICSVModel
    {
        public int STREET_NUMBER { get; set; }
        public string STREET_NAME { get; set; }
        public string STREET_SUFFIX { get; set; }
        public string ADDR_LINE_ONE { get; set; }
        public string ADDR_LINE_TWO { get; set; }
        public string ADDR_LINE_THREE { get; set; }
    }
}
