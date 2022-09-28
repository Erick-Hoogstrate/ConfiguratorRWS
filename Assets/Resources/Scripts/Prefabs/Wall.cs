using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

[System.Serializable]
public class Wall
{
    public float length;
    public float positionX;
    public float positionY;
    public float positionZ;

    public Wall(float Length, float posx, float posy, float posz)
    {
        length = Length;
        positionX = posx;
        positionY = posy;
        positionZ = posz;
    }

}
