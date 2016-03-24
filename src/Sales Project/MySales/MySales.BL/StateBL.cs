using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class StateBl
    {
        public List<State> GetStateList()
        {
            var states = new StateDl().GetStateList();
            return states;
        }
    }
}
