using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class StateBL
    {
        public List<State> GetStateList()
        {
            List<State> states = new List<State>();

            states = new StateDL().GetStateList();
            return states;
        }
    }
}
