using SisOdonto.Infra.CrossCutting.SysMessage.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdonto.Infra.CrossCutting.SysMessage
{
    public class MessageRecord
    {
        public String Key { get { return Guid.NewGuid().ToString(); } }
        public string Description { get; set; }
        public MessageType Type { get; set; }
    }
}
