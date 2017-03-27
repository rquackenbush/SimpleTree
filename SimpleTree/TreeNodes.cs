using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CaptiveAire.SimpleTree
{
    /// <summary>
    /// A collection of tree nodes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNodes<T> : ObservableCollection<TreeNode<T>>
    {
        internal TreeNodes()
        {
        }

        public TreeNode<T> Parent { get; internal set; }

        protected override void InsertItem(int index, TreeNode<T> item)
        {
            item.SetParent(this);

            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, TreeNode<T> item)
        {
            var previousItem = this[index];

            previousItem.SetParent(null);

            base.SetItem(index, item);

            item.SetParent(this);
        }

        protected override void RemoveItem(int index)
        {
            var node = this[index];

            node.SetParent(null);

            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            foreach (var item in this)
            {
                item.SetParent(null);
            }

            base.ClearItems();
        }
    }
}
