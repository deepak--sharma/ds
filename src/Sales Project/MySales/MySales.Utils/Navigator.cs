using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MySales.Utils
{
    public class Navigator
    {
        private string _steps;
        public Navigator(string steps)
        {
            _steps = steps;
        }
        public void Launch()
        {
            var frm = new Form
                          {
                              Name = "frmNavigator",
                              Text = "Navigator",
                              Height = 500,
                              Width = 900,
                              StartPosition = FormStartPosition.CenterParent
                          };
            var tree = new TreeView
                           {
                               ShowPlusMinus = true,
                               ShowRootLines = true,
                               Height = 300,
                               Width = 200
                           };
            
            var parentNode = tree.Nodes.Add("root", "root");
            //tree.Nodes.Add(parentNode)
            parentNode.ExpandAll();
            parentNode.Nodes.Add("child1", "child1");
            //tree.Nodes.Add();
            frm.Controls.Add(tree);
            frm.Show();
        }
    }
}
