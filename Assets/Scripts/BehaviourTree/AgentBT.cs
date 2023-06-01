using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class AgentBT : MonoBehaviour
    {
        private Node root;

        protected void Start()
        {
            root = SetupRoot();
        }

        private void Update()
        {
            if (Time.timeScale != 0)
            {
                root?.Process();
            }
        }

        protected abstract Node SetupRoot();
    }
}

