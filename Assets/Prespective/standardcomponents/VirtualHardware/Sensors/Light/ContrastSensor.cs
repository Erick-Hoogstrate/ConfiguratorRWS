using System.Reflection;
using UnityEngine;

namespace u040.prespective.standardcomponents.virtualhardware.sensors.light
{
    /// <summary>
    /// Represents a generic conveyor belt moving id single direction
    /// 
    /// <para>Copyright (c) 2015-2023 Prespective, Unit040 Beheer B.V. All Rights Reserved. See License.txt in the project Prespective folder for license information.</para>
    /// </summary>
    public class ContrastSensor : ColorDetector
    {
        #region<properties>
        [SerializeField]private float matchFactorBase = 0f;
        /// <summary>
        /// The factor of similarities between the Base Color and the detected color
        /// </summary>
        public float MatchFactorBase
        {
            get
            {
                return matchFactorBase;
            }
            private set
            {
                matchFactorBase = value;
            }
        }

        [SerializeField]  private float matchFactorBackground = 0f;
        /// <summary>
        /// The factor of similarities between the Background Color and the detected color
        /// </summary>
        public float MatchFactorBackground
        {
            get
            {
                return matchFactorBackground;
            }
            private set
            {
                matchFactorBackground = value;
            }
        }

        /// <summary>
        /// The Base Color of the Contrast Sensor
        /// </summary>
        public Color BaseColor
        {
            get 
            { 
                return this.ReferenceColor; 
            }
            set 
            { 
                this.ReferenceColor = value; 
            }
        }

        [SerializeField] private Color backgroundColor = Color.black;
        /// <summary>
        /// The Background Color of the Contrast Sensor
        /// </summary>
        public Color BackgroundColor
        {
            get { return this.backgroundColor; }
            set
            {
                if (this.backgroundColor != value)
                {
                    this.backgroundColor = value;
                }
            }
        }
        #endregion

        #region<compare>
        protected override void compareColor(Color _color)
        {
            float differenceR, differenceG, differenceB;
            float contributionR, contributionG, contributionB;

            //Calculate Base match
            //Calculate the differences between the R, G, and B values
            differenceR = Mathf.Abs(BaseColor.r - DetectedColor.r);
            differenceG = Mathf.Abs(BaseColor.g - DetectedColor.g);
            differenceB = Mathf.Abs(BaseColor.b - DetectedColor.b);

            //Each value has a potential of contributing a third of the maximum match factor
            contributionR = 1f / 3f * (1f - differenceR);
            contributionG = 1f / 3f * (1f - differenceG);
            contributionB = 1f / 3f * (1f - differenceB);

            //Add all contributions together for the resulting match factor base
            MatchFactorBase = contributionR + contributionG + contributionB;

            //Calculate Background match
            //Calculate the differences between the R, G, and B values
            differenceR = Mathf.Abs(BackgroundColor.r - DetectedColor.r);
            differenceG = Mathf.Abs(BackgroundColor.g - DetectedColor.g);
            differenceB = Mathf.Abs(BackgroundColor.b - DetectedColor.b);

            //Each value has a potential of contributing a third of the maximum match factor
            contributionR = 1f / 3f * (1f - differenceR);
            contributionG = 1f / 3f * (1f - differenceG);
            contributionB = 1f / 3f * (1f - differenceB);

            //Add all contributions together for the resulting match factor background
            MatchFactorBackground = contributionR + contributionG + contributionB;

            //Flagged depends on which color has the most match
            flagged = MatchFactorBase >= MatchFactorBackground;
        }
        #endregion
    }
}
