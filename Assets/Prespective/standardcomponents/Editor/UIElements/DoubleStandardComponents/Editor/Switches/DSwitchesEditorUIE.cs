using System;
using System.Collections;
using u040.prespective.core;
using u040.prespective.prepair.ui.buttons;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static u040.prespective.prepair.ui.buttons.DBaseSwitch;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches.editor
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
        private readonly int buttonWidth = 100;

        #region << Control Panel Fields>>
        TextField selectedStateControlPanel;
        TextField idControlPanel;
        #endregion

        string noSwitchStatesText = "N/A";

        protected override void ExecuteOnEnable()
        {
            switchStateTree = Resources.Load<VisualTreeAsset>("Switches/DSwitchStateLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
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

        protected override void Initialize()
        {
            base.Initialize();
            #region << Live Data >>
            selectedState = root.Q<TextField>(name: "selected-state");
            selectedState.isReadOnly = true;
            #endregion

            statesContainer = root.Q<VisualElement>(name: "states-container");
            noSaveStatesAvailable = root.Q<Label>(name: "no-states-available");

            ListSwitchStateLayout();
            UpdateStateContainerEnabled();
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

            //clone the visualtree from the referenced uxml and add it to the container
            VisualElement switchStateRoot = switchStateTree.CloneTree();
            _container.Add(switchStateRoot);

            Foldout stateFoldout = switchStateRoot.Q<Foldout>(name: "switch-state");
            Button selectState = new Button();
            TextField id = switchStateRoot.Q<TextField>(name: "id");
            Button deleteState = switchStateRoot.Q<Button>(name: "delete-state");
            TextField name = switchStateRoot.Q<TextField>(name: "name");
            DoubleField lowerWeight = switchStateRoot.Q<DoubleField>(name: "lower-weight");
            DoubleField upperWeight = switchStateRoot.Q<DoubleField>(name: "upper-weight");
            DoubleField position = switchStateRoot.Q<DoubleField>(name: "position");
            PropertyField onSelected = switchStateRoot.Q<PropertyField>(name: "on-selected");
            PropertyField onUnselected = switchStateRoot.Q<PropertyField>(name: "on-unselected");

            Func<string> getFoldoutText = () => "[" + _switchState.Id + "] " + _switchState.Name;

            stateFoldout.text = getFoldoutText();

            selectState.text = "Select State";
            selectState.style.width = buttonWidth;
            stateFoldout.Q<Toggle>().Add(selectState);
            selectState.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                component.SelectState(_switchState.Id);
                PersistentEditorCoroutine.StartCoroutine(redrawWindow());
            });

            deleteState.style.width = buttonWidth;
            deleteState.style.marginRight = 3;
            deleteState.RegisterCallback<MouseUpEvent>(_mouseEvent =>
            {
                component.DeleteState(index);
                switchStateRoot.parent.Remove(switchStateRoot);
                PersistentEditorCoroutine.StartCoroutine(redrawWindow());
                UpdateStateContainerEnabled();
            });

            UIUtility.InitializeField
            (
                name,
                () => _switchState.Name,
                e =>
                {
                    _switchState.Name = e.newValue;
                    stateFoldout.text = getFoldoutText();
                }
            );

            UIUtility.InitializeField
            (
                lowerWeight,
                () => _switchState.LowerWeight,
                e =>
                {
                    _switchState.LowerWeight = e.newValue;
                    component.RecalculateTransitions();
                    lowerWeight.SetValueWithoutNotify(_switchState.LowerWeight);
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                upperWeight,
                () => _switchState.UpperWeight,
                e =>
                {
                    _switchState.UpperWeight = e.newValue;
                    component.RecalculateTransitions();
                    upperWeight.SetValueWithoutNotify(_switchState.UpperWeight);
                    SceneView.RepaintAll();
                }
            );

            UIUtility.InitializeField
            (
                position,
                () => _switchState.Position,
                e =>
                {
                    _switchState.Position = e.newValue;
                    component.RecalculateTransitions();
                    SceneView.RepaintAll();
                }
            );

            SerializedProperty serializedSwitchState = switchStates.GetArrayElementAtIndex(index);

            SerializedProperty serializedOnSelectedEvent = serializedSwitchState.FindPropertyRelative("OnSelected");
            SerializedProperty serializedOnUnselectedEvent = serializedSwitchState.FindPropertyRelative("OnUnselected");

            onSelected.BindProperty(serializedOnSelectedEvent);
            onUnselected.BindProperty(serializedOnUnselectedEvent);
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

        protected void UpdateStateContainerEnabled()
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
            UIUtility.ToggleNoBoxAndReadOnly(selectedStateControlPanel, true);

            idControlPanel = new TextField("ID");
            UIUtility.ToggleNoBoxAndReadOnly(idControlPanel, true);

            ScheduleControlPanelUpdate(selectedStateControlPanel);

            _container.Add(selectedStateControlPanel);
            _container.Add(idControlPanel);
        }

        protected override void UpdateControlPanelData()
        {
            selectedStateControlPanel.value = component.SelectedState != null && component.SelectedState.Name != "" && Application.isPlaying ? component.SelectedState.Name.ToString() : "N/A";
            idControlPanel.value = component.SelectedState != null && Application.isPlaying ? component.SelectedState.Id.ToString() : "N/A";
        }
    }
}
