using UnityEngine;

namespace Pattern
{
    public abstract class Singleton<T> : MonoBehaviour where T:MonoBehaviour,new()
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<T>();
                }

                return instance;
            }
            
        }

        protected void AssignInstance(T _instance)
        {
            if(instance==null)
                instance = _instance;
        }
        
    }
}
