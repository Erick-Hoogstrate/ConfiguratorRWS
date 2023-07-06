using UnityEngine;
using u040.prespective.standardcomponents.kinetics.motor.linearactuator;
using u040.prespective.standardcomponents.virtualhardware.actuators.motors;

public class Open_State : MonoBehaviour
{

    [Tooltip("Boolean Value of the toggle")]
    public  DLinearActuator LinearActuator;
    public bool Boolean = false;

    public void Toggle(bool oToggle)
    {
        Boolean = oToggle;
    }
    void Update()
    {
        if (Boolean)
        {
            LinearActuator.Target = 1f;
        }
    }
}
