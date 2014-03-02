using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.DL;
using MySales.BO;
using MySales.Utils;

namespace MySales.BL
{
    public class AddressBL
    {
        public Address GetAddress(Int64 id)
        {
            return new AddressDL().GetAddress(id);
        }

        public Utility.ActionStatus AddUpdateAddress(Address address)
        {
            return new AddressDL().AddUpdateAddress(address);
        }

        public void DeleteAddress(Int64 id)
        {
            new AddressDL().DeleteAddress(id);
        }

        //public Utility.ActionStatus UpdateAdvanceDetails(Employee emp)
        //{
        //    Utility.ActionStatus code = Utility.ActionStatus.SUCCESS;
        //    try
        //    {
        //        code = new AdvanceDetailDL().UpdateAdvanceDetails(emp);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return code;
        //}

    }
}
