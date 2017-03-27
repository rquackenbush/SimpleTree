using Xunit;

namespace CaptiveAire.SimpleTree.Tests
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

        [Fact]
        public void FirstSiblingTest()
        {
            var tree = CreateTree();

            var firstSibling = tree.Nodes[3].FirstSibling;

            Assert.Same(tree.Nodes[0], firstSibling);
        }

        [Fact]
        public void FirstSiblingNoParentTest()
        {
            var node = new TreeNode<Foo>();

            var firstSibling = node.FirstSibling;

            Assert.Null(firstSibling);
        }

        [Fact]
        public void LastSiblingTest()
        {
            var tree = CreateTree();

            var lastSibling = tree.Nodes[3].LastSibling;

            Assert.Same(tree.Nodes[3], lastSibling);
        }

        [Fact]
        public void LastSiblingNoParentTest()
        {
            var node = new TreeNode<Foo>();

            var lastSibling = node.LastSibling;

            Assert.Null(lastSibling);
        }

        [Fact]
        public void PreviousSiblingTest()
        {
            var tree = CreateTree();

            var previousSibling = tree.Nodes[3].PreviousSibling;

            Assert.Same(tree.Nodes[2], previousSibling);
        }

        [Fact]
        public void PreviousSiblingNoListTest()
        {
            var node = new TreeNode<Foo>();

            var previousSibling = node.PreviousSibling;

            Assert.Null(previousSibling);
        }

        [Fact]
        public void PreviousSiblingNoParentTest()
        {
            var tree = CreateTree();

            var previousSibling = tree.Nodes[3].PreviousSibling;

            Assert.Same(tree.Nodes[2], previousSibling);
        }

        [Fact]
        public void NoPreviousSiblingTest()
        {
            var tree = CreateTree();

            var previousSibling = tree.Nodes[0].PreviousSibling;

            Assert.Null(previousSibling);
        }

        [Fact]
        public void NextSiblingTest()
        {
            var tree = CreateTree();

            var nextSibling = tree.Nodes[2].NextSibling;

            Assert.Same(tree.Nodes[3], nextSibling);
        }

        [Fact]
        public void NextSiblingNoListTest()
        {
            var node = new TreeNode<Foo>();

            var nextSibling = node.NextSibling;

            Assert.Null(nextSibling);
        }

        [Fact]
        public void NoNextSiblingTest()
        {
            var tree = CreateTree();

            var nextSibling = tree.Nodes[3].NextSibling;

            Assert.Null(nextSibling);
        }
    }
}
