using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SimpleTree.Tests
{
    public class SiblingTests
    {
        private Tree<Foo> CreateTree()
        {
            var tree = new Tree<Foo>();

            tree.Nodes.Add(new TreeNode<Foo>());
            tree.Nodes.Add(new TreeNode<Foo>());
            tree.Nodes.Add(new TreeNode<Foo>());
            tree.Nodes.Add(new TreeNode<Foo>());

            return tree;
        }

        [Test]
        public void FirstSiblingTest()
        {
            var tree = CreateTree();

            var firstSibling = tree.Nodes[3].FirstSibling;

            Assert.AreSame(tree.Nodes[0], firstSibling);
        }

        [Test]
        public void FirstSiblingNoParentTest()
        {
            var node = new TreeNode<Foo>();

            var firstSibling = node.FirstSibling;

            Assert.IsNull(firstSibling);
        }

        [Test]
        public void LastSiblingTest()
        {
            var tree = CreateTree();

            var lastSibling = tree.Nodes[3].LastSibling;

            Assert.AreSame(tree.Nodes[3], lastSibling);
        }

        [Test]
        public void LastSiblingNoParentTest()
        {
            var node = new TreeNode<Foo>();

            var lastSibling = node.LastSibling;

            Assert.IsNull(lastSibling);
        }

        [Test]
        public void PreviousSiblingTest()
        {
            var tree = CreateTree();

            var previousSibling = tree.Nodes[3].PreviousSibling;

            Assert.AreSame(tree.Nodes[2], previousSibling);
        }

        [Test]
        public void PreviousSiblingNoListTest()
        {
            var node = new TreeNode<Foo>();

            var previousSibling = node.PreviousSibling;

            Assert.IsNull(previousSibling);
        }

        [Test]
        public void PreviousSiblingNoParentTest()
        {
            var tree = CreateTree();

            var previousSibling = tree.Nodes[3].PreviousSibling;

            Assert.AreSame(tree.Nodes[2], previousSibling);
        }

        [Test]
        public void NoPreviousSiblingTest()
        {
            var tree = CreateTree();

            var previousSibling = tree.Nodes[0].PreviousSibling;

            Assert.IsNull(previousSibling);
        }

        [Test]
        public void NextSiblingTest()
        {
            var tree = CreateTree();

            var nextSibling = tree.Nodes[2].NextSibling;

            Assert.AreSame(tree.Nodes[3], nextSibling);
        }

        [Test]
        public void NextSiblingNoListTest()
        {
            var node = new TreeNode<Foo>();

            var nextSibling = node.NextSibling;

            Assert.IsNull(nextSibling);
        }

        [Test]
        public void NoNextSiblingTest()
        {
            var tree = CreateTree();

            var nextSibling = tree.Nodes[3].NextSibling;

            Assert.IsNull(nextSibling);
        }
    }
}
