using UnityEngine;
using u040.prespective.standardcomponents.kinetics.motor.linearactuator;

public class GateState : MonoBehaviour
{

    [Tooltip("Boolean Value of the toggle")]
    public DLinearActuator LinearActuator;
    public bool Boolean = false;

    public void Toggle(bool oToggle)
    {
        Boolean = oToggle;
    }
    void Update()
    {
        if (Input.GetKeyDown("w")){
            LinearActuator.Target = 1f;
        }
        if (gameObject.name == "Close" && Boolean)
        {
            LinearActuator.Target = 0f;
        }
        if (gameObject.name == "Open" && Boolean)
        {
            LinearActuator.Target = 1f;
        }
        if (gameObject.name == "CloseGate" && Boolean)
        {
            LinearActuator.Target = 1f;
        }
        if (gameObject.name == "OpenGate" && Boolean)
        {
            LinearActuator.Target = 0f;
        }
    }
}