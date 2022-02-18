using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace AdvTreeControls
{
	public class TreeModel : ITreeModel
	{
		private TreeNode _root;
		public TreeNode Root
		{
			get { return _root; }
		}

		public Collection<TreeNode> Nodes
		{
			get { return _root.Nodes; }
		}

		public TreeModel()
		{
			_root = new TreeNode();
			_root.Model = this;
		}

		public TreePath GetPath(TreeNode node)
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

		public TreeNode FindNode(TreePath path)
		{
			if (path.IsEmpty())
				return _root;
			else
				return FindNode(_root, path, 0);
		}

		private TreeNode FindNode(TreeNode root, TreePath path, int level)
		{
			foreach (TreeNode node in root.Nodes)
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
			TreeNode node = FindNode(treePath);
			if (node != null)
				foreach (TreeNode n in node.Nodes)
					yield return n;
			else
				yield break;
		}

		public bool IsLeaf(TreePath treePath)
		{
			TreeNode node = FindNode(treePath);
			if (node != null)
				return node.IsLeaf;
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
		internal void OnNodeInserted(TreeNode parent, int index, TreeNode node)
		{
			if (NodesInserted != null)
			{
				TreeModelEventArgs args = new TreeModelEventArgs(GetPath(parent), new int[] { index }, new object[] { node });
				NodesInserted(this, args);
			}

		}

		public event EventHandler<TreeModelEventArgs> NodesRemoved;
		internal void OnNodeRemoved(TreeNode parent, int index, TreeNode node)
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
