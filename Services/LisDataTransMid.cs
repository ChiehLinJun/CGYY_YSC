using CGYY_YSC.Entity.DB;
using System;
using System.Collections.Generic;

namespace CGYY_YSC.Services
{
    internal class LisDataTransMid
    {
        public void Trans()
        {
            try
            {
                /**RecodeODB recodeODB = new RecodeODB();
                //List<RecodeEntity> insertList = recodeODB.FetchViewToRecode();
//123
                if (insertList != null)
                {
                    if (insertList.Count > 0) recodeODB.multiInsert(insertList);
                }**/
            }
            catch (Exception ex)
            {
                Log.ErrorLog(ex.ToString());
            }
        }
    }
}