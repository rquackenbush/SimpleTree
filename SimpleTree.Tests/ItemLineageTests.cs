using System;
using System.Linq;
using Xunit;

namespace CaptiveAire.SimpleTree.Tests
{
 public class ItemLineageTests
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
            var parentFoo = new Foo();
            var childFoo = new Foo();

            var parentNode = new TreeNode<Foo>(parentFoo);
            var childNode = new TreeNode<Foo>(childFoo);

            parentNode.Nodes.Add(childNode);

            var lineage = childNode.GetItemLineage();

            Assert.Equal(2, lineage.Count());

            Assert.Same(childFoo, lineage.ElementAt(0));
            Assert.Same(parentFoo, lineage.ElementAt(1));

        }

        [Fact]
        public void NullTest()
        {
            TreeNode<Foo> node = null;

            var lineage = node.GetItemLineage();           

            Assert.Throws<ArgumentNullException>(() => lineage.Count());
        }
    }
}
