using System;
using UnityEngine;

namespace ScriptableObjects
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T:ScriptableObject
    {
        private static T _instance=null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T scriptableObjets = Resources.Load("ScriptableObjects/Settings") as T;
                    if (scriptableObjets == null)
                    {
                        Debug.LogError("No scriptable object found!");
                        throw new NullReferenceException();
                    }
                    
                    _instance = scriptableObjets;
                }

                return _instance;
            }
            
        }
    }
}
