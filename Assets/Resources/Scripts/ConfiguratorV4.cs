/*using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using System.Text.Json;
using System; //Need this for the convert.Int32

// [ExecuteInEditMode]
public class ConfiguratorV4 : MonoBehaviour
{
    // Configuration file
    public TextAsset file;
    private string jsonString;
    private JsonData itemData;


    private Door doors;
    private TrafficLight TL;
    private Wall walls;
    private Water waters;


    // Array with all data
    [System.Serializable]
    public class Data
    {
        public List<Door> data_doors = new List<Door>();
        public List<TrafficLight> data_TL = new List<TrafficLight>();
        public List<Wall> data_walls = new List<Wall>();
        public List<Water> data_waters = new List<Water>();
    }

    public Data data = new Data();

    [InspectorButton("OnButtonClickedClear")]
    public bool Clear;

    [InspectorButton("OnButtonClickedLoad")]
    public bool Load;

    [InspectorButton("OnButtonClickedCreate")]
    public bool Create;

    private void OnButtonClickedClear()
    {
        data.data_doors = new List<Door>();
        data.data_TL = new List<TrafficLight>();
        data.data_walls = new List<Wall>();
        data.data_waters = new List<Water>();
    }

    private void OnButtonClickedLoad()
    {
        jsonString = File.ReadAllText("Assets/Resources/Configuration/" + file.name + ".json");
        itemData = JsonMapper.ToObject(jsonString);


        Debug.Log(itemData["name"]);
        Debug.Log(itemData["cluster"]);


        for (int i = 0; i < itemData["waterways"].Count; i++)
        {
            for (int j = 0; j < itemData["waterways"][i]["doors"].Count; j++)
            {
                doors = new Door(0,0,0,0,0,0);// This helps to add things to this list

                doors.type = ((int)itemData["waterways"][i]["doors"][j]["type"]);
                doors.direction = ((int)itemData["waterways"][i]["doors"][j]["direction"]);
                doors.width = ((float)itemData["waterways"][i]["doors"][j]["width"]);
                doors.positionX = (float)itemData["waterways"][i]["doors"][j]["position"][0];
                doors.positionY = (float)itemData["waterways"][i]["doors"][j]["position"][1];
                doors.positionZ = (float)itemData["waterways"][i]["doors"][j]["position"][2];

                data.data_doors.Add(doors);
            }

            for (int j = 0; j < itemData["waterways"][i]["traffic_lights"].Count; j++)
            {
                TL = new TrafficLight(0,0,0,0,0,0);// This helps to add things to this list

                TL.type = ((int)itemData["waterways"][i]["traffic_lights"][j]["type"]);
                TL.direction = ((int)itemData["waterways"][i]["traffic_lights"][j]["direction"]);
                TL.scale = ((float)itemData["waterways"][i]["traffic_lights"][j]["scale"]);
                TL.positionX = (float)itemData["waterways"][i]["traffic_lights"][j]["position"][0];
                TL.positionY = (float)itemData["waterways"][i]["traffic_lights"][j]["position"][1];
                TL.positionZ = (float)itemData["waterways"][i]["traffic_lights"][j]["position"][2];

                data.data_TL.Add(TL);
            }

            for (int j = 0; j < itemData["waterways"][i]["walls"].Count; j++)
            {
                walls = new Wall(0,0,0,0);// This helps to add things to this list

                // walls.direction = ((int)itemData["waterways"][i]["walls"][j]["direction"]);
                walls.scale = ((float)itemData["waterways"][i]["walls"][j]["scale"]);
                walls.positionX = (float)itemData["waterways"][i]["walls"][j]["position"][0];
                walls.positionY = (float)itemData["waterways"][i]["walls"][j]["position"][1];

                if (itemData["waterways"][i]["walls"][j]["position"][2].GetJsonType() == JsonType.Int)
                {
                    walls.positionZ = (float)itemData["waterways"][i]["walls"][j]["position"][2];
                }
                else{
                    walls.positionZ = (float)(double)itemData["waterways"][i]["walls"][j]["position"][2];
                }

                data.data_walls.Add(walls);
            }

            for (int j = 0; j < itemData["waterways"][i]["water"].Count; j++)
            {
                waters = new Water(0,0,0,0,0);// This helps to add things to this list

                waters.length = ((int)itemData["waterways"][i]["water"][j]["length"]);
                waters.width = ((float)itemData["waterways"][i]["water"][j]["width"]);
                waters.positionX = (float)itemData["waterways"][i]["water"][j]["position"][0];
                waters.positionY = (float)itemData["waterways"][i]["water"][j]["position"][1];
                waters.positionZ = (float)itemData["waterways"][i]["water"][j]["position"][2];

                data.data_waters.Add(waters);
            }
        }
    }


    private void OnButtonClickedCreate()
    {
        // Doors
        for (int g = 0; g < data.data_doors.Count; g++)
        {
            SpawnObject("MitreGate", "Mitre gate", data.data_doors[g].positionX, data.data_doors[g].positionY, data.data_doors[g].positionZ, data.data_doors[g].direction, data.data_doors[g].width);
        }

        // Traffic lights
        for (int z = 0; z < data.data_TL.Count; z++)
        {
            if (data.data_TL[z].type == 0)
            {
                SpawnObject("TL_entering", "Traffic light entering", data.data_TL[z].positionX, data.data_TL[z].positionY, data.data_TL[z].positionZ, data.data_TL[z].direction, data.data_TL[z].scale);
            }
            else if (data.data_TL[z].type == 1)
            {
                SpawnObject("TL_leaving", "Traffic light leaving", data.data_TL[z].positionX, data.data_TL[z].positionY, data.data_TL[z].positionZ, data.data_TL[z].direction, data.data_TL[z].scale);
            }
        }

        // Walls
        for (int g = 0; g < data.data_walls.Count; g++)
        {
            SpawnObject("LockWall", "Walls", data.data_walls[g].positionX, data.data_walls[g].positionY, data.data_walls[g].positionZ, 1, data.data_walls[g].scale);
        }

        // Waters
        for (int g = 0; g < data.data_waters.Count; g++)
        {
            SpawnObject("Water/Water4/Prefabs/Water4Advanced", "Water", data.data_waters[g].positionX, data.data_waters[g].positionY, data.data_waters[g].positionZ, 1, data.data_waters[g].width);
        }
    }


    private void SpawnObject(string object2, string name, float posx, float posy, float posz, int direction, float scale)
    {
        // Position
        Vector3 spawnPos = new Vector3(posx, posy, posz);

        // Get correct prefab
        UnityEngine.Object objectToSpawn = Resources.Load("Prefabs/" + object2); // note: not .prefab!

        // Instantiate prefab
        GameObject newObject = (GameObject)GameObject.Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

        // Naming object
        newObject.name = name;

        // Scale and direction object
        newObject.transform.localScale = new Vector3(
                                            newObject.transform.localScale.x * scale,
                                            newObject.transform.localScale.y * scale,
                                            newObject.transform.localScale.z * scale * direction);
    }

}


// If invert doors +12 in z richting om te compenseren*/