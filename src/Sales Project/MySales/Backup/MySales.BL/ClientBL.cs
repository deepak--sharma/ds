using MySales.BO;
using MySales.DL;
using System;
using System.Collections.Generic;
namespace MySales.BL
{
    public class ClientBL
    {

        public string CreateClient(Client theClient)
        {
            string status = "START";
            try
            {
                int code = (new ClientDL()).InsertClient(theClient);
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

        public string GetClientNames(ref List<Client> lstClient)
        {
            string status = "START";
            try
            {
                lstClient = new ClientDL().GetClientNames();
                if (lstClient != null && lstClient.Count > 0)
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

        public long GetNextClientID()
        {
            long _id = 0;
            try
            {
                _id = new ClientDL().GetNextClientID();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _id;

        }
    }
}
