using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CaptiveAire.SimpleTree.Tests
{
    public class CreateTreeTests
    {
        [Fact]
        public void CreateTree()
        {
            var bar0 = new Bar(1, null);
            var bar1 = new Bar(2, null);
            var bar0_0 = new Bar(3, 1);
            var bar0_1 = new Bar(4, 1);
            var bar_0_0_0 = new Bar(5, 3);

            var list = new List<Bar>()
                {
                    bar0, 
                    bar1, 
                    bar0_0,
                    bar0_1,
                    bar_0_0_0
                };

            //Create the tree
            var tree = TreeExtensions.CreateTree(list.Where(b => b.ParentId == null), 
                list, 
                (p, c) => c.ParentId != null && c.ParentId.Value == p.Id);

            Assert.Same(bar0, tree.Nodes[0].Item);

            Assert.Same(bar1, tree.Nodes[1].Item);

            Assert.Same(bar0_1, tree.Nodes[0].Nodes[1].Item);
        }

        [Fact]
        public void CreateTree2()
        {
            var bar0 = new Bar(1, null);
            var bar1 = new Bar(2, null);
            var bar0_0 = new Bar(3, 1);
            var bar0_1 = new Bar(4, 1);
            var bar_0_0_0 = new Bar(5, 3);

            var list = new List<Bar>()
                {
                    bar0, 
                    bar1, 
                    bar0_0,
                    bar0_1,
                    bar_0_0_0
                };



            //Create the tree
            var tree = list.CreateTree(b => b.ParentId == null, (p, c) => c.ParentId != null && c.ParentId.Value == p.Id);

            Assert.Same(bar0, tree.Nodes[0].Item);
            
            Assert.Same(bar1, tree.Nodes[1].Item);

            Assert.Same(bar0_1, tree.Nodes[0].Nodes[1].Item);
        }
    }
}
