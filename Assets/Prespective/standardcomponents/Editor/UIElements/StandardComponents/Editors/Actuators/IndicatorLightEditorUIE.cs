using System.Collections.Generic;
using System.Reflection;
using u040.prespective.core.editor;
using u040.prespective.prepair.kinematics;
using u040.prespective.standardcomponents.editor;
using u040.prespective.utility.editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace u040.prespective.standardcomponents.userinterface.lights.editor
{
    [CustomEditor(typeof(IndicatorLight))]
    public class IndicatorLightEditorUIE : StandardComponentEditorUIE<IndicatorLight>
    {
        #region << Live Data Fields >>
        TextField state;
        TextField intensity;
        #endregion
        #region << Property Fields>>
        ColorField lightColor;
        ColorField baseColor;
        VisualElement materialsContainer;
        List<Button> materialButtons = new List<Button>();
        #endregion

        private Material[] sharedMaterials
        {
            get
            {
                return component.Renderer.sharedMaterials;
            }
        }

        protected override void ExecuteOnEnable()
        {
            visualTree = Resources.Load<VisualTreeAsset>("Actuators/IndicatorLightLayout");
            base.ExecuteOnEnable();
        }

        protected override void UpdateLiveData()
        {
            state.value = component.IsActive ? "Active" : "Inactive";
            state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;

            intensity.value = component.Intensity.ToString();
        }

        protected override void Initialize()
        {
            base.Initialize();

            #region << Live Data >>
            state = root.Q<TextField>(name: "state");
            state.isReadOnly = true;

            intensity = root.Q<TextField>(name: "intensity");
            intensity.isReadOnly = true;

            #endregion
            #region << Properties >>
            lightColor = root.Q<ColorField>(name: "light-color");
            baseColor = root.Q<ColorField>(name: "base-color");
            materialsContainer = root.Q<VisualElement>(name: "material-container");

            UIUtility.InitializeField
            (
                lightColor,
                () => component.LightColor,
                e =>
                {
                    component.LightColor = e.newValue;
                }
            );

            UIUtility.InitializeField
            (
                baseColor,
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

            Button activate = new Button();
            activate.text = component.IsActive ? "Disable" : "Activate";
            activate.style.width = 120;
            activate.RegisterCallback<MouseUpEvent>(mouseEvent => 
            {
                component.SetActive(!component.IsActive);
                state.value = component.IsActive ? "Active" : "Inactive";
                state.Q<VisualElement>(name: "unity-text-input").style.color = component.IsActive ? new Color(0f, 0.5f, 0f) : Color.red;
                activate.text = component.IsActive ? "Disable" : "Activate";
            });
            stateContainer.Add(activate);

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
                () => component.Intensity,
                e => component.Intensity = e.newValue
            );

            _container.Add(stateContainer);
            _container.Add(sliderContainer);
        }
    }
}
