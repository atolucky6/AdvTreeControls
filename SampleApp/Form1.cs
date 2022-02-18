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
    public partial class Form1 : Form
    {
        NodeTreeModelController _nodeController;
        Node _rootNode;

        public Form1()
        {
            InitializeComponent();

            NodeTextBox nodeTextBox = new NodeTextBox()
            {
                Column = 0,
                EditEnabled = false,
                DataPropertyName = "Name",
                TextAlign = HorizontalAlignment.Left,
                
            };
            _treeNode.NodeControls.Add(nodeTextBox);

            _rootNode = new Node() { Name = "Root1" };
            for (int i = 0; i < 1000; i++)
            {
                var childNode = new Node() { Name = Guid.NewGuid().ToString() };
                _rootNode.ChildNodes.Add(childNode);
            }
            _nodeController = new NodeTreeModelController(_rootNode, "Name") ;
            _treeNode.RowHeight = 24;
            _treeNode.HideSelection = false;
            _treeNode.SelectionMode = AdvTreeControls.TreeSelectionMode.MultiSameParent;
            _treeNode.Model = _nodeController;
            _treeNode.NodeMouseDoubleClick += _treeNode_NodeMouseDoubleClick;
        }

        private void _treeNode_NodeMouseDoubleClick(object sender, AdvTreeControls.TreeNodeAdvMouseEventArgs e)
        {
            if (e.Node != null && e.Node.Tag is Node selectedNode)
            {
                selectedNode.Name = Guid.NewGuid().ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        int index = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            Node childNode = new Node()
            {
                Name = $"Node{index}"
            };
            index++;

            if (_treeNode.SelectedNode != null &&_treeNode.SelectedNode.Tag is Node node)
            {

            }
            else
            {
                _rootNode.ChildNodes.Add(childNode);
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_treeNode.SelectedNodes != null)
            {
                foreach (var item in _treeNode.SelectedNodes.ToArray())
                {
                    if (item.Tag is Node selectedNode)
                    {
                        selectedNode.Source.Remove(selectedNode);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_treeNode.SelectedNode != null)
            {
                if (_treeNode.SelectedNode.Tag is Node selectedNode)
                {
                    Node childNode = new Node()
                    {
                        Name = $"Node{index}"
                    };
                    index++;

                    selectedNode.ChildNodes.Add(childNode);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_treeNode.SelectedNode != null)
            {
                if (_treeNode.SelectedNode.Tag is Node selectedNode)
                {
                    Node childNode = new Node()
                    {
                        Name = $"Node{index}"
                    };
                    index++;

                    // selectedNode.Source.Insert(selectedNode.Source.IndexOf(selectedNode), childNode); // Insert above selected item

                    selectedNode.Source.Insert(selectedNode.Source.IndexOf(selectedNode) + 1, childNode); // Insert below selected item
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (_treeNode.SelectedNodes != null)
            {

                foreach (var item in _treeNode.SelectedNodes)
                {
                    if (item.Tag is Node selectedNode)
                    {
                        selectedNode.Name = Guid.NewGuid().ToString();
                    }
                }
            }
            timer1.Start();

        }
    }
}
