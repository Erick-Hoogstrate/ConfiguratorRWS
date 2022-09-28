using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

[System.Serializable]
public class Door
{
    public int type;
    public int direction;
    public float width;
    public string name;
    public float positionX;
    public float positionY;
    public float positionZ;

    public Door(int Type, int dir, float Width, string Name, float posx, float posy, float posz)
    {
        type = Type;
        direction = dir;
        width = Width;
        name = Name;
        positionX = posx;
        positionY = posy;
        positionZ = posz;
    }

}
