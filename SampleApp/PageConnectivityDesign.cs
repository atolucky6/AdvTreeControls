using AdvTreeControls.NodeControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleApp
{
    public partial class PageConnectivityDesign : UserControl
    {
        public PageConnectivityDesign()
        {
            InitializeComponent();

            NodeTreeView _nodeTreeView = new NodeTreeView();
            _nodeTreeView.Dock = DockStyle.Fill;

            _nodeTreeView.RootNode = new Node();
            _nodeTreeView.TreeView.NodeControls.Add(new NodeTextBox() { DataPropertyName = "Name" });
            _nodeTreeView.RootNode.ChildNodes.Add(new Node() { Name = "Root1" });
            _nodeTreeView.ContextMenuStrip = contextMenuStrip1;

            kryptonHeaderGroup1.Panel.Controls.Add(_nodeTreeView);
        }
    }
}
