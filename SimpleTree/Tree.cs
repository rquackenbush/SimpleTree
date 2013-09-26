namespace SimpleTree
{
    /// <summary>
    /// A simple implementation of a generic tree.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Tree<T>
    {
        protected readonly TreeNodes<T> _nodes = new TreeNodes<T>();

        public Tree()
        {
             
        }

        public TreeNodes<T> Nodes
        {
            get { return _nodes;  }
        }
    }

    

    
}
