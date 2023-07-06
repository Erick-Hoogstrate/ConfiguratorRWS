using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public abstract class ControlPanelInterface : MonoBehaviour
    {
        //Control Panel Title
        private const string DEFAULT_NAME = "Unassigned Control Panel";
        
        /// <summary>
        /// Title of the Control Panel
        /// </summary>
        public string Title = DEFAULT_NAME;

        /// <summary>
        /// The Game Object of the selected Component
        /// </summary>
        public GameObject SelectedGameObject;

        [SerializeField] private Component selectedComponent;

        /// <summary>
        /// The selected component for which the Control Panel is drawn
        /// </summary>
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
                    }

                    //Set new component
                    this.selectedComponent = value;

                    //Update title accordingly
                    this.Title = this.selectedComponent == null ? DEFAULT_NAME : selectedComponent.GetType().Name;
                }
            }
        }

        /// <summary>
        /// Resets the Control Panel
        /// </summary>
        public void Clear()
        {
            SelectedComponent = null;
            SelectedGameObject = null;
        }

        /// <summary>
        /// Automatically generate a ControlPanelInterface for a component which implements the IControlPanel interface.
        /// </summary>
        /// <param name="_component"></param>
        /// <returns>The newly created Control Panel</returns>
        public static ControlPanelInterface CreateForComponent(Component _component)
        {
            if (_component != null)
            {
                //Create the Control Panel and assign component
                ControlPanelInterface controlPanel = _component.gameObject.AddComponent<ControlPanelInterface>();
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
            }
            return false;
        }
    }
}
