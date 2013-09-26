using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SimpleTree.Tests
{
    [TestFixture]
    public class NodeLineageTests
    {
        [Test]
        public void NoLineage()
        {
            var foo = new Foo();

            var node = new TreeNode<Foo>(foo);

            var lineage = node.GetItemLineage();

            Assert.AreEqual(1, lineage.Count());
            Assert.AreSame(foo, lineage.First());
        }

        [Test]
        public void SingleParent()
        {

            var parentNode = new TreeNode<Foo>();
            var childNode = new TreeNode<Foo>();

            parentNode.Nodes.Add(childNode);

            var lineage = childNode.GetNodeLineage();

            Assert.AreEqual(2, lineage.Count());

            Assert.AreSame(childNode, lineage.ElementAt(0));
            Assert.AreSame(parentNode, lineage.ElementAt(1));

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullTest()
        {
            TreeNode<Foo> node = null;

            var lineage = node.GetNodeLineage();

            Assert.AreEqual(1, lineage.Count());
        }
    }
}
