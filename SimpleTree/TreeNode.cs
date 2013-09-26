using System.ComponentModel;
using System.Linq;

namespace SimpleTree
{
    /// <summary>
    /// A node in a Tree<typeparam name="T"></typeparam>.
    /// </summary>
    /// <typeparam name="T">The item of item to represent in the tree.</typeparam>
    public class TreeNode<T> : Tree<T>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TreeNodes<T> _parentList;

        /// <summary>
        /// Creates a node with a default item.
        /// </summary>
        public TreeNode()
        {
            _nodes.Parent = this;
        }

        /// <summary>
        /// Creates a node with the specified item.
        /// </summary>
        /// <param name="item"></param>
        public TreeNode(T item)
            : this()
        {
            this.Item = item;
        }

        /// <summary>
        /// Sets the parent of this item.
        /// </summary>
        /// <param name="parentList"></param>
        internal void SetParent(TreeNodes<T> parentList)
        {
            _parentList = parentList;
        }

        /// <summary>
        /// Gets the parent node of this node.
        /// </summary>
        public TreeNode<T> Parent
        {
            get
            {
                if (_parentList == null)
                    return null;

                return _parentList.Parent;
            }
        }

        private T _item;
        /// <summary>
        /// Gets or sets the item for this tree node.
        /// </summary>
        public T Item
        {
            get { return _item; }
            set
            {
                _item = value;

                RaisePropertyChanged("Item");
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;

            if (handler == null)
                return;

            handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets the next sibling node. Returns null if this is the last node in the collection.
        /// </summary>
        public TreeNode<T> NextSibling
        {
            get
            {
                if (_parentList == null)
                    return null;

                var index = _parentList.IndexOf(this);

                if (index == -1)
                    return null;

                if (index >= _parentList.Count - 1)
                    return null;

                return _parentList[index + 1];
            }
        }

        /// <summary>
        /// Gets the previous sibling node.  Returns null if this is the first node in the collection.
        /// </summary>
        public TreeNode<T> PreviousSibling
        {
            get
            {
                if (_parentList == null)
                    return null;

                var index = _parentList.IndexOf(this);

                if (index <= 0)
                    return null;

                return _parentList[index - 1];
            }
        }

        /// <summary>
        /// Gets the first sibling of this node.
        /// </summary>
        public TreeNode<T> FirstSibling
        {
            get
            {
                if (_parentList == null)
                    return null;

                return _parentList.FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the last sibling of this node.  If this node is the last node in the collection, it will be returned.
        /// </summary>
        public TreeNode<T> LastSibling
        {
            get
            {
                if (_parentList == null)
                    return null;

                return _parentList.LastOrDefault();
            }
        }

        /// <summary>
        /// Gets the child nodes of this node.
        /// </summary>
        public TreeNodes<T> Nodes
        {
            get { return _nodes; }
        }


    }
}
