SimpleTree
==========

A simple generic Tree implementation.

```c#
//Create the parent node
var parentNode = new TreeNode<string>("1");

//Create the child node
var childnode = new TreeNode<string>("1.1");

//Add the childnode to the parent node
parentNode.Nodes.Add(childnode);
```

Create a tree from an existing hierarchy:

```c#
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
```

Flatten a tree:
```c#
var foo1 = new Foo();
var foo1_1 = new Foo();
var foo1_2 = new Foo();

var tree = new Tree<Foo>();

tree.Nodes.Add(new TreeNode<Foo>(foo1));
tree.Nodes[0].Nodes.Add(new TreeNode<Foo>(foo1_1));
tree.Nodes[0].Nodes.Add(new TreeNode<Foo>(foo1_2));

IEnumerable<Foo> flattened = tree.Nodes.Flatten();
```
