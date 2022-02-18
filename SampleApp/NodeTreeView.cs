using AdvTreeControls;
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
    public partial class NodeTreeView : UserControl
    {
        Node _rootNode;
        NodeTreeModelController _nodeController;

        public TreeViewAdv TreeView { get => _treeView; }

        public Node RootNode
        {
            get => _rootNode;
            set
            {
                if (_rootNode != value)
                {
                    _nodeController = null;
                    _treeView.Model = null;

                    _rootNode = value;
                    OnRootNodeChanged();

                    if (_rootNode != null)
                    {
                        _nodeController = new NodeTreeModelController(_rootNode, "Name");
                        _treeView.Model = _nodeController;
                    }
                }
            }
        }

        public NodeTreeModelController Controller
        {
            get => _nodeController;
        }

        public Node SelectedValue
        {
            get => _treeView.SelectedNode == null ? null : _treeView.SelectedNode.Tag as Node;
        }

        public IEnumerable<Node> SelectedValues
        {
            get
            {
                return _treeView.SelectedNodes == null ? Enumerable.Empty<Node>() : _treeView.SelectedNodes.Select(x => x.Tag as Node);
            }
        }

        public NodeTreeView()
        {
            InitializeComponent();
            _treeView.SelectionChanged += OnTreeViewSelectionChanged;
        }

        private void OnTreeViewSelectionChanged(object sender, EventArgs e)
        {

        }

        protected virtual void OnRootNodeChanged()
        {

        }
    }
}
