#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace u040.prespective.prepair.inspector
{
    public class ControlPanelInterface : MonoBehaviour
    {
        //Inspector value
#pragma warning disable 0414
        [SerializeField] [Obfuscation] private int toolbarTab;
#pragma warning restore 0414

        //Control Panel Title
        private const string DEFAULT_NAME = "Unassigned Control Panel";
        public string Title = DEFAULT_NAME;


        //The selected gameobject on which the component exists
        public GameObject SelectedGameObject;

        [SerializeField] [Obfuscation] private Component selectedComponent;
        public Component SelectedComponent
        {
            get
            {
                return this.selectedComponent;
            }
            set
            {
                if (this.selectedComponent != value)
                {
                    if (value != null)
                    {
                        //Check whether component is suitable
                        Editor editor = Editor.CreateEditor(value);
                        if (!(editor is IControlPanel) && !(editor is IControlPanelUIE))
                        {
                            Debug.LogError("Cannot set " + value.GetType().Name + " as the selected component since it does implement an inspector with the IControlPanel interface.");
                            return;
                        }
                    }

                    //Set new component
                    this.selectedComponent = value;

                    //Update title accordingly
                    this.Title = this.selectedComponent == null ? DEFAULT_NAME : selectedComponent.GetType().Name;
                }
            }
        }

        /// <summary>
        /// Resets the selectes Control Panel and GameObject
        /// </summary>
        public void Clear()
        {
            SelectedComponent = null;
            SelectedGameObject = null;
        }

        /// <summary>
        /// Automatically generate a ControlPanelInterface for a component which implements from the IControlPanel interface.
        /// </summary>
        /// <param name="_component"></param>
        /// <returns></returns>
        public static ControlPanelInterface CreateForComponent(Component _component)
        {
            if (_component != null)
            {
                //Check whether component is suitable
                Editor editor = Editor.CreateEditor(_component);
                if (!(editor is IControlPanel))
                {
                    Debug.LogError("Cannot create a control panel for " + _component.GetType().Name + " since editor called " + editor.GetType().Name + " it does implement an inspector with the IControlPanel interface.");
                    return null;
                }

                //Create the Control Panel and assign component
                ControlPanelInterface controlPanel = _component.gameObject.AddComponent<ControlPanelInterface>();
                controlPanel.SelectedGameObject = _component.gameObject;
                controlPanel.SelectedComponent = _component;
                return controlPanel;
            }
            return null;
        }

        /// <summary>
        /// Automatically generate a ControlPanelInterface for a component which implements from the IControlPanel interface.
        /// </summary>
        /// <param name="_component"></param>
        /// <returns></returns>
        public static ControlPanelInterface CreateControlPanelForComponent(Component _component)
        {
            if (_component != null)
            {
                //Check whether component is suitable
                Editor editor = Editor.CreateEditor(_component);
                if (!(editor is IControlPanelUIE))
                {
                    Debug.LogError("Cannot create a control panel for " + _component.GetType().Name + " since editor called " + editor.GetType().Name + " it does implement an inspector with the IControlPanel interface.");
                    return null;
                }

                //Create the Control Panel and assign component
                ControlPanelInterfaceUIE controlPanel = _component.gameObject.AddComponent<ControlPanelInterfaceUIE>();
                controlPanel.SelectedGameObject = _component.gameObject;
                controlPanel.SelectedComponent = _component;
                return controlPanel;
            }
            return null;
        }

        public static bool IsSuitable(Component _component)
        {
            if(_component != null)
            {
                Editor editor = Editor.CreateEditor(_component);
                return editor is IControlPanel;
            }
            return false;
        }

    }
}    
#endif
