using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class ControlPanelInterfaceUIE : ControlPanelInterface
    {
        /// <summary>
        /// Automatically generate a ControlPanelInterface for a component which implements from the IControlPanel interface.
        /// </summary>
        /// <param name="_component"></param>
        /// <returns></returns>
        public static ControlPanelInterface CreateControlPanelForComponent(Component _component)
        {
            if (_component != null)
            {

                //Create the Control Panel and assign component
                ControlPanelInterfaceUIE controlPanel = _component.gameObject.AddComponent<ControlPanelInterfaceUIE>();
                controlPanel.SelectedGameObject = _component.gameObject;
                controlPanel.SelectedComponent = _component;
                return controlPanel;
            }
            return null;
        }
    }
}
