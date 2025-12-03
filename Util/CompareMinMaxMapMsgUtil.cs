using CGYY_YSC.Entity.MsgMapping;
using System.Collections.Generic;

namespace CGYY_YSC.Util
{
    public class CompareMinMaxMapMsgUtil
    {
        private List<LimitVMsgCompareEntity> _responseList { set; get; }

        public List<LimitVMsgCompareEntity> responseList
        {
            set => _responseList = value;
        }

        public string CompareAndReply(string mapTopic, double mapValue)
        {
            string rtResponse = "";
            foreach (var tuple in this._responseList)
            {
                if (mapTopic != tuple.Topic) continue;

                rtResponse = (mapValue > tuple.Min && mapValue <= tuple.Max) ? tuple.Response : tuple.DeResponse;

                if (rtResponse != "") break;
            }

            return rtResponse;
        }
    }
}
