using System.Collections.Generic;
using u040.prespective.standardcomponents.virtualhardware.actuators.lights;
using u040.prespective.utility.editor.editorui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.editor.editorui.inspectorwindow.virtualhardware.actuators.lights
{
    [CustomEditor(typeof(IndicatorLight))]
    public class IndicatorLightEditorUIE : StandardComponentEditorUIE<IndicatorLight>
    {
        #region << FIELDS >>
        //Live Data Fields
        private TextField state;
        private TextField intensity;

        //Property Fields
        private ColorField lightColorField;
        private ColorField baseColorField;
        private VisualElement materialsContainer;
        private List<Button> materialButtons = new List<Button>();
        #endregion

        #region << PROPERTIES >>
        protected override string visualTreeFile
        {
            get
            {
                return "IndicatorLightEditorLayout";
            }
        }

        private Material[] sharedMaterials
        {
            get
            {
                return component.Renderer.sharedMaterials;
            }
        }
        #endregion

        protected override void executeOnEnable()
        {
            base.executeOnEnable();
        }

        protected override void updateLiveData()
        {
            state.value = component.IsActive ? "Active" : "Inactive";
            state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;

            intensity.value = component.Intensity.ToString();
        }

        protected override void initialize()
        {
            base.initialize();

            #region << Live Data >>
            state = root.Q<TextField>(name: "state");
            state.isReadOnly = true;

            intensity = root.Q<TextField>(name: "intensity");
            intensity.isReadOnly = true;

            #endregion
            #region << Properties >>
            lightColorField = root.Q<ColorField>(name: "light-color");
            baseColorField = root.Q<ColorField>(name: "base-color");
            materialsContainer = root.Q<VisualElement>(name: "material-container");

            UIUtility.InitializeField
            (
                lightColorField,
                component,
                () => component.LightColor,
                e =>
                {
                    component.LightColor = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                baseColorField,
                component,
                () => component.BaseColor,
                e =>
                {
                    component.BaseColor = e.newValue;
                }
            );

            if (sharedMaterials.Length > 0)
            {
                for (int i = 0; i < sharedMaterials.Length; i++)
                {
                    int j = i;

                    VisualElement materialContainer = new VisualElement();
                    ObjectField material = UIUtility.CreateObjectLocatorField(sharedMaterials[i]);
                    Button selectMaterial = new Button();
                    Button deSelectMaterial = new Button();

                    materialButtons.Add(selectMaterial);
                    materialButtons.Add(deSelectMaterial);

                    materialContainer.AddToClassList("row");
                    material.style.flexGrow = 1;
                    material.style.flexShrink = 1;
                    selectMaterial.style.width = 120;
                    deSelectMaterial.style.width = 120;

                    selectMaterial.text = "Select Material";
                    deSelectMaterial.text = "Deselect Material";

                    materialContainer.Add(material);
                    materialContainer.Add(selectMaterial);
                    materialContainer.Add(deSelectMaterial);
                    materialsContainer.Add(materialContainer);

                    selectMaterial.RegisterCallback<MouseUpEvent>(mouseEvent =>
                    {
                        component.OriginalMaterial = sharedMaterials[j];
                        UpdateSelectAndDeselectButtonsVisibility();
                    });

                    deSelectMaterial.RegisterCallback<MouseUpEvent>(mouseEvent =>
                    {
                        component.LoadOriginalMaterial();
                        UpdateSelectAndDeselectButtonsVisibility();
                    });

                    if (sharedMaterials.Length - 1 == i)
                    {
                        UpdateSelectAndDeselectButtonsVisibility();
                    }
                }
            }
            #endregion
        }

        public void UpdateSelectAndDeselectButtonsVisibility()
        {
            for (int i = 0; i < sharedMaterials.Length; i++)
            {
                bool isSelected = component.OriginalMaterialIndex == i;

                Button selectMaterial = materialButtons[i * 2];
                Button deSelectMaterial = materialButtons[i * 2 + 1];

                UIUtility.SetDisplay(selectMaterial, component.OriginalMaterialIndex != i);
                UIUtility.SetDisplay(deSelectMaterial, component.OriginalMaterialIndex == i);
            }
        }

        public override void ShowControlPanelProperties(VisualElement _container)
        {
            VisualElement stateContainer = new VisualElement();
            stateContainer.AddToClassList("row");

            TextField state = new TextField("State");
            state.AddToClassList("no-box");
            state.AddToClassList("flex-grow");
            state.isReadOnly = true;
            state.value = component.IsActive ? "Active" : "Inactive";
            state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
            stateContainer.Add(state);

            Button enable = new Button();
            enable.text = component.IsActive ? "Disable" : "Enable";
            enable.style.width = 120;
            enable.RegisterCallback<MouseUpEvent>(_mouseEvent => 
            {
                component.SetActive(!component.IsActive);
                state.value = component.IsActive ? "Active" : "Inactive";
                state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
                enable.text = component.IsActive ? "Disable" : "Enable";
            });
            stateContainer.Add(enable);

            VisualElement sliderContainer = new VisualElement();
            sliderContainer.AddToClassList("row");

            Slider intensitySlider = new Slider("Intensity", 0, 1);
            intensitySlider.AddToClassList("flex-grow");

            FloatField intensityField = new FloatField();
            intensityField.style.width = 60;

            sliderContainer.Add(intensitySlider);
            sliderContainer.Add(intensityField);


            UIUtility.BindSliderAndField
            (
                intensitySlider,
                intensityField,
                component,
                () => component.Intensity,
                e => component.Intensity = e.newValue
            );

            _container.Add(stateContainer);
            _container.Add(sliderContainer);
        }
    }
}
