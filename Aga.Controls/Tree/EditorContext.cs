using Aga.Controls.Tree.NodeControls;
using System.Drawing;
using System.Windows.Forms;

namespace Aga.Controls.Tree
{
    public struct EditorContext
    {
        private TreeNodeAdv _currentNode;
        public TreeNodeAdv CurrentNode
        {
            get { return _currentNode; }
            set { _currentNode = value; }
        }

        private Control _editor;
        public Control Editor
        {
            get { return _editor; }
            set { _editor = value; }
        }

        private NodeControl _owner;
        public NodeControl Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        private Rectangle _bounds;
        public Rectangle Bounds
        {
            get { return _bounds; }
            set { _bounds = value; }
        }

        private DrawContext _drawContext;
        public DrawContext DrawContext
        {
            get { return _drawContext; }
            set { _drawContext = value; }
        }
    }
}
