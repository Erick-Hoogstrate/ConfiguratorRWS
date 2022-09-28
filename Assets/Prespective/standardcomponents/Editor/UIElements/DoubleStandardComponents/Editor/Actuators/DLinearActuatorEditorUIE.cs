using System.Reflection;
using u040.prespective.prepair.kinematics;
using u040.prespective.standardcomponents.editor;
using u040.prespective.standardcomponents.kinetics.motor.linearactuator;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.kinetics.motor.dcmotor.editor
{
    [CustomEditor(typeof(DLinearActuator))]
    public class DLinearActuatorEditorUIE : StandardComponentEditorUIE<DLinearActuator>
    {
        #region << Live Data Fields >>
        TextField targetField;
        TextField position;
        #endregion
        #region << Property Fields>>
        IMGUIContainer warningContainer;
        ObjectField prismaticJoint;
        Toggle invertPosition;
        DoubleField extendSpeed;
        DoubleField extendTime;
        DoubleField retractSpeed;
        DoubleField retractTime;
        #endregion
        #region << Control Panel Fields >>
        DoubleField positionControlPanel;
        #endregion

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Actuators/DLinearActuatorLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            targetField.value = (component.Target * 100).ToString();
            position.value = (component.Position * 100).ToString();
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Live Data >>
            targetField = root.Q<TextField>(name: "target");
            targetField.isReadOnly = true;
            position = root.Q<TextField>(name: "position");
            position.isReadOnly = true;

            #endregion
            #region << Properties >>
            warningContainer = root.Q<IMGUIContainer>(name: "warning-container");
            prismaticJoint = root.Q<ObjectField>(name: "prismatic-joint-field");
            invertPosition = root.Q<Toggle>(name: "invert-position");
            extendSpeed = root.Q<DoubleField>(name: "extending-speed");
            extendTime = root.Q<DoubleField>(name: "extending-time");
            retractSpeed = root.Q<DoubleField>(name: "retracting-speed");
            retractTime = root.Q<DoubleField>(name: "retracting-time");

            warningContainer.onGUIHandler = OnInspectorGUI;

            UIUtility.InitializeField
            (
                prismaticJoint,
                () => component.KinematicPrismaticJoint,
                e =>
                {
                    component.KinematicPrismaticJoint = (APrismaticJoint)e.newValue;
                },
                typeof(APrismaticJoint)
            );

            UIUtility.InitializeField
            (
                invertPosition,
                () => component.InvertPosition,
                e =>
                {
                    component.InvertPosition = e.newValue;
                }
            );

            extendSpeed.isDelayed = true;
            UIUtility.InitializeField
            (
                extendSpeed,
                () => component.ExtendingMoveSpeed,
                e =>
                {
                    component.ExtendingMoveSpeed = e.newValue;
                    extendSpeed.SetValueWithoutNotify(component.ExtendingMoveSpeed);
                    extendTime.SetValueWithoutNotify(component.ExtendingCycleTime);
                }
            );

            extendTime.isDelayed = true;
            UIUtility.InitializeField
            (
                extendTime,
                () => component.ExtendingCycleTime,
                e =>
                {
                    component.ExtendingCycleTime = e.newValue;
                    extendSpeed.SetValueWithoutNotify(component.ExtendingMoveSpeed);
                    extendTime.SetValueWithoutNotify(component.ExtendingCycleTime);
                }
            );

            retractSpeed.isDelayed = true;
            UIUtility.InitializeField
            (
                retractSpeed,
                () => component.RetractingMoveSpeed,
                e =>
                {
                    component.RetractingMoveSpeed = e.newValue;
                    retractTime.SetValueWithoutNotify(component.RetractingCycleTime);
                    retractSpeed.SetValueWithoutNotify(component.RetractingMoveSpeed);
                }
            );

            retractTime.isDelayed = true;
            UIUtility.InitializeField
            (
                retractTime,
                () => component.RetractingCycleTime,
                e =>
                {
                    component.RetractingCycleTime = e.newValue;
                    retractSpeed.SetValueWithoutNotify(component.RetractingMoveSpeed);
                    retractTime.SetValueWithoutNotify(component.RetractingCycleTime);
                }
            );
            #endregion

            EditorApplication.playModeStateChanged += state =>
            {
                prismaticJoint.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                invertPosition.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                extendSpeed.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                extendTime.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                retractSpeed.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
                retractTime.SetEnabled(!(state == PlayModeStateChange.EnteredPlayMode));
            };
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
                () => (float)component.Target,
                e => component.Target = e.newValue
            );
            #endregion

            positionControlPanel = new DoubleField("Position (%)");
            UIUtility.ToggleNoBoxAndReadOnly(positionControlPanel, true);

            ScheduleControlPanelUpdate(positionControlPanel);

            _container.Add(sliderContainer);
            _container.Add(positionControlPanel);
        }

        protected override void UpdateControlPanelData()
        {
            positionControlPanel.value = component.Position;
        }
    }
}
