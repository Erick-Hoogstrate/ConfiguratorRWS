using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

[System.Serializable]
public class TrafficLight
{
    public int type;
    public int direction;
    public float scale;
    public string name;
    public float positionX;
    public float positionY;
    public float positionZ;

    public TrafficLight(int Type, int dir, float Scale, string Name, float posx, float posy, float posz)
    {
        type = Type;
        direction = dir;
        scale = Scale;
        name = Name;
        positionX = posx;
        positionY = posy;
        positionZ = posz;
    }

}