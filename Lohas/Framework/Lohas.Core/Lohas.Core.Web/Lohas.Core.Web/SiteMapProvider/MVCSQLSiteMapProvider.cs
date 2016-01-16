//========================================================================
// Copyright(C): Lohas
//
// CLR Version : 4.0.30319.42000
// NameSpace : Lohas.Core.Web.SiteMapProvider
// FileName : MVCSQLSiteMapProvider
// Created by : Jerry (hongjian_kong@outlook.com) at 2016/1/18 11:13:46
// Function :MVCSQLSiteMapProvider  
//================================================================

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Routing;
using DataAccess.EntityFramework;

namespace Lohas.Core.Web.SiteMapProvider
{
    public class MVCSQLSiteMapProvider : StaticSiteMapProvider
    {
        private string _connect = String.Empty;
        private Dictionary<Guid, SiteMapNode> _nodes = new Dictionary<Guid, SiteMapNode>();
        private SiteMapNode _root;
        public override void Initialize(string name, NameValueCollection attributes)
        {
            if (attributes == null)
            {
                throw new ArgumentNullException("attributes");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "MvcSitemapProvider";
            }

            if (string.IsNullOrEmpty(attributes["description"]))
            {
                attributes.Remove("description");
                attributes.Add("description", "MVC site map provider");
            }
            base.Initialize(name,attributes);

           
            if (attributes.Count > 0)
            {
                string attr = attributes.GetKey(0);
                if (!string.IsNullOrEmpty(attr))
                    throw new ProviderException(string.Format(
                      "Unrecognized attribute: {0}", attr));
            }
        }


        protected override SiteMapNode GetRootNodeCore()
        {
            var rootNode = new SiteMapNode(this,Guid.Empty.ToString());
            return rootNode;
        }

        public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
        {
            lock (this)
            {
                using (var context = new LohasContext())
                {
                    var maps = context.Systems_SiteMap.Where(p=>p.ParentID.ToString()==node.Key).OrderBy(p => p.ID).ToList();
                    foreach (var item in maps)
                    {
                        AddNode(CreateSiteMapFromRow(item), node);
                    }
                }

               // return _root;
            }
            return base.GetChildNodes(node);
        }

        public override SiteMapNode BuildSiteMap()
        {
            lock (this)
            {
                if (_root != null)
                    return _root;
                using (var context=new LohasContext())
                {
                    var maps = context.Systems_SiteMap.OrderBy(p => p.ID).ToList();
                    foreach (var item in maps)
                    {
                        if (item.Equals(maps.First()))
                        {
                            if(item.ParentID==Guid.Empty) 
                            _root = CreateSiteMapFromRow(item);
                            AddNode(_root, null);
                        }
                        else
                        {
                            SiteMapNode node = CreateSiteMapFromRow(item);
                            AddNode(node, GetParentNodeFromNode(item));
                        }
                    }
                }
                
                return _root;
            }
        }

        private SiteMapNode GetParentNodeFromNode(Systems_SiteMap item)
        {
            if (!_nodes.ContainsKey(item.ParentID))
                throw new ProviderException("Invalid parent ID");
            return _nodes[item.ParentID];
        }

        private SiteMapNode CreateSiteMapFromRow(Systems_SiteMap item)
        {
            if (_nodes.ContainsKey(item.ID))
                throw new ProviderException("MDuplicate node ID");
            var node = new SiteMapNode(this, item.ID.ToString());
            if (!string.IsNullOrEmpty(item.Url))
            {
                node.Title = string.IsNullOrEmpty(item.Title) ? null : item.Title;
                node.Description =
                 string.IsNullOrEmpty(item.Description) ? null : item.Description;
                node.Url = string.IsNullOrEmpty(item.Url) ? null : item.Url;
            }
            else
            {
                node.Title = string.IsNullOrEmpty(item.Title) ? null : item.Title;
                node.Description =
                 string.IsNullOrEmpty(item.Description) ? null : item.Description;
                IDictionary<string, string> routeValues = new Dictionary<string, string>();
                if (string.IsNullOrEmpty(item.UrlController))
                    routeValues.Add("controller", "Home");
                else
                    routeValues.Add("controller", item.UrlController);
                if (string.IsNullOrEmpty(item.UrlController))
                    routeValues.Add("action", "Index");
                else
                    routeValues.Add("action", item.UrlAction);
                
                HttpContextWrapper httpContext = new HttpContextWrapper(HttpContext.Current);
                RouteData routeData = RouteTable.Routes.GetRouteData(httpContext);
                if (routeData != null)
                {
                    VirtualPathData virtualPath = routeData.Route.GetVirtualPath(
                      new RequestContext(httpContext, routeData),
                      new RouteValueDictionary(routeValues));
                    if (virtualPath != null)
                    {
                        node.Url = "~/" + virtualPath.VirtualPath;
                    }
                }
            }
            _nodes.Add(item.ID, node);
            return node;
        }
    }
}
