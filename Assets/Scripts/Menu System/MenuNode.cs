using System;
using System.Collections.Generic;


public class MenuNode
{
    public string Title;
    public List<MenuNode> Items = new List<MenuNode>();
    public Func<string> LabelFunc;  
    public Action OnSelect;         

    public bool IsLeaf => OnSelect != null;

    public MenuNode(string title) { Title = title; }

    public string GetLabel() => LabelFunc != null ? LabelFunc() : Title;

    public void AddChild(MenuNode child) {
        Items.Add(child);
    } 

    public void AddLeaf(Func<string> label, Action onSelect)
    {
        Items.Add(new MenuNode("Leaf") { LabelFunc = label, OnSelect = onSelect });
    }
}
