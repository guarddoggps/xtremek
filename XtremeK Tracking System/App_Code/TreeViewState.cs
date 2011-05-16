using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;

/// <summary>
/// Summary description for TreeViewState
/// </summary>
public class TreeViewState
{
    public void SaveTreeView(RadTreeView treeView, string key)
    {
        List<bool?> list = new List<bool?>();
        SaveTreeViewExpandedState(treeView.Nodes, list);
        HttpContext.Current.Session[key + treeView.ID] = list;
    }

    private void SaveTreeViewExpandedState(RadTreeNodeCollection nodes, List<bool?> list)
    {
        foreach (RadTreeNode node in nodes)
        {
            list.Add(node.Expanded);
            if (node.Nodes.Count > 0)
            {
                SaveTreeViewExpandedState(node.Nodes, list);

                foreach (RadTreeNode _node in node.Nodes)
                {
                    if (_node.Selected)
                    {
                        HttpContext.Current.Session["_selectedNode"] = _node;
                    }
                }

            }
        }
    }

    private int RestoreTreeViewIndex;

    public void RestoreTreeView(RadTreeView treeView, string key)
    {
        RestoreTreeViewIndex = 0;
        RestoreTreeViewExpandedState(treeView.Nodes,
            (List<bool?>)HttpContext.Current.Session[key + treeView.ID] ?? new List<bool?>());
    }

    private void RestoreTreeViewExpandedState(RadTreeNodeCollection nodes, List<bool?> list)
    {



        foreach (RadTreeNode node in nodes)
        {
            if (RestoreTreeViewIndex >= list.Count) return;

            node.Expanded = (bool)list[RestoreTreeViewIndex++];
            if (node.Nodes.Count > 0)
            {
                RestoreTreeViewExpandedState(node.Nodes, list);
                RadTreeNode _selectedNode = new RadTreeNode();

                _selectedNode = (RadTreeNode)HttpContext.Current.Session["_selectedNode"];

                foreach (RadTreeNode _node in node.Nodes)
                {
                    if (_selectedNode != null)
                    {
                        string _nodeText = _node.Text;
                        string _selectedNodeText = _selectedNode.Text;
                        if (_node.Text == _selectedNode.Text)
                            _node.Selected = true;
                    }
                }
            }
        }
    }
}
