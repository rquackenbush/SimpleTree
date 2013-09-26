using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SimpleTree.Tests
{
    [TestFixture]
    public class TreeTests
    {
        [Test]
        public void RetrieveSingleNode()
        {
            //Create the tree
            var tree = new Tree<Foo>();

            //Create a simple object to put in the tree
            var foo = new Foo();

            //Add the item in a new node
            tree.Nodes.Add(new TreeNode<Foo>(foo));

            //Make sure that the item is represented
            Assert.AreSame(foo, tree.Nodes[0].Item);
        }

        [Test]
        public void CountTest()
        {
            //Create the tree
            var tree = new Tree<Foo>();

            //Add two nodes
            tree.Nodes.Add(new TreeNode<Foo>(null));
            tree.Nodes.Add(new TreeNode<Foo>(null));

            Assert.AreEqual(2, tree.Nodes.Count);
        }

        [Test]
        public void ParentTest()
        {
            //Create the parent node
            var parentNode = new TreeNode<Foo>(null);

            //Create the child node
            var childnode = new TreeNode<Foo>(null);

            //Add the childnode to the parent node
            parentNode.Nodes.Add(childnode);

            //Make sure that the parent is set correctly
            Assert.AreSame(parentNode, childnode.Parent);

        }

        [Test]
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
            Assert.IsNull(childnode.Parent);
        }

        [Test]
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
            Assert.IsNull(childnode.Parent);
        }
    
        [Test]
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
            Assert.IsNull(childnode.Parent);

            //Make sure the new child has the parent correctly set
            Assert.AreSame(differentChildNode.Parent, parentNode);
        }
    }
}
