using System;
using System.Collections.Generic;
using System.Text;

namespace AdvTreeControls
{
	public interface IToolTipProvider
	{
		string GetToolTip(TreeNodeAdv node);
	}
}
