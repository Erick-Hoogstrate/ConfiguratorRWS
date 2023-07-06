using UnityEngine;
using u040.prespective.utility.modelmanagement;
using System.Reflection;
using u040.prespective.prepair.virtualhardware.sensors;

namespace u040.prespective.standardcomponents.virtualhardware.sensors.light
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class ColorDetector : QuantitativeSensor
    {
        #region<properties>
        /// <summary>
        /// The Color Sensor this Color Detector uses to see colors.
        /// </summary>
        public ColorSensor ColorSensor;

        [SerializeField] private Color referenceColor = Color.red;
        /// <summary>
        /// The color which the ColorDetector is referencing colors to
        /// </summary>
        public Color ReferenceColor
        {
            get { return this.referenceColor; }
            set
            {
                if (this.referenceColor != value)
                {
                    this.referenceColor = value;

                    if (Application.isPlaying)
                    {
                        compareColor(ColorSensor.OutputSignal);
                    }
                }
            }
        }

        /// <summary>
        /// The Color value of the ColorSensor
        /// </summary>
        public Color DetectedColor
        {
            get
            {
                if (ColorSensor)
                {
                    return ColorSensor.OutputSignal;
                }
                else
                {
                    return Color.black;
                }
            }
        }

        [SerializeField]  private float threshold = 0.75f;
        /// <summary>
        /// The minimum MatchFactor to detect a match
        /// </summary>
        public float Threshold
        {
            get 
            { 
                return this.threshold; 
            }
            set
            {
                if (this.threshold != value)
                {
                    this.threshold = Mathf.Clamp(value, 0f, 1f);
                }
            }
        }

        [SerializeField] private float matchFactor = 0f;
        /// <summary>
        /// The factor of similarities between the reference color and the detected color
        /// </summary>
        public float MatchFactor
        {
            get 
            { 
                return this.matchFactor; 
            }
            set
            {
                if (this.matchFactor != value)
                {
                    this.matchFactor = Mathf.Clamp(value, 0f, 1f);
                    this.flagged = this.matchFactor >= Threshold;
                }
            }
        }
        #endregion

        #region<unity functions>
        /// <summary>
        /// Unity reset
        /// </summary>
        public void Reset()
        {
            this.onReset();
        }

        /// <summary>
        /// Function run on Unity Reset
        /// </summary>
        protected virtual void onReset()
        {
            this.ColorSensor = this.RequireComponent<ColorSensor>(true);
        }

        /// <summary>
        /// Unity awake
        /// </summary>
        public void Awake()
        {
            this.onAwake();
        }

        /// <summary>
        /// Function run on Unity Awake
        /// </summary>
        protected virtual void onAwake()
        {
            //If a color sensor has been assigned
            if (ColorSensor)
            {
                //Add a listener to its OnValueChanged event
                ColorSensor.OnValueChanged.AddListener(() =>
                {
                    compareColor(ColorSensor.OutputSignal);
                });
            }

            //If no color sensor has been assigned
            else
            {
                Debug.LogWarning("No Color sensor has been assigned.");
            }
        }

        /// <summary>
        /// Function run on Unity Start
        /// </summary>
        protected override void onStart()
        {
            base.onStart();

            //If a color sensor has been assigned
            if (ColorSensor)
            {
                //Update the ColorDetector
                compareColor(ColorSensor.OutputSignal);
            }
        }
        #endregion

        #region<compare>
        /// <summary>
        /// Compare a color with the reference color for similarities
        /// </summary>
        /// <param name="_color"></param>
        protected virtual void compareColor(Color _color)
        {
            //Calculate the differences between the R, G, and B values
            float differenceR = Mathf.Abs(ReferenceColor.r - DetectedColor.r);
            float differenceG = Mathf.Abs(ReferenceColor.g - DetectedColor.g);
            float differenceB = Mathf.Abs(ReferenceColor.b - DetectedColor.b);

            //Each value has a potential of contributing a third of the maximum match factor
            float contributionR = 1f / 3f * (1f - differenceR);
            float contributionG = 1f / 3f * (1f - differenceG);
            float contributionB = 1f / 3f * (1f - differenceB);

            //Add all contributions together for the resulting match factor
            MatchFactor = contributionR + contributionG + contributionB;
        }
        #endregion
    }
}
