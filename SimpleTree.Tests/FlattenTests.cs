using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CaptiveAire.SimpleTree.Tests
{
    public class FlattenTests
    {
        [Fact]
        public void FlattenSingle()
        {
            var foo = new Foo();

            var tree = new Tree<Foo>();

            tree.Nodes.Add(new TreeNode<Foo>(foo));

            var flattened = tree.Nodes.Flatten();

            Assert.Equal(1, flattened.Count());

            Assert.True(flattened.Contains(foo));
        }

        [Fact]
        public void FlattenHierarchy()
        {
            var foo1 = new Foo();
            var foo1_1 = new Foo();
            var foo1_2 = new Foo();

            var tree = new Tree<Foo>();

            tree.Nodes.Add(new TreeNode<Foo>(foo1));
            tree.Nodes[0].Nodes.Add(new TreeNode<Foo>(foo1_1));
            tree.Nodes[0].Nodes.Add(new TreeNode<Foo>(foo1_2));

            IEnumerable<Foo> flattened = tree.Nodes.Flatten();

            Assert.Equal(3, flattened.Count());

            Assert.True(flattened.Contains(foo1));
            Assert.True(flattened.Contains(foo1_1));
            Assert.True(flattened.Contains(foo1_2));
        }
    }
}
