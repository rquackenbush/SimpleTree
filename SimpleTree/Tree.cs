namespace CaptiveAire.SimpleTree
{
    /// <summary>
    /// A simple implementation of a generic tree.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Tree<T>
    {
        protected readonly TreeNodes<T> _nodes = new TreeNodes<T>();

        /// <summary>
        /// Gets the root nodes of the tree.
        /// </summary>
        public TreeNodes<T> Nodes
        {
            get { return _nodes;  }
        }
    }
}
