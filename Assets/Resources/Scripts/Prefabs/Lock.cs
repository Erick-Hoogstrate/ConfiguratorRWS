using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

[System.Serializable]
public class Lock
{
    public string name;
    public float positionX;
    public float positionY;
    public float positionZ;

    public Lock(string Name, float posx, float posy, float posz)
    {
        name = Name;
        positionX = posx;
        positionY = posy;
        positionZ = posz;
    }

}
