using u040.prespective.core.editor.editorui;
using u040.prespective.core.editor.editorui.inspectorwindow;
using u040.prespective.standardcomponents.userinterface;
using u040.prespective.utility.editor.editorui.uistatepersistence;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow
{
    public abstract class StandardComponentEditorUIE<T> : PrespectiveEditorUIE<T>, IControlPanelUIE where T : MonoBehaviour
    {
        protected Foldout properties;
        protected IVisualElementScheduledItem updateListSchedule;
        protected IVisualElementScheduledItem updateControlPanelSchedule;


        private int updateTime = 100;

        protected override void initialize()
        {
            #region << Live Data >>
            //Live data is not persistently implemented as a foldout, apply foldout tracking when it is.
            Foldout liveDataFoldout = root.Q<Foldout>("live-data");

            if (liveDataFoldout != null)
            {
                UIStateUtility.InitTrackedFoldout(liveDataFoldout, component);
            }

            //Foldouts can also be queried as a VisualElement, because of inheritance.
            VisualElement liveDataElement = root.Q<VisualElement>(name: "live-data");

            if (liveDataElement != null)
            {
                scheduleUpdate(liveDataElement);
            }
            #endregion

            #region << Properties >>
            properties = root.Q<Foldout>(name: "properties");
            if (properties != null) 
            { 
                UIStateUtility.InitTrackedFoldout(properties, component);
            }
            #endregion

            #region << Control Panel >>
            VisualElement controlPanel = root.Q<VisualElement>(name: "control-panel");
            if (controlPanel == null) { return; }

            Button generateControlPanel = root.Q<Button>(name: "generate-control-panel");
            generateControlPanel.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                ControlPanelInterfaceUIE.CreateControlPanelForComponent(component);
            });
            #endregion

            #region << Playmode Checks >>
            EditorApplication.playModeStateChanged += playState =>
            {
                if (properties != null)
                {
                    properties.contentContainer.SetEnabled(!(playState == PlayModeStateChange.EnteredPlayMode));
                }
            };
            #endregion
        }

        protected void scheduleUpdate(VisualElement _visualElement)
        {
            updateListSchedule = _visualElement.schedule.Execute(() => updateLiveData()).Every(updateTime);
        }

        protected void scheduleControlPanelUpdate(VisualElement _visualElement)
        {
            updateControlPanelSchedule = _visualElement.schedule.Execute(() => updateControlPanelData()).Every(updateTime);
        }

        protected override void executeOnEnable()
        {
            theme = VisualTheme.StandardComponents;
        }

        protected virtual void updateLiveData() { }

        protected virtual void updateControlPanelData() { }


        public abstract void ShowControlPanelProperties(VisualElement _container);
    }
}
