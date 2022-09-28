using u040.prespective.prepair.ui.buttons;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders.editor
{
    public abstract class DEncoderEditorUIE<T> : StandardComponentEditorUIE<T> where T : DBaseEncoder
    {
        #region << Live Data Fields >>
        TextField value;
        #endregion
        #region << Encoder Properties >>
        DoubleField cycleValue;
        DoubleField baseValue;
        Toggle enableRounding;
        DoubleField roundingInterval;
        Toggle capValue;
        DoubleField minimumValue;
        DoubleField maximumValue;
        PropertyField onValueChangedField;
        #endregion
        #region << Control Panel Fields >>
        TextField valueControlPanel;
        #endregion
        protected override void UpdateLiveData()
        {
            value.value = Application.isPlaying ? component.OutputSignal.ToString() : "N/A";
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Live Data >>
            value = root.Q<TextField>(name: "value");
            #endregion
            #region << Encoder Properties >>
            cycleValue = root.Q<DoubleField>(name: "cycle-value");
            baseValue = root.Q<DoubleField>(name: "base-value");
            enableRounding = root.Q<Toggle>(name: "enable-rounding");
            roundingInterval = root.Q<DoubleField>(name: "rounding-interval");
            capValue = root.Q<Toggle>(name: "cap-value");
            minimumValue = root.Q<DoubleField>(name: "minimum-value");
            maximumValue = root.Q<DoubleField>(name: "maximum-value");
            onValueChangedField = root.Q<PropertyField>(name: "on-value-changed");

            UIUtility.InitializeField
            (
                cycleValue,
                () => component.ValuePerWholeCycle,
                e =>
                {
                    component.ValuePerWholeCycle = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                baseValue,
                () => component.BaseValue,
                e =>
                {
                    component.BaseValue = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                enableRounding,
                () => component.EnableRounding,
                e =>
                {
                    component.EnableRounding = e.newValue;
                    UIUtility.SetDisplay(roundingInterval, e.newValue);
                }
            );

            UIUtility.InitializeField
            (
                roundingInterval,
                () => component.RoundingInterval,
                e =>
                {
                    component.RoundingInterval = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                capValue,
                () => component.CapValue,
                e =>
                {
                    component.CapValue = e.newValue;
                    UIUtility.SetDisplay(minimumValue, e.newValue);
                    UIUtility.SetDisplay(maximumValue, e.newValue);

                }
            );

            UIUtility.InitializeField
            (
                minimumValue,
                () => component.MinCapValue,
                e =>
                {
                    component.MinCapValue = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                maximumValue,
                () => component.MaxCapValue,
                e =>
                {
                    component.MaxCapValue = e.newValue;
                }
            );

            onValueChangedField.bindingPath = "onValueChanged";

            UIUtility.SetDisplay(roundingInterval, component.EnableRounding);
            UIUtility.SetDisplay(minimumValue, component.CapValue);
            UIUtility.SetDisplay(maximumValue, component.CapValue);
            #endregion
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            valueControlPanel = new TextField("Value");
            UIUtility.ToggleNoBoxAndReadOnly(valueControlPanel, true);

            ScheduleControlPanelUpdate(valueControlPanel);

            _container.Add(valueControlPanel);
        }

        protected override void UpdateControlPanelData()
        {
            valueControlPanel.value = Application.isPlaying ? component.OutputSignal.ToString() : "N/A";
        }
    }
}
