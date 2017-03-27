using System.Collections.Generic;
using Xunit;

namespace CaptiveAire.SimpleTree.Tests
{

    public class CreateTreeFromHiearchyTests
    {
        [Fact]
        public void CreateTreeFromHierarchy()
        {
            var menuItems = new List<MenuItem>()
            {
                new MenuItem("1"),
                new MenuItem("2")
                {
                    Children = new List<MenuItem>()
                    {
                        new MenuItem("2.1"),
                        new MenuItem("2.2")
                    }
                }
            };

            //Create the tree
            var tree = menuItems.CreateTreeFromHierarchy(m => m.Children);

            Assert.Equal("1", tree.Nodes[0].Item.Text);
            Assert.Equal("2", tree.Nodes[1].Item.Text);
            Assert.Equal("2.1", tree.Nodes[1].Nodes[0].Item.Text);
            Assert.Equal("2.2", tree.Nodes[1].Nodes[1].Item.Text);
        }
    }

    public class MenuItem
    {
        public MenuItem(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public List<MenuItem> Children = new List<MenuItem>();
    }
}
