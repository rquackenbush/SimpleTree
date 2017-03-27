using System;
using System.Linq;
using Xunit;

namespace CaptiveAire.SimpleTree.Tests
{
    public class NodeLineageTests
    {
        [Fact]
        public void NoLineage()
        {
            var foo = new Foo();

            var node = new TreeNode<Foo>(foo);

            var lineage = node.GetItemLineage();

            Assert.Equal(1, lineage.Count());
            Assert.Same(foo, lineage.First());
        }

        [Fact]
        public void SingleParent()
        {

            var parentNode = new TreeNode<Foo>();
            var childNode = new TreeNode<Foo>();

            parentNode.Nodes.Add(childNode);

            var lineage = childNode.GetNodeLineage();

            Assert.Equal(2, lineage.Count());

            Assert.Same(childNode, lineage.ElementAt(0));
            Assert.Same(parentNode, lineage.ElementAt(1));

        }

        [Fact]
        public void NullTest()
        {
            TreeNode<Foo> node = null;

            var lineage = node.GetNodeLineage();

            Assert.Throws<ArgumentNullException>(() => lineage.Count());
        }
    }
}
