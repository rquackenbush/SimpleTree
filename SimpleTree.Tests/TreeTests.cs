using Xunit;

namespace CaptiveAire.SimpleTree.Tests
{
    public class TreeTests
    {
        [Fact]
        public void RetrieveSingleNode()
        {
            //Create the tree
            var tree = new Tree<Foo>();

            //Create a simple object to put in the tree
            var foo = new Foo();

            //Add the item in a new node
            tree.Nodes.Add(new TreeNode<Foo>(foo));

            //Make sure that the item is represented
            Assert.Same(foo, tree.Nodes[0].Item);
        }

        [Fact]
        public void CountTest()
        {
            //Create the tree
            var tree = new Tree<Foo>();

            //Add two nodes
            tree.Nodes.Add(new TreeNode<Foo>(null));
            tree.Nodes.Add(new TreeNode<Foo>(null));

            Assert.Equal(2, tree.Nodes.Count);
        }

        [Fact]
        public void ParentTest()
        {
            //Create the parent node
            var parentNode = new TreeNode<Foo>(null);

            //Create the child node
            var childnode = new TreeNode<Foo>(null);

            //Add the childnode to the parent node
            parentNode.Nodes.Add(childnode);

            //Make sure that the parent is set correctly
            Assert.Same(parentNode, childnode.Parent);

        }

        [Fact]
        public void ParentIsNullWithClear()
        {
            //Create the parent node
            var parentNode = new TreeNode<Foo>(null);

            //Create the child node
            var childnode = new TreeNode<Foo>(null);

            //Add the childnode to the parent node
            parentNode.Nodes.Add(childnode);

            //Ditch the child nodes
            parentNode.Nodes.Clear();

            //Make sure that the parent is cleared
            Assert.Null(childnode.Parent);
        }

        [Fact]
        public void ParentIsNullWithRemove()
        {
            //Create the parent node
            var parentNode = new TreeNode<Foo>(null);

            //Create the child node
            var childnode = new TreeNode<Foo>(null);

            //Add the childnode to the parent node
            parentNode.Nodes.Add(childnode);

            //Ditch the child nodes
            parentNode.Nodes.Remove(childnode);

            //Make sure that the parent is cleared
            Assert.Null(childnode.Parent);
        }

        [Fact]
        public void SetNodeTest()
        {
            //Create the parent node
            var parentNode = new TreeNode<Foo>();

            //Create the child node
            var childnode = new TreeNode<Foo>();

            //Add the childnode to the parent node
            parentNode.Nodes.Add(childnode);

            var differentChildNode = new TreeNode<Foo>();

            parentNode.Nodes[0] = differentChildNode;

            //Make sure the old parent was cleared
            Assert.Null(childnode.Parent);

            //Make sure the new child has the parent correctly set
            Assert.Same(differentChildNode.Parent, parentNode);
        }
    }
}
