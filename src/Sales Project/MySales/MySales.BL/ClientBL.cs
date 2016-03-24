using MySales.BO;
using MySales.DL;
using System;
using System.Collections.Generic;
namespace MySales.BL
{
    public class ClientBl
    {

        public string CreateClient(Client theClient)
        {
            var status = "START";
            try
            {
                var code = (new ClientDl()).InsertClient(theClient);
                status = code < 1 ? "ERROR" : "SUCCESS";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }

        public string GetClientNames(ref List<Client> lstClient)
        {
            var status = "START";
            try
            {
                lstClient = new ClientDl().GetClientNames();
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

        public long GetNextClientId()
        {
            long id = 0;
            try
            {
                id = new ClientDl().GetNextClientId();
            }
            catch (Exception ex)
            {
                throw;
            }
            return id;

        }
    }
}
