using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

[System.Serializable]
public class Embankment
{
    public float length;
    public float width;
    public float height;
    public float positionX;
    public float positionY;
    public float positionZ;

    public Embankment(float Length, float Width, float Height, float posx, float posy, float posz)
    {
        length = Length;
        width = Width;
        height = Height;
        positionX = posx;
        positionY = posy;
        positionZ = posz;
    }

}
