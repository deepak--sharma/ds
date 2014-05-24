using System;
using MySales.BO;
using MySales.DL;
using System.Collections.Generic;

namespace MySales.BL
{
    public class DealerBl
    {
        public string CreateDealer(Dealer theDealer)
        {
            var status = "START";
            try
            {
                var code = (new DealerDl()).InsertDealer(theDealer);
                status = code < 1 ? "ERROR" : "SUCCESS";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }

        public string GetDealerNames(ref List<Dealer> lstDealer)
        {
            var status = "START";
            try
            {
                lstDealer = new DealerDl().GetDealerNames();
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

        public long GetNextDealerId()
        {
            var id = 0;
            try
            {
                id = (int) new DealerDl().GetNextDealerId();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }
    }
}
