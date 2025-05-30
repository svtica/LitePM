using System;

namespace Aga.Controls.Tree
{
    public class TreeViewAdvEventArgs : EventArgs
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
