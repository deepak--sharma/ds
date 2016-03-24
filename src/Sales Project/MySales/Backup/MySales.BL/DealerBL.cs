using System;
using MySales.BO;
using MySales.DL;
using System.Collections.Generic;

namespace MySales.BL
{
    public class DealerBL
    {
        public string CreateDealer(Dealer theDealer)
        {
            string status = "START";
            try
            {
                int code = (new DealerDL()).InsertDealer(theDealer);
                if (code < 1)
                {
                    status = "ERROR";
                }
                else
                {
                    status = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }

        public string GetDealerNames(ref List<Dealer> lstDealer)
        {
            string status = "START";
            try
            {
                lstDealer = new DealerDL().GetDealerNames();
                if (lstDealer != null && lstDealer.Count > 0)
                {
                    status = "SUCCESS";
                }
                else
                {
                    status = "VOID";
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }

            return status;

        }

        public long GetNextDealerID()
        {
            long _id = 0;
            try
            {
                _id = new DealerDL().GetNextDealerID();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _id;
        }
    }
}
