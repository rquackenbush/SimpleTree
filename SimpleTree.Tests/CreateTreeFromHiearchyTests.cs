using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SimpleTree.Tests
{
    [TestFixture]
    public class CreateTreeFromHiearchyTests
    {
        [Test]
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

            Assert.AreEqual("1", tree.Nodes[0].Item.Text);
            Assert.AreEqual("2", tree.Nodes[1].Item.Text);
            Assert.AreEqual("2.1", tree.Nodes[1].Nodes[0].Item.Text);
            Assert.AreEqual("2.2", tree.Nodes[1].Nodes[1].Item.Text);
        }
    }

    public class MenuItem
    {
        public MenuItem()
        {
            
        }

        public MenuItem(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public List<MenuItem> Children = new List<MenuItem>();
    }
}
