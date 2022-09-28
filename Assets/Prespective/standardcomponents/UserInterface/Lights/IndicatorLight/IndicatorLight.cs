using System.Reflection;
using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface.lights
{
    [ExecuteInEditMode]
    public class IndicatorLight : MonoBehaviour, IActuator
    {
#pragma warning disable 0414
        [SerializeField] [Obfuscation] private int toolbarTab;
#pragma warning restore 0414

        [SerializeField] private int originalMaterialIndex = -1;

        public int OriginalMaterialIndex
        {
            get { return this.originalMaterialIndex; }
            private set
            {
                this.originalMaterialIndex = value;
            }
        }
        [SerializeField] private Material originalMaterial;
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

        [SerializeField] private Material currentMaterial;

        [SerializeField] private Color lightColor = Color.yellow;
        public Color LightColor
        {
            get { return lightColor; }
            set
            {
                if (this.lightColor != value * Intensity)
                {
                    lightColor = value;
                    if (currentMaterial != null)
                    {
                        currentMaterial.SetColor("_EmissionColor", value * Intensity);
                    }
                }
            }
        }

        [SerializeField] private Color baseColor = Color.black;
        public Color BaseColor
        {
            get { return baseColor; }
            set
            {
                if(this.baseColor != value)
                {
                    baseColor = value;
                    if (currentMaterial != null)
                    {
                        currentMaterial.SetColor("_Color", value);
                    }
                }
            }
        }

        [SerializeField] [Range(0f, 1f)] private float intensity = 0.75f;
        public float Intensity
        {
            get { return intensity; }
            set
            {
                intensity = value;
                LightColor = LightColor;
            }
        }

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
        }

        private const string MATERIAL_NAME_ADDITIVE = " (IndicatorLight)";

        private Renderer storedRenderer = null;
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


        internal void OnDestroy()
        {
            LoadOriginalMaterial();
        }

        internal void OnEnable()
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

        #region <<Original Material Handling>>
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
                currentMaterial.name += MATERIAL_NAME_ADDITIVE;
                BaseColor = currentMaterial.color;
                currentMaterial.SetColor("_EmissionColor", lightColor * Intensity);

                sharedMats[OriginalMaterialIndex] = currentMaterial;
                Renderer.sharedMaterials = sharedMats;

            }
            else { Debug.LogWarning("Cannot save Material because no Renderer was detected"); }
        }

        /// <summary>
        /// A method to reset the material back to its original
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
        #endregion

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

        private bool detectedRequiredComponents()
        {
            return Renderer != null && GetComponents<MeshFilter>().Length > 0 && GetComponents<IndicatorLight>().Length <= 1;
        }
    }
}
