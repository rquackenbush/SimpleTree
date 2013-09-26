using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SimpleTree.Tests
{
    [TestFixture]
    public class FlattenTests
    {
        [Test]
        public void FlattenSingle()
        {
            var foo = new Foo();

            var tree = new Tree<Foo>();

            tree.Nodes.Add(new TreeNode<Foo>(foo));

            var flattened = tree.Nodes.Flatten();

            Assert.AreEqual(1, flattened.Count());

            Assert.IsTrue(flattened.Contains(foo));
        }

        [Test]
        public void FlattenHierarchy()
        {
            var foo1 = new Foo();
            var foo1_1 = new Foo();
            var foo1_2 = new Foo();

            var tree = new Tree<Foo>();

            tree.Nodes.Add(new TreeNode<Foo>(foo1));
            tree.Nodes[0].Nodes.Add(new TreeNode<Foo>(foo1_1));
            tree.Nodes[0].Nodes.Add(new TreeNode<Foo>(foo1_2));

            var flattened = tree.Nodes.Flatten();

            Assert.AreEqual(3, flattened.Count());

            Assert.IsTrue(flattened.Contains(foo1));
            Assert.IsTrue(flattened.Contains(foo1_1));
            Assert.IsTrue(flattened.Contains(foo1_2));
        }
    }
}
