using AdvTreeControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp
{
    public class NodeTreeModelController : ITreeModel
    {
		private string[] _watchProperties;
		private Node _root;
		public Node Root
		{
			get { return _root; }
		}

		public Collection<Node> Nodes
		{
			get { return _root.ChildNodes; }
		}

		public NodeTreeModelController(Node rootNode, params string[] watchProperties)
		{
			_root = rootNode;
			_watchProperties = watchProperties;
            _root.ChildNodes.CollectionChanged += ChildNodes_CollectionChanged;

			foreach (Node item in _root.ChildNodes)
			{
				item.ChildNodes.CollectionChanged += ChildNodes_CollectionChanged;
				item.PropertyChanged += Item_PropertyChanged;
			}
		}

        private void ChildNodes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
			if (e.NewItems != null)
            {
                foreach (Node item in e.NewItems)
                {
					item.ChildNodes.CollectionChanged += ChildNodes_CollectionChanged;
                    item.PropertyChanged += Item_PropertyChanged;
					OnNodeInserted(item.Parent, e.NewStartingIndex, item);
				}
            }

			if (e.OldItems != null)
            {
				foreach (Node item in e.OldItems)
				{
					item.ChildNodes.CollectionChanged -= ChildNodes_CollectionChanged;
					item.PropertyChanged -= Item_PropertyChanged;
					OnNodeRemoved(item.Parent, e.OldStartingIndex, item);
				}
			}
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
			if (sender is Node node && _watchProperties != null && _watchProperties.Any(x => x == e.PropertyName))
            {
				TreePath parentPath = GetPath(node.Parent);
				if (parentPath != null)
				{
					TreeModelEventArgs args = new TreeModelEventArgs(parentPath, new int[] { node.Source.IndexOf(node) }, new object[] { node });
					OnNodesChanged(args);
				}
			}
        }

        public TreePath GetPath(Node node)
		{
			if (node == _root)
				return TreePath.Empty;
			else
			{
				Stack<object> stack = new Stack<object>();
				while (node != _root)
				{
					stack.Push(node);
					node = node.Parent;
				}
				return new TreePath(stack.ToArray());
			}
		}

		public Node FindNode(TreePath path)
		{
			if (path.IsEmpty())
				return _root;
			else
				return FindNode(_root, path, 0);
		}

		private Node FindNode(Node root, TreePath path, int level)
		{
			foreach (Node node in root.ChildNodes)
				if (node == path.FullPath[level])
				{
					if (level == path.FullPath.Length - 1)
						return node;
					else
						return FindNode(node, path, level + 1);
				}
			return null;
		}

		#region ITreeModel Members

		public System.Collections.IEnumerable GetChildren(TreePath treePath)
		{
			Node node = FindNode(treePath);
			if (node != null)
				foreach (Node n in node.ChildNodes)
					yield return n;
			else
				yield break;
		}

		public bool IsLeaf(TreePath treePath)
		{
			Node node = FindNode(treePath);
			if (node != null)
            {
				return node.ChildNodes.Count == 0;
            }
			else
				throw new ArgumentException("treePath");
		}

		public event EventHandler<TreeModelEventArgs> NodesChanged;
		internal void OnNodesChanged(TreeModelEventArgs args)
		{
			if (NodesChanged != null)
				NodesChanged(this, args);
		}

		public event EventHandler<TreePathEventArgs> StructureChanged;
		public void OnStructureChanged(TreePathEventArgs args)
		{
			if (StructureChanged != null)
				StructureChanged(this, args);
		}

		public event EventHandler<TreeModelEventArgs> NodesInserted;
		internal void OnNodeInserted(Node parent, int index, Node node)
		{
			if (NodesInserted != null)
			{
				TreeModelEventArgs args = new TreeModelEventArgs(GetPath(parent), new int[] { index }, new object[] { node });
				NodesInserted(this, args);
			}

		}

		public event EventHandler<TreeModelEventArgs> NodesRemoved;
		internal void OnNodeRemoved(Node parent, int index, Node node)
		{
			if (NodesRemoved != null)
			{
				TreeModelEventArgs args = new TreeModelEventArgs(GetPath(parent), new int[] { index }, new object[] { node });
				NodesRemoved(this, args);
			}
		}

		#endregion
	}
}
