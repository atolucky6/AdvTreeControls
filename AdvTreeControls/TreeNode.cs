using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace AdvTreeControls
{
	public class TreeNode
	{
		#region NodeCollection
		private class TreeNodeCollection : Collection<TreeNode>
		{
			private TreeNode _owner;

			public TreeNodeCollection(TreeNode owner)
			{
				_owner = owner;
			}

			protected override void ClearItems()
			{
				while (this.Count != 0)
					this.RemoveAt(this.Count - 1);
			}

			protected override void InsertItem(int index, TreeNode item)
			{
				if (item == null)
					throw new ArgumentNullException("item");

				if (item.Parent != _owner)
				{
					if (item.Parent != null)
						item.Parent.Nodes.Remove(item);
					item._parent = _owner;
					base.InsertItem(index, item);

					TreeModel model = _owner.FindModel();
					if (model != null)
						model.OnNodeInserted(_owner, index, item);
				}
			}

			protected override void RemoveItem(int index)
			{
				TreeNode item = this[index];
				item._parent = null;
				base.RemoveItem(index);

				TreeModel model = _owner.FindModel();
				if (model != null)
					model.OnNodeRemoved(_owner, index, item);
			}

			protected override void SetItem(int index, TreeNode item)
			{
				if (item == null)
					throw new ArgumentNullException("item");

				RemoveAt(index);
				InsertItem(index, item);
			}
		}

		#endregion

		#region Properties

		private TreeModel _model;
		internal TreeModel Model
		{
			get { return _model; }
			set { _model = value; }
		}

		private TreeNodeCollection _nodes;
		public Collection<TreeNode> Nodes
		{
			get { return _nodes; }
		}

		private TreeNode _parent;
		public TreeNode Parent
		{
			get { return _parent; }
			set 
			{
				if (value != _parent)
				{
					if (_parent != null)
						_parent.Nodes.Remove(this);

					if (value != null)
						value.Nodes.Add(this);
				}
			}
		}

		public int Index
		{
			get
			{
				if (_parent != null)
					return _parent.Nodes.IndexOf(this);
				else
					return -1;
			}
		}

		public TreeNode PreviousNode
		{
			get
			{
				int index = Index;
				if (index > 0)
					return _parent.Nodes[index - 1];
				else
					return null;
			}
		}

		public TreeNode NextNode
		{
			get
			{
				int index = Index;
				if (index >= 0 && index < _parent.Nodes.Count - 1)
					return _parent.Nodes[index + 1];
				else
					return null;
			}
		}

		private string _text;
		public virtual string Text
		{
			get { return _text; }
			set 
			{
				if (_text != value)
				{
					_text = value;
					NotifyModel();
				}
			}
		}

		private CheckState _checkState;
		public virtual CheckState CheckState
		{
			get { return _checkState; }
			set 
			{
				if (_checkState != value)
				{
					_checkState = value;
					NotifyModel();
				}
			}
		}

		public bool IsChecked
		{
			get 
			{ 
				return CheckState != CheckState.Unchecked;
			}
			set 
			{
				if (value)
					CheckState = CheckState.Checked;
				else
					CheckState = CheckState.Unchecked;
			}
		}

		public virtual bool IsLeaf
		{
			get
			{
				return false;
			}
		}

		#endregion

		public TreeNode()
			: this(string.Empty)
		{
		}

		public TreeNode(string text)
		{
			_text = text;
			_nodes = new TreeNodeCollection(this);
		}

		private TreeModel FindModel()
		{
			TreeNode node = this;
			while (node != null)
			{
				if (node.Model != null)
					return node.Model;
				node = node.Parent;
			}
			return null;
		}

		protected void NotifyModel()
		{
			TreeModel model = FindModel();
			if (model != null && Parent != null)
			{
				TreePath path = model.GetPath(Parent);
				if (path != null)
				{
					TreeModelEventArgs args = new TreeModelEventArgs(path, new int[] { Index }, new object[] { this });
					model.OnNodesChanged(args);
				}
			}
		}
	}
}
