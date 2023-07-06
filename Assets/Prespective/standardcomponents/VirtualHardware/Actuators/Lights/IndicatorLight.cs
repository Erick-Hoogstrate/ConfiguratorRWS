using System.Reflection;
using u040.prespective.prepair.virtualhardware.actuators;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.actuators.lights
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    [ExecuteInEditMode]
    public class IndicatorLight : MonoBehaviour, IActuator
    {
        #region << Constants & ReadOnly >>
        private const string MATERIAL_NAME_SUFFIX = " (IndicatorLight)";
        #endregion

        #region << Fields & Properties >>
        [SerializeField] [Obfuscation] private Material currentMaterial;

        [SerializeField] [Obfuscation] private int originalMaterialIndex = -1;

        /// <summary>
        /// The index of the Original Material in the MeshRenderer
        /// </summary>
        public int OriginalMaterialIndex
        {
            get { return this.originalMaterialIndex; }
            private set
            {
                this.originalMaterialIndex = value;
            }
        }

        [SerializeField] [Obfuscation] private Material originalMaterial;
        /// <summary>
        /// The Original Material that was replaced by the copy for the Indicator Light
        /// </summary>
        public Material OriginalMaterial
        {
            get { return this.originalMaterial; }
            set
            {
                if (value != this.originalMaterial && value != currentMaterial)
                {
                    saveOriginalMaterial(value);
                }
            }
        }

        [SerializeField] [Obfuscation] private Color lightColor = Color.yellow;
        /// <summary>
        /// The color used for the Emission of the Indicator Light
        /// </summary>
        public Color LightColor
        {
            get { return lightColor; }
            set
            {
                if (this.lightColor == value)
                {
                    return;
                }

                lightColor = value;
                applyMaterialLighting();
            }
        }

        [SerializeField] [Obfuscation] private Color baseColor = Color.black;
        /// <summary>
        /// The base color of the material used for the Indicator Light
        /// </summary>
        public Color BaseColor
        {
            get { return baseColor; }
            set
            {
                if (this.baseColor == value)
                {
                    return;
                }

                baseColor = value;
                applyMaterialLighting();
            }
        }

        [SerializeField] [Obfuscation] private float intensity = 0.75f;
        /// <summary>
        /// The intensity for the Emission
        /// </summary>
        public float Intensity
        {
            get { return intensity; }
            set
            {
                if (intensity == value)
                {
                    return;
                }

                intensity = value;
                applyMaterialLighting();
            }
        }

        private Renderer storedRenderer = null;
        /// <summary>
        /// The Renderer used for the Indicator Light
        /// </summary>
        public Renderer Renderer
        {
            get
            {
                if (storedRenderer == null)
                {
                    storedRenderer = GetComponent<Renderer>();
                }
                return storedRenderer;
            }
        }

        /// <summary>
        /// Whether the Indicator Light currently has its Emission active
        /// </summary>
        public bool IsActive
        {
            get
            {
                if (currentMaterial != null)
                {
                    return currentMaterial.IsKeywordEnabled("_EMISSION");
                }
                return false;
            }
            set
            {
                SetActive(value);
            }
        }
        #endregion

        #region << Constructors >>

        #endregion

        #region << Unity & Custom Events >>
        public void OnDestroy()
        {
            LoadOriginalMaterial();
        }

        public void OnEnable()
        {
            if (detectedRequiredComponents())
            {
                if (OriginalMaterial == null)
                {
                    saveOriginalMaterial();
                }
            }
            else
            {
                Debug.LogError("Cannot be attached to this GameObject since it either does not have a MeshRenderer and a Mesh, or another " + typeof(IndicatorLight).Name + " already exists on it.");
                DestroyImmediate(this);
            }
        }
        #endregion

        #region << Overrides & Accessor Functions >>
        /// <summary>
        /// Resets the Original Material back to the Mesh Renderer and removes the copy made by the Indicator Light
        /// </summary>
        public void LoadOriginalMaterial()
        {
            if (detectedRequiredComponents())
            {
                if (OriginalMaterial != null)
                {
                    Material[] currentMats = Renderer.sharedMaterials;
                    currentMats[OriginalMaterialIndex] = OriginalMaterial;
                    Renderer.sharedMaterials = currentMats;

                    this.originalMaterial = null;
                    OriginalMaterialIndex = -1;
                    currentMaterial = null;
                }
            }
        }

        /// <summary>
        /// Set the Emission of the Indicator Light to active/inactive
        /// </summary>
        /// <param name="_state">Active/Inactive</param>
        public void SetActive(bool _state)
        {
            if (currentMaterial != null)
            {
                if (_state != IsActive)
                {
                    //Set active
                    if (_state)
                    {
                        currentMaterial.EnableKeyword("_EMISSION");
                    }

                    //Set inactive
                    else
                    {
                        currentMaterial.DisableKeyword("_EMISSION");
                    }
                }
            }
            else
            {
                Debug.LogWarning("Unable to set active while no material has been selected.");
            }
        }
        #endregion

        #region << Utility Functions >>
        /// <summary>
        /// A method the cache the original material
        /// </summary>
        /// <param name="_material"></param>
        private void saveOriginalMaterial(Material _material = null)
        {
            if (detectedRequiredComponents())
            {
                LoadOriginalMaterial();

                Material[] sharedMats = Renderer.sharedMaterials;

                OriginalMaterialIndex = getSharedMaterialIndex(_material, sharedMats);
                this.originalMaterial = sharedMats[OriginalMaterialIndex];
                currentMaterial = new Material(OriginalMaterial);
                currentMaterial.name += MATERIAL_NAME_SUFFIX;
                BaseColor = currentMaterial.color;
                currentMaterial.SetColor("_EmissionColor", lightColor * Intensity);

                sharedMats[OriginalMaterialIndex] = currentMaterial;
                Renderer.sharedMaterials = sharedMats;

            }
            else { Debug.LogWarning("Cannot save Material because no Renderer was detected"); }
        }

        /// <summary>
        /// Get the index of a material in the SharedMaterials array
        /// </summary>
        /// <param name="_material"></param>
        /// <returns></returns>
        private int getSharedMaterialIndex(Material _material, Material[] _mats)
        {
            for (int i = 0; i < _mats.Length; i++)
            {
                if (_mats[i] == _material)
                {
                    return i;
                }
            }
            return 0; //Not found
        }

        /// <summary>
        /// Apply the lighting settings to the material
        /// </summary>
        private void applyMaterialLighting()
        {
            if (currentMaterial != null)
            {
                currentMaterial.SetColor("_Color", BaseColor);
                currentMaterial.SetColor("_EmissionColor", LightColor * Intensity);
            }
        }

        private bool detectedRequiredComponents()
        {
            return Renderer != null && GetComponents<MeshFilter>().Length > 0 && GetComponents<IndicatorLight>().Length <= 1;
        }
        #endregion
    }
}
