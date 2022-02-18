using System;
using System.Collections.Generic;
using System.Text;

namespace AdvTreeControls
{
	public class TreeViewAdvEventArgs: EventArgs
	{
		private TreeNodeAdv _node;

		public TreeNodeAdv Node
		{
			get { return _node; }
		}

		public TreeViewAdvEventArgs(TreeNodeAdv node)
		{
			_node = node;
		}
	}
}
