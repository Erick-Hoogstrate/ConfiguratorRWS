using Apache.NMS.ActiveMQ.Threads;
using u040.prespective.core.editor;
using u040.prespective.prepair.inspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor
{
    public abstract class StandardComponentEditorUIE<T> : PrespectiveEditorUIE<T>, IControlPanelUIE where T : Component
    {
        protected Foldout properties;
        protected IVisualElementScheduledItem updateListSchedule;
        protected IVisualElementScheduledItem updateControlPanelSchedule;


        private int updateTime = 100;

        protected override void Initialize()
        {
            #region << Live Data >>
            VisualElement liveData = root.Q<VisualElement>(name: "live-data");
            if (liveData != null)
            {
                ScheduleUpdate(liveData);
                //updateListSchedule = liveData.schedule.Execute(() => UpdateLiveData()).Every(updateTime);
            }
            #endregion

            #region << Properties >>
            properties = root.Q<Foldout>(name: "properties");
            #endregion

            #region << Control Panel >>
            VisualElement controlPanel = root.Q<VisualElement>(name: "control-panel");
            if (controlPanel == null) { return; }

            Button generateControlPanel = root.Q<Button>(name: "generate-control-panel");
            generateControlPanel.RegisterCallback<MouseUpEvent>(mouseEvent =>
            {
                ControlPanelInterface.CreateControlPanelForComponent(component);
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

        protected void ScheduleUpdate(VisualElement _visualElement)
        {
            updateListSchedule = _visualElement.schedule.Execute(() => UpdateLiveData()).Every(updateTime);
        }

        protected void ScheduleControlPanelUpdate(VisualElement _visualElement)
        {
            updateControlPanelSchedule = _visualElement.schedule.Execute(() => UpdateControlPanelData()).Every(updateTime);
        }

        protected override void ExecuteOnEnable()
        {
            theme = "standard-components";
            base.ExecuteOnEnable();
        }

        protected virtual void UpdateLiveData() { }

        protected virtual void UpdateControlPanelData() { }


        public abstract void ShowControlPanelProperties(VisualElement _container);
    }
}
