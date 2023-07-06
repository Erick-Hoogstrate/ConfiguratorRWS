using u040.prespective.core.events;
using u040.prespective.prepair.virtualhardware.sensors;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.sensors.light
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class ColorSensor : QualitativeSensor<Color>, ISensor
    {
        #region<properties>
        [SerializeField] private RenderTexture renderTexture;
        [SerializeField]  private Texture2D texture;

        /// <summary>
        /// The Camera component used to "see" colors in the Scene
        /// </summary>
        public Camera SensorCamera;

        [SerializeField] private bool fixedRendering = false;
        /// <summary>
        /// Synchronize the render speed of the SensorCamera to the FixedUpdate. This ensures not missing passing objects but requires more resources.
        /// </summary>
        public bool FixedRendering
        {
            get { return this.fixedRendering; }
            set
            {
                if (this.fixedRendering != value)
                {
                    this.fixedRendering = value;
                }
            }
        }

        /// <summary>
        /// Default color when nothing is detected
        /// </summary>
        public Color VoidColor = Color.black;

        [SerializeField] private Vector2 range = new Vector2(0f, 1f);
        /// <summary>
        /// The range in between the Color Sensor is able to see
        /// </summary>
        public Vector2 Range
        {
            get
            {
                return range;
            }
            set
            {
                if (range != value)
                {
                    float newXValue = Mathf.Max(0f, value.x);
                    float newYValue = Mathf.Max(newXValue, value.y);

                    range = new Vector2(newXValue, newYValue);
                }
            }
        }
        #endregion

        #region<unity functions>
        /// <summary>
        /// Unity awake
        /// </summary>
        public void Awake()
        {
            //Render texture
            renderTexture = new RenderTexture(1, 1, 16, RenderTextureFormat.ARGB32);
            renderTexture.name = "Generated Render Texture";
            renderTexture.Create();

            //Texture2D
            texture = new Texture2D(1, 1);

            //Create Camera object
            GameObject cameraObject = new GameObject("SENSOR_CAMERA_OBJECT");
            cameraObject.transform.SetParent(this.transform);
            cameraObject.transform.localPosition = Vector3.zero;
            cameraObject.transform.localRotation = Quaternion.identity;

            //Setup Camera
            SensorCamera = cameraObject.AddComponent<Camera>();
            SensorCamera.clearFlags = CameraClearFlags.SolidColor;
            SensorCamera.backgroundColor = VoidColor;
            SensorCamera.orthographic = true;
            SensorCamera.orthographicSize = 0.0001f;
            SensorCamera.targetTexture = renderTexture;
            SensorCamera.nearClipPlane = Range.x;
            SensorCamera.farClipPlane = Range.y;
            if (FixedRendering) { SensorCamera.enabled = false; }

            //Create local event link
            ALocalEventLink link = ALocalEventLink.Create(cameraObject, this);
            link.PostRender = postRender;
        }

        /// <summary>
        /// Unity fixed update
        /// </summary>
        public void FixedUpdate()
        {
            if (SensorCamera != null && FixedRendering)
            {
                //Manually render camera on fixed update to prevent it missing detections
                SensorCamera.Render();
            }
        }

        private void postRender(Camera _camera)
        {
            texture.ReadPixels(new Rect(0, 0, 1, 1), 0, 0, true);
            this.value = texture.GetPixel(0, 0);
        }
        #endregion
    }
}
