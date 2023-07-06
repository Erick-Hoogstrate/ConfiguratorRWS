using u040.prespective.prepair.kinematics.joints.basic;
using u040.prespective.standardcomponents.virtualhardware.actuators.motors;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.actuators.motors
{
    [CustomEditor(typeof(DLinearActuator))]
    public class DLinearActuatorEditorUIE : StandardComponentEditorUIE<DLinearActuator>
    {
        #region << FIELDS >>
        //Live Data Fields
        TextField targetField;
        TextField positionField;

        //Property Fields
        IMGUIContainer warningContainer;
        ObjectField prismaticJointField;
        Toggle invertPositionField;
        DoubleField extendSpeedField;
        DoubleField extendTimeField;
        DoubleField retractSpeedField;
        DoubleField retractTimeField;

        //Control Panel Fields
        DoubleField positionControlPanel;

        #endregion
        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "DLinearActuatorEditorLayout";
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void updateLiveData()
        {
            targetField.value = (component.Target * 100).ToString();
            positionField.value = (component.Position * 100).ToString();
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Live Data >>
            targetField = root.Q<TextField>(name: "target");
            targetField.isReadOnly = true;
            positionField = root.Q<TextField>(name: "position");
            positionField.isReadOnly = true;
            #endregion
            #region << Properties >>
            warningContainer = root.Q<IMGUIContainer>(name: "warning-container");
            prismaticJointField = root.Q<ObjectField>(name: "prismatic-joint-field");
            invertPositionField = root.Q<Toggle>(name: "invert-position");
            extendSpeedField = root.Q<DoubleField>(name: "extending-speed");
            extendTimeField = root.Q<DoubleField>(name: "extending-time");
            retractSpeedField = root.Q<DoubleField>(name: "retracting-speed");
            retractTimeField = root.Q<DoubleField>(name: "retracting-time");
            warningContainer.onGUIHandler = OnInspectorGUI;

            UIUtility.InitializeField
            (
                prismaticJointField,
                component,
                () => component.KinematicPrismaticJoint,
                e =>
                {
                    component.KinematicPrismaticJoint = (APrismaticJoint)e.newValue;
                },
                typeof(APrismaticJoint)
            );

            UIUtility.InitializeField
            (
                invertPositionField,
                component,
                () => component.InvertPosition,
                e =>
                {
                    component.InvertPosition = e.newValue;
                }
            );

            extendSpeedField.isDelayed = true;
            UIUtility.InitializeField
            (
                extendSpeedField,
                component,
                () => component.ExtendingMoveSpeed,
                e =>
                {
                    component.ExtendingMoveSpeed = e.newValue;
                    extendSpeedField.SetValueWithoutNotify(component.ExtendingMoveSpeed);
                    extendTimeField.SetValueWithoutNotify(component.ExtendingCycleTime);
                }
            );

            extendTimeField.isDelayed = true;
            UIUtility.InitializeField
            (
                extendTimeField,
                component,
                () => component.ExtendingCycleTime,
                e =>
                {
                    component.ExtendingCycleTime = e.newValue;
                    extendSpeedField.SetValueWithoutNotify(component.ExtendingMoveSpeed);
                    extendTimeField.SetValueWithoutNotify(component.ExtendingCycleTime);
                }
            );

            retractSpeedField.isDelayed = true;
            UIUtility.InitializeField
            (
                retractSpeedField,
                component,
                () => component.RetractingMoveSpeed,
                e =>
                {
                    component.RetractingMoveSpeed = e.newValue;
                    retractTimeField.SetValueWithoutNotify(component.RetractingCycleTime);
                    retractSpeedField.SetValueWithoutNotify(component.RetractingMoveSpeed);
                }
            );

            retractTimeField.isDelayed = true;
            UIUtility.InitializeField
            (
                retractTimeField,
                component,
                () => component.RetractingCycleTime,
                e =>
                {
                    component.RetractingCycleTime = e.newValue;
                    retractSpeedField.SetValueWithoutNotify(component.RetractingMoveSpeed);
                    retractTimeField.SetValueWithoutNotify(component.RetractingCycleTime);
                }
            );

            EditorApplication.playModeStateChanged += state =>
            {
                prismaticJointField.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                invertPositionField.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                extendSpeedField.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                extendTimeField.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                retractSpeedField.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                retractTimeField.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
            };
            #endregion
        }

        public override void OnInspectorGUI()
        {
            if (component.KinematicPrismaticJoint == null)
            {
                EditorGUILayout.HelpBox("Without a Prismatic Joint this component will not function.", MessageType.Error);
            }
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            #region Slider
            VisualElement sliderContainer = new VisualElement();
            sliderContainer.AddToClassList("row");

            Slider targetSlider = new Slider("Target (%)", 0f, 1f);
            targetSlider.AddToClassList("flex-grow");

            FloatField targetField = new FloatField();
            targetField.style.width = 60;

            sliderContainer.Add(targetSlider);
            sliderContainer.Add(targetField);

            UIUtility.BindSliderAndField
            (
                targetSlider,
                targetField,
                component,
                () => (float)component.Target,
                e => component.Target = e.newValue
            );
            #endregion

            positionControlPanel = new DoubleField("Position (%)");
            UIUtility.SetReadOnlyState(positionControlPanel, true);

            scheduleControlPanelUpdate(positionControlPanel);

            _container.Add(sliderContainer);
            _container.Add(positionControlPanel);
        }

        protected override void updateControlPanelData()
        {
            positionControlPanel.value = component.Position;
        }
    }
}
