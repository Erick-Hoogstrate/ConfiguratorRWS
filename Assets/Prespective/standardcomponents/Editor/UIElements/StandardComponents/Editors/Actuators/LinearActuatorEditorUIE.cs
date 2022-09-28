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
#pragma warning disable 0618

    [CustomEditor(typeof(LinearActuator))]
    public class LinearActuatorEditorUIE : StandardComponentEditorUIE<LinearActuator>
    {
        #region << Live Data Fields >>
        TextField targetField;
        TextField position;
        #endregion
        #region << Property Fields>>
        IMGUIContainer warningContainer;

        ObjectField prismaticJoint;
        Toggle invertPosition;
        FloatField extendSpeed;
        FloatField extendTime;
        FloatField retractSpeed;
        FloatField retractTime;
        #endregion
        #region << Control Panel Fields >>
        FloatField positionControlPanel;
        #endregion
        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Actuators/Motors/LinearActuatorLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            targetField.value = component.Target.ToString();
            position.value = component.Position.ToString();
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
            extendSpeed = root.Q<FloatField>(name: "extending-speed");
            extendTime = root.Q<FloatField>(name: "extending-time");
            retractSpeed = root.Q<FloatField>(name: "retracting-speed");
            retractTime = root.Q<FloatField>(name: "retracting-time");

            warningContainer.onGUIHandler = OnInspectorGUI;

            UIUtility.InitializeField
            (
                prismaticJoint,
                () => component.PrismaticJoint,
                e =>
                {
                    component.PrismaticJoint = (AFPrismaticJoint)e.newValue;
                },
                typeof(AFPrismaticJoint)
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

            UIUtility.InitializeField
            (
                extendSpeed,
                () => component.ExtendingMoveSpeed,
                e =>
                {
                    component.ExtendingMoveSpeed = e.newValue;
                    extendTime.SetValueWithoutNotify(component.ExtendingCycleTime);
                }
            );

            UIUtility.InitializeField
            (
                extendTime,
                () => component.ExtendingCycleTime,
                e =>
                {
                    component.ExtendingCycleTime = e.newValue;
                    extendSpeed.SetValueWithoutNotify(component.ExtendingMoveSpeed);
                }
            );

            UIUtility.InitializeField
            (
                retractSpeed,
                () => component.RetractingMoveSpeed,
                e =>
                {
                    component.RetractingMoveSpeed = e.newValue;
                    retractTime.SetValueWithoutNotify(component.RetractingCycleTime);
                }
            );

            UIUtility.InitializeField
            (
                retractTime,
                () => component.RetractingCycleTime,
                e =>
                {
                    component.RetractingCycleTime = e.newValue;
                    retractSpeed.SetValueWithoutNotify(component.RetractingMoveSpeed);
                }
            );

            #endregion
        }

        public override void OnInspectorGUI()
        {
            if(component.PrismaticJoint == null) 
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
                () => component.Target,
                e => component.Target = e.newValue
            );
            #endregion

            positionControlPanel = new FloatField("Position (%)");
            UIUtility.ToggleNoBoxAndReadOnly(positionControlPanel, true);

            ScheduleControlPanelUpdate(positionControlPanel);

            _container.Add(sliderContainer);
            _container.Add(positionControlPanel);
        }

        protected virtual void updateControlPanelData() 
        {
            positionControlPanel.value = component.Position;
        }
#pragma warning restore 0618
    }
}
