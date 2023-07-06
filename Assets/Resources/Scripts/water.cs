using System;
using System.Collections.Generic;
using System.Reflection;
using u040.prespective.prelogic;
using u040.prespective.prelogic.component;
using u040.prespective.standardcomponents.virtualhardware.sensors.position;
using UnityEngine;

namespace u040.prespective.standardcomponents.userinterface.buttons.encoders
{
    public class water : MonoBehaviour
    {
        public DLinearEncoder WaalEnc;
        public DLinearEncoder ARKEnc;
        public DLinearEncoder kolk1Enc;
        public DLinearEncoder kolk2Enc;

        public GameObject kolk1;
        public GameObject kolk2;
        public GameObject ARK;
        public GameObject Waal;
        
        public GameObject inner_paddle;
        public GameObject middle_paddle;
        public GameObject outer_paddle;

        public GameObject inner_gate;
        public GameObject middle_gate;
        public GameObject outer_gate;





        [SerializeField] private float speed = 15f;
        private Vector3 kolk1_match_ARK = new Vector3(-34f, -7f, -65.1f);
        private Vector3 kolk2_match_ARK = new Vector3(-34f, -7f, -49f);

        private Vector3 kolk1_match_Waal = new Vector3(-34f, -2.2f, -65.1f);
        private Vector3 kolk2_match_Waal = new Vector3(-34f, -2.2f, -49f);





        // Update is called once per frame
        void Update()
        {

            //if (inner_gate.GetComponent<GateState>().Boolean == true)
            //{
                
            //    if (inner_paddle.GetComponent<GateState>().Boolean == true)
            //    {
                    
            //        if (middle_gate.GetComponent<GateState>().Boolean == false)
            //        {
            //            kolk1.transform.position = Vector3.MoveTowards(kolk1.transform.position, kolk1_match_ARK, Time.deltaTime * speed );
            //            kolk2.transform.position = Vector3.MoveTowards(kolk2.transform.position, kolk2_match_ARK, Time.deltaTime * speed );
            //        }

            //        if (middle_gate.GetComponent<GateState>().Boolean == true)
            //        {
            //            kolk1.transform.position = Vector3.MoveTowards(kolk1.transform.position, kolk1_match_ARK, Time.deltaTime * speed );
            //        }

            //    }
            //}

            //if (middle_gate.GetComponent<GateState>().Boolean == true)
            //{
                
            //    if (middle_paddle.GetComponent<GateState>().Boolean == true)
            //    {
                    

            //        kolk1.transform.position = Vector3.MoveTowards(kolk1.transform.position, kolk1_match_Waal, Time.deltaTime * speed );


            //    }
            //}

            if (outer_gate.GetComponent<GateState>().Boolean == true)
                Debug.Log("hefdeur dicht");
            {
                if (outer_paddle.GetComponent<GateState>().Boolean == true)
                {
                    Debug.Log("Schuif open");
                    

                        kolk1.transform.position = Vector3.MoveTowards(kolk1.transform.position, kolk1_match_Waal, Time.deltaTime * speed );
                        kolk2.transform.position = Vector3.MoveTowards(kolk2.transform.position, kolk2_match_Waal, Time.deltaTime * speed );
                        
                    

                    //if (middle_gate.GetComponent<GateState>().Boolean == true)
                    //{
                    //    kolk1.transform.position = Vector3.MoveTowards(kolk1.transform.position, kolk1_match_Waal, Time.deltaTime * speed);
                        
                    //}
                }

            }
        }
    }
}
