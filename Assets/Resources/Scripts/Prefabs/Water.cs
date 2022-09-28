using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

[System.Serializable]
public class Water
{
    public float length;
    public float width;
    public float positionX;
    public float positionY;
    public float positionZ;

    public Water(float Length, float Width, float posx, float posy, float posz)
    {
        length = Length;
        width = Width;
        positionX = posx;
        positionY = posy;
        positionZ = posz;
    }

}
