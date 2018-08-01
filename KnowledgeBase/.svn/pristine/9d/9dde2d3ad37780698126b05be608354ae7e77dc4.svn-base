using Microsoft.Extensions.Options;
using SSOManagerLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SSOManagerLib
{
    public class AddAttibute
    {
        public static IDictionary<string, AttrCache> _AttrCacheDic = new Dictionary<string, AttrCache>();

        public  AddAttibute(IOptions<ModuleOption> options)
        {

            if (_AttrCacheDic.Values.Count > 0)
                return;
            var option = options.Value;
            Assembly assembly = Assembly.LoadFrom(option.DllName);
            // List<AttrCache> lcache = new List<AttrCache>();
            foreach (var cls in option.ClassList)
            {
                var t = assembly.GetType(cls.ClassName);
                var controller = t.Name.Substring(0, t.Name.Length - 10);
                if (t.GetCustomAttribute<NoAthorize>() != null)
                {
                    if (!_AttrCacheDic.Values.Any(o => o.ControllerName == controller))
                    {
                        _AttrCacheDic.Add(Guid.NewGuid().ToString(), new AttrCache { ControllerName = t.Name.Substring(0, t.Name.Length - 10), ActionName = "" });
                    }

                }
                else
                {
                    var methods = t.GetMethods();
                    
                    foreach (var method in methods)
                    {
                        if (method.GetCustomAttribute<NoAthorize>() != null)
                        {
                            if (!_AttrCacheDic.Values.Any(o => o.ActionName == method.Name))
                            {
                                _AttrCacheDic.Add(Guid.NewGuid().ToString(), new AttrCache { ControllerName = t.Name.Substring(0, t.Name.Length - 10), ActionName = method.Name });
                            }
                        }
                    }
                }
            }
        }

        //
        public bool GetActionAttr(string action,string controller)
        {

            string actionname = action;
            string controllername = controller;
            if (_AttrCacheDic.Count == 0)
                return false;
            foreach (var item in _AttrCacheDic.Values)
            {
                if (item.ActionName == actionname && item.ControllerName == controllername)
                    return true;
            }
            return false;
        }

        public bool GetControllerAttr(string controller)
        {


            string controllername = controller;
            if (_AttrCacheDic.Count == 0)
                return false;
            foreach (var item in _AttrCacheDic.Values)
            {
                if (item.ControllerName == controllername && string.IsNullOrEmpty(item.ActionName))
                    return true;
            }
            return false;
        }
    }
}