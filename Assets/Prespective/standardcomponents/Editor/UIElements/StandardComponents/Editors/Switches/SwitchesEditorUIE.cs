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
using static u040.prespective.prepair.ui.buttons.BaseSwitch;

namespace u040.prespective.standardcomponents.userinterface.buttons.switches.editor
{
    public abstract class SwitchesEditorUIE<T> : StandardComponentEditorUIE<T> where T : BaseSwitch
    {

        #region << FIELDS >>
        protected VisualTreeAsset switchStateTree;
        protected Label noSaveStatesAvailable;
        protected VisualElement statesContainer;

        protected SerializedProperty switchStates;
        protected IEnumerator reselectComponent;
        TextField selectedStateControlPanel;
        TextField idControlPanel;

        private readonly int buttonWidth = 100;
        #endregion

        protected override void ExecuteOnEnable()
        {
            switchStateTree = Resources.Load<VisualTreeAsset>("Switches/SwitchStateLayout");
            base.ExecuteOnEnable();
        }

        protected override void Initialize()
        {
            base.Initialize();

            statesContainer = root.Q<VisualElement>(name: "states-container");
            noSaveStatesAvailable = root.Q<Label>(name: "no-states-available");

            //CreateSwitchStateLayout(statesContainer, null);
            ListSwitchStateLayout();
        }

        public void ListSwitchStateLayout()
        {
            statesContainer.Clear();
            switchStates = serializedObject.FindProperty("SwitchStates");
            int switchStatesArraySize = switchStates.arraySize;
            if (switchStatesArraySize > 0)
            {
                UIUtility.SetDisplay(noSaveStatesAvailable, false);
                for (int i = 0; i < switchStatesArraySize; i++)
                {
                    SwitchState switchState = component.SwitchStates[i];
                    createSwitchStateLayout(statesContainer, switchState);
                }
            }
        }

        protected void createSwitchStateLayout(VisualElement _container, SwitchState _switchState)
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
            FloatField lowerWeight = switchStateRoot.Q<FloatField>(name: "lower-weight");
            FloatField upperWeight = switchStateRoot.Q<FloatField>(name: "upper-weight");
            FloatField position = switchStateRoot.Q<FloatField>(name: "position");
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
            UIUtility.InitializeField
            (
                selectedStateControlPanel,
                () => component.SelectedState != null && component.SelectedState.Name != "" && Application.isPlaying ? component.SelectedState.Name.ToString() : "N/A",
                100
            );

            idControlPanel = new TextField("ID");
            UIUtility.ToggleNoBoxAndReadOnly(idControlPanel, true);
            UIUtility.InitializeField
            (
                idControlPanel,
                () => component.SelectedState != null && Application.isPlaying ? component.SelectedState.Id.ToString() : "N/A",
                100
            );

            _container.Add(selectedStateControlPanel);
            _container.Add(idControlPanel);
        }
    }
}
