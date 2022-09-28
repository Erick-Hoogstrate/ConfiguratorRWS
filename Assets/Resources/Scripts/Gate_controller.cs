using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_controller : MonoBehaviour
{
    public GameObject GateOpen;
    public GameObject GateClose;

    // Update is called once per frame
    void Update()
    {
        if (GateOpen.GetComponent<GateState>().Boolean == true)
        {
            
        }
        if (GateClose.GetComponent<GateState>().Boolean == true)
        {
            
        }
    }
}