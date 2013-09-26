using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTree;

namespace SimpleTree.Tests
{
    [TestFixture]
    public class ItemLineageTests
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
            var parentFoo = new Foo();
            var childFoo = new Foo();

            var parentNode = new TreeNode<Foo>(parentFoo);
            var childNode = new TreeNode<Foo>(childFoo);

            parentNode.Nodes.Add(childNode);

            var lineage = childNode.GetItemLineage();

            Assert.AreEqual(2, lineage.Count());

            Assert.AreSame(childFoo, lineage.ElementAt(0));
            Assert.AreSame(parentFoo, lineage.ElementAt(1));

        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void NullTest()
        {
            TreeNode<Foo> node = null;

            var lineage = node.GetItemLineage();           

            Assert.AreEqual(1, lineage.Count());
        }
    }
}
