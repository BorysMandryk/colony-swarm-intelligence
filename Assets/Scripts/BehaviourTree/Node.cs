using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.XR;

namespace BehaviourTree
{
    public abstract class Node
    {
        public Node parent;

        private Dictionary<string, object> dataContext = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }

        public void SetData(string key, object value)
        {
            if (parent == null)
            {
                dataContext[key] = value;
            }
            else
            {
                parent.SetData(key, value);
            }
        }

        public object GetData(string key)
        {
            object value;
            if (dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            if (parent != null)
            {
                value = parent.GetData(key);
            }
            return value;
        }

        public void ClearData(string key)
        {
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return;
            }

            if (parent != null)
            {
                parent.ClearData(key);
            }
        }

        public abstract NodeStatus Process();
    }
}

