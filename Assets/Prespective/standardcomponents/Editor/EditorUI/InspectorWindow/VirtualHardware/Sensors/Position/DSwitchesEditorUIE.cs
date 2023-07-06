using System;
using System.Collections;
using u040.prespective.prepair.virtualhardware.sensors.position;
using u040.prespective.utility.bridge;
using u040.prespective.utility.editor.editorui;
using u040.prespective.utility.editor.editorui.uistatepersistence;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static u040.prespective.prepair.virtualhardware.sensors.position.DBaseSwitch;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.sensors.position
{
    public abstract class DSwitchesEditorUIE<T> : StandardComponentEditorUIE<T> where T : DBaseSwitch
    {
        #region << Live Data Fields >>
        TextField selectedState;
        #endregion

        protected VisualTreeAsset switchStateTree;
        protected Label noSaveStatesAvailable;
        protected VisualElement statesContainer;

        protected SerializedProperty switchStates;
        protected IEnumerator reselectComponent;
        private readonly int BUTTON_WIDTH = 100;

        #region << Control Panel Fields>>
        private TextField selectedStateControlPanel;
        private TextField idControlPanel;
        #endregion

        private string noSwitchStatesText = "N/A";

        protected override void executeOnEnable()
        {
            switchStateTree = Resources.Load<VisualTreeAsset>("DSwitchStateLayout");
            base.executeOnEnable();
        }

        protected override void updateLiveData()
        {
            if (component.SelectedState == null || component.SelectedState.Name == null || component.SelectedState.Name == "")
            {
                selectedState.value = noSwitchStatesText;
            }
            else
            {
                selectedState.value = "["+ component.SelectedState.Id + "] " + component.SelectedState.Name;
            }
        }

        protected override void initialize()
        {
            base.initialize();
            #region << Live Data >>
            selectedState = root.Q<TextField>(name: "selected-state");
            selectedState.isReadOnly = true;
            #endregion

            statesContainer = root.Q<VisualElement>(name: "states-container");
            noSaveStatesAvailable = root.Q<Label>(name: "no-states-available");

            ListSwitchStateLayout();
            updateStateContainerEnabled();
        }

        public void ListSwitchStateLayout()
        {
            statesContainer.Clear();
            switchStates = serializedObject.FindProperty("SwitchStates");
            int switchStatesArraySize = switchStates.arraySize;
            if (switchStatesArraySize > 0)
            {
                for (int i = 0; i < switchStatesArraySize; i++)
                {
                    createSwitchStateLayout(statesContainer, component.SwitchStates[i]);
                }
            }
        }

        protected void createSwitchStateLayout(VisualElement _container, DSwitchState _switchState)
        {
            int index = component.SwitchStates.FindIndex(_e => _e.Id == _switchState.Id);

            //clone the visual tree from the referenced UXML and add it to the container
            VisualElement switchStateRoot = switchStateTree.CloneTree();
            _container.Add(switchStateRoot);

            Foldout stateFoldout = switchStateRoot.Q<Foldout>(name: "switch-state");
            Func<string> getFoldoutText = () => "[" + _switchState.Id + "] " + _switchState.Name;
            stateFoldout.text = getFoldoutText();
            stateFoldout.name += "-" + _switchState.Id;
            UIStateUtility.InitTrackedFoldout(stateFoldout, component);

            Button selectStateButton = new Button();
            selectStateButton.text = "Select State";
            selectStateButton.style.width = BUTTON_WIDTH;
            stateFoldout.Q<Toggle>().Add(selectStateButton);
            selectStateButton.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                component.SelectState(_switchState.Id);
                EditorCoroutines.StartEditorCoroutine(redrawWindow());
            });

            TextField idField = switchStateRoot.Q<TextField>(name: "id");
            Button deleteStateButton = switchStateRoot.Q<Button>(name: "delete-state");
            deleteStateButton.style.width = BUTTON_WIDTH;
            deleteStateButton.style.marginRight = 3;
            deleteStateButton.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                component.DeleteState(index);
                switchStateRoot.parent.Remove(switchStateRoot);
                EditorCoroutines.StartEditorCoroutine(redrawWindow());
                updateStateContainerEnabled();
            });

            TextField nameField = switchStateRoot.Q<TextField>(name: "name");
            UIUtility.InitializeField
            (
                nameField,
                component,
                () => _switchState.Name,
                _e =>
                {
                    _switchState.Name = _e.newValue;
                    stateFoldout.text = getFoldoutText();
                }
            );

            DoubleField lowerWeightField = switchStateRoot.Q<DoubleField>(name: "lower-weight");
            UIUtility.InitializeField
            (
                lowerWeightField,
                component,
                () => _switchState.LowerWeight,
                _e =>
                {
                    _switchState.LowerWeight = _e.newValue;
                    component.RecalculateTransitions();
                    lowerWeightField.SetValueWithoutNotify(_switchState.LowerWeight);
                    SceneView.RepaintAll();
                }
            );

            DoubleField upperWeightField = switchStateRoot.Q<DoubleField>(name: "upper-weight");
            UIUtility.InitializeField
            (
                upperWeightField,
                component,
                () => _switchState.UpperWeight,
                _e =>
                {
                    _switchState.UpperWeight = _e.newValue;
                    component.RecalculateTransitions();
                    upperWeightField.SetValueWithoutNotify(_switchState.UpperWeight);
                    SceneView.RepaintAll();
                }
            );

            DoubleField positionField = switchStateRoot.Q<DoubleField>(name: "position");
            UIUtility.InitializeField
            (
                positionField,
                component,
                () => _switchState.Position,
                _e =>
                {
                    _switchState.Position = _e.newValue;
                    component.RecalculateTransitions();
                    SceneView.RepaintAll();
                }
            );

            SerializedProperty serializedSwitchState = switchStates.GetArrayElementAtIndex(index);

            PropertyField onSelectedField = switchStateRoot.Q<PropertyField>(name: "on-selected");
            SerializedProperty serializedOnSelectedEvent = serializedSwitchState.FindPropertyRelative("OnSelected");
            onSelectedField.BindProperty(serializedOnSelectedEvent);

            PropertyField onUnselectedField = switchStateRoot.Q<PropertyField>(name: "on-unselected");
            SerializedProperty serializedOnUnselectedEvent = serializedSwitchState.FindPropertyRelative("OnUnselected");
            onUnselectedField.BindProperty(serializedOnUnselectedEvent);
        }

        protected IEnumerator redrawWindow(VisualElement _inspectorRoot)
        {
            //1) Clear the inspector and deselect the current selection
            _inspectorRoot.Clear();
            Selection.activeObject = null;
            yield return new WaitForEndOfFrame();
            //2) Next frame, remake the selection
            Selection.activeObject = target;
            yield return null;
        }

        protected void updateStateContainerEnabled()
        {
            UIUtility.SetDisplay(statesContainer, !(statesContainer.childCount == 0));
            UIUtility.SetDisplay(noSaveStatesAvailable, statesContainer.childCount == 0);
        }

        protected IEnumerator redrawWindow()
        {
            //1) Clear the inspector and deselect the current selection
            //_inspectorRoot.Clear();
            Selection.activeObject = null;
            yield return new WaitForEndOfFrame();
            //2) Next frame, remake the selection
            Selection.activeObject = target;
            yield return null;
        }
        public override void ShowControlPanelProperties(VisualElement _container)
        {
            selectedStateControlPanel = new TextField("Selected State");
            UIUtility.SetReadOnlyState(selectedStateControlPanel, true);

            idControlPanel = new TextField("ID");
            UIUtility.SetReadOnlyState(idControlPanel, true);

            scheduleControlPanelUpdate(selectedStateControlPanel);

            _container.Add(selectedStateControlPanel);
            _container.Add(idControlPanel);
        }

        protected override void updateControlPanelData()
        {
            selectedStateControlPanel.value = component.SelectedState != null && component.SelectedState.Name != "" && Application.isPlaying ? component.SelectedState.Name.ToString() : "N/A";
            idControlPanel.value = component.SelectedState != null && Application.isPlaying ? component.SelectedState.Id.ToString() : "N/A";
        }
    }
}
