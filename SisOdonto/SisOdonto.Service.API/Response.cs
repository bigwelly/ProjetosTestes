using SisOdonto.Infra.CrossCutting.SysMessage.Enumerate;
using SisOdonto.Infra.CrossCutting.SysMessage;

namespace SisOdonto.Service.API
{
    public class Response<T>
    {
        public Response()
        {
            Data = new List<T>();
            MessageList = new MessageCollection();
            ResponseType = ResponseType.Success;
        }

        public long TotalOfRegisters => Data.Count;
        public MessageCollection MessageList { get; }
        public ResponseType ResponseType { get; set; }
        public string ResponseTypeString { get { return ResponseType.ToString(); } }

        public List<T> Data { get; set; }
    }
}

