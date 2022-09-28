using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickable_button : MonoBehaviour
{
    public bool button;
    private Camera MainCamera;


    void Update()
    {
        MainCamera = Camera.main;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out Hit))
            {
                if (Hit.transform.name == gameObject.name)
                {
                    button = !button; // toggles onoff at each click


                }
            }
        }
    }
}