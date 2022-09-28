using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle_controller : MonoBehaviour
{
    public GameObject Open;
    public GameObject Close;

    // Update is called once per frame
    void Update()
    {
        if(Open.GetComponent<GateState>().Boolean == true)
        {
            
        }
        if (Close.GetComponent<GateState>().Boolean == true)
        {
            
        }
    }
}
