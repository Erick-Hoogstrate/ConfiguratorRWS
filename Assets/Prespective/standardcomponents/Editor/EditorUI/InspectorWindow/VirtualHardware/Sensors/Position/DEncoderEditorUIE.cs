using u040.prespective.prepair.virtualhardware.sensors.position;
using u040.prespective.utility.editor.editorui;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.sensors.position
{
    public abstract class DEncoderEditorUIE<T> : StandardComponentEditorUIE<T> where T : DBaseEncoder
    {
        #region << Live Data Fields >>
        TextField valueField;
        #endregion
        #region << Encoder Properties >>
        DoubleField cycleValueField;
        DoubleField baseValueField;
        Toggle enableRoundingToggle;
        DoubleField roundingIntervalField;
        Toggle capValueToggle;
        DoubleField minimumValueField;
        DoubleField maximumValueField;
        #endregion
        #region << Control Panel Fields >>
        TextField valueControlPanel;
        #endregion
        protected override void updateLiveData()
        {
            valueField.value = Application.isPlaying ? component.OutputSignal.ToString() : "N/A";
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Live Data >>
            valueField = root.Q<TextField>(name: "value");
            #endregion
            #region << Encoder Properties >>
            cycleValueField = root.Q<DoubleField>(name: "cycle-value");
            baseValueField = root.Q<DoubleField>(name: "base-value");
            enableRoundingToggle = root.Q<Toggle>(name: "enable-rounding");
            roundingIntervalField = root.Q<DoubleField>(name: "rounding-interval");
            capValueToggle = root.Q<Toggle>(name: "cap-value");
            minimumValueField = root.Q<DoubleField>(name: "minimum-value");
            maximumValueField = root.Q<DoubleField>(name: "maximum-value");

            UIUtility.InitializeField
            (
                cycleValueField,
                component,
                () => component.ValuePerWholeCycle,
                e =>
                {
                    component.ValuePerWholeCycle = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                baseValueField,
                component,
                () => component.BaseValue,
                e =>
                {
                    component.BaseValue = e.newValue;
                    minimumValueField.SetValueWithoutNotify(component.MinCapValue);
                    maximumValueField.SetValueWithoutNotify(component.MaxCapValue);
                }
            );

            UIUtility.InitializeField
            (
                enableRoundingToggle,
                component,
                () => component.EnableRounding,
                e =>
                {
                    component.EnableRounding = e.newValue;
                    UIUtility.SetDisplay(roundingIntervalField, e.newValue);
                }
            );

            UIUtility.InitializeField
            (
                roundingIntervalField,
                component,
                () => component.RoundingInterval,
                e =>
                {
                    component.RoundingInterval = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                capValueToggle,
                component,
                () => component.CapValue,
                e =>
                {
                    component.CapValue = e.newValue;
                    UIUtility.SetDisplay(minimumValueField, e.newValue);
                    UIUtility.SetDisplay(maximumValueField, e.newValue);

                }
            );

            UIUtility.InitializeField
            (
                minimumValueField,
                component,
                () => component.MinCapValue,
                e =>
                {
                    component.MinCapValue = e.newValue;
                    baseValueField.SetValueWithoutNotify(component.BaseValue);
                    maximumValueField.SetValueWithoutNotify(component.MaxCapValue);
                }
            );

            UIUtility.InitializeField
            (
                maximumValueField,
                component,
                () => component.MaxCapValue,
                e =>
                {
                    component.MaxCapValue = e.newValue;
                    minimumValueField.SetValueWithoutNotify(component.MinCapValue);
                    baseValueField.SetValueWithoutNotify(component.BaseValue);
                }
            );
            
            UIUtility.SetDisplay(roundingIntervalField, component.EnableRounding);
            UIUtility.SetDisplay(minimumValueField, component.CapValue);
            UIUtility.SetDisplay(maximumValueField, component.CapValue);
            #endregion
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            valueControlPanel = new TextField("Value");
            UIUtility.SetReadOnlyState(valueControlPanel, true);

            scheduleControlPanelUpdate(valueControlPanel);

            _container.Add(valueControlPanel);
        }

        protected override void updateControlPanelData()
        {
            valueControlPanel.value = Application.isPlaying ? component.OutputSignal.ToString() : "N/A";
        }
    }
}
