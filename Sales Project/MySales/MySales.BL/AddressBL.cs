using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.DL;
using MySales.BO;
using MySales.Utils;

namespace MySales.BL
{
    public class AddressBl
    {
        public Address GetAddress(Int64 id)
        {
            return new AddressDl().GetAddress(id);
        }

        public Utility.ActionStatus AddUpdateAddress(Address address)
        {
            return new AddressDl().AddUpdateAddress(address);
        }

        public void DeleteAddress(Int64 id)
        {
            new AddressDl().DeleteAddress(id);
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
