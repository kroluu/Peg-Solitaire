using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TUiPanelElement">Enum to represent panels in scene</typeparam>
    /// <typeparam name="TUiContext">Enums to hold context of game</typeparam>
    public abstract class UiCoreController<TUiPanelElement,TUiContext> : MonoBehaviour 
    {
        /// <summary>
        /// Stores current context in game. Switch it by using SwitchContext() method.
        /// </summary>
        protected TUiContext CurrentContext { get; set; }
        
        /// <summary>
        /// Stores previous context in case of going back in contexts.
        /// </summary>
        protected TUiContext PreviousContext { get; set; }
        
        /// <summary>
        /// Type of controller. Used to recognize type of controller in container.
        /// </summary>
        protected abstract CoreContext CoreContext { get; set; }
        
        /// <summary>
        /// Container of GameObjects in scene
        /// </summary>
        private readonly Dictionary<TUiPanelElement,Transform> listOfPanels = new Dictionary<TUiPanelElement,Transform>();
        
        /// <summary>
        /// Controller references container. 
        /// </summary>
        private static readonly  Dictionary<CoreContext,UiCoreController<TUiPanelElement,TUiContext>> coreInstances = 
            new Dictionary<CoreContext, UiCoreController<TUiPanelElement, TUiContext>>();
        
        /// <summary>
        /// Switches context.
        /// Implementation must assign current context to previous context and assign new context to current context.
        /// Depends on context SetPanel() must be run.
        /// </summary>
        /// <param name="context"></param>
        public abstract void SwitchContext(TUiContext context);

        protected abstract void Awake();
        protected abstract void Start();
        
        /// <summary>
        /// Use to add controller reference to dictionary.
        /// </summary>
        /// <param name="instanceToAssign">Controller reference that is derriving from UiCoreController.</param>
        protected void AssignReferenceToCore(UiCoreController<TUiPanelElement,TUiContext> instanceToAssign)
        {
            if (coreInstances.ContainsKey(CoreContext) && coreInstances[CoreContext] == null)
            {
                coreInstances[CoreContext] = instanceToAssign;
                return;
            }
                
            coreInstances.Add(CoreContext,instanceToAssign);
        }
        
        /// <summary>
        /// Use to get controller's instance from context you passed as parameter. 
        /// </summary>
        /// <param name="_coreContext">Type of controller from which you want to get instance</param>
        /// <returns>Return instance of specific controller</returns>
        public static T GetController<T>(CoreContext _coreContext) where T: UiCoreController<TUiPanelElement,TUiContext>
        {
            return coreInstances[_coreContext] as T;
        }
        
        /// <summary>
        /// Finds all GameObjects in scene hierarchy. Be aware that GameObjects must have same name in hierarchy as panel enums.
        /// Call it on Awake() before using SwitchContext().
        /// </summary>
        protected void FindPanelsInHierarchy()
        {
            List<Transform> foundPanels = new List<Transform>();
            foreach (Transform child in transform)
            {
                foundPanels.Add(child);
            }
            foreach (TUiPanelElement uiPanelElement in (TUiPanelElement[])Enum.GetValues(typeof(TUiPanelElement)))
            {
                Transform foundPanel = foundPanels.FirstOrDefault(x => x.name.Equals(uiPanelElement.ToString()));
                if (foundPanel)
                {
                    listOfPanels.Add(uiPanelElement,foundPanel);
                }
            }
            
        }
        
        /// <summary>
        /// Call to set all panels that are passed as parameter as active. Hides rest of panels.
        /// </summary>
        /// <param name="panelsToActive">Enum types passed to finds it in panels container</param>
        protected void SetPanel(params TUiPanelElement[] panelsToActive)
        {
            foreach (KeyValuePair<TUiPanelElement,Transform> pair in listOfPanels)
            {
                /*pair.Value.gameObject.SetActive(panelsToActive.Any(x=>x.Equals(pair.Key)));*/
                if (panelsToActive.Any(x => x.Equals(pair.Key)))
                {
                    var inf = pair.Value.gameObject.GetComponent<IPanelViewController>();
                    pair.Value.gameObject.GetComponent<IPanelViewController>().ShowPanel();
                }
                else
                {
                    pair.Value.gameObject.GetComponent<IPanelViewController>().HidePanel();
                }
                
            }
        }
        
    }
}

