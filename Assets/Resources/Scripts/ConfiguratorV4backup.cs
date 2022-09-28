/*using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using System.Text.Json;
using System; //Need this for the convert.Int32

// [ExecuteInEditMode]
public class ConfiguratorV4backup : MonoBehaviour
{
    // Configuration file
    public TextAsset file;
    private string jsonString;
    private JsonData itemData;

    // List with data for components
    private List<Door> data_doors = new List<Door>();
    private List<TrafficLight> data_TL = new List<TrafficLight>();

    private Door doors;
    private TrafficLight TL;


    // Array with all data
    [System.Serializable]
    public class Data
    {
        public List<Door> data_doors = new List<Door>();
        public List<TrafficLight> data_TL = new List<TrafficLight>();
    }

    public Data data = new Data();

    [InspectorButton("OnButtonClickedLoad")]
    public bool Load;

    [InspectorButton("OnButtonClickedCreate")]
    public bool Create;

    private void OnButtonClickedLoad()
    {
        jsonString = File.ReadAllText("Assets/Resources/Configuration/" + file.name + ".json");
        itemData = JsonMapper.ToObject(jsonString);


        Debug.Log(itemData["name"]);
        Debug.Log(itemData["cluster"]);

        Debug.Log(data.data_doors[0].type);


        for (int i = 0; i < itemData["waterways"].Count; i++)
        {
            for (int j = 0; j < itemData["waterways"][i]["doors"].Count; j++)
            {
                doors = new Door(0,0,0,0,0);// This helps to add things to this list

                // Debug.Log("i"+i);
                // Debug.Log("j"+j);
                // Debug.Log("waarde" + ((int)itemData["waterways"][i]["doors"][j]["position"][0]));

                doors.type = ((int)itemData["waterways"][i]["doors"][j]["type"]);
                doors.direction = ((int)itemData["waterways"][i]["doors"][j]["direction"]);
                doors.width = ((float)itemData["waterways"][i]["doors"][j]["width"]);
                doors.positionX = (float)itemData["waterways"][i]["doors"][j]["position"][0];
                doors.positionY = (float)itemData["waterways"][i]["doors"][j]["position"][1];

                data_doors.Add(doors);

                // Spawn object
                // SpawnObject("MitreGate", "Mitre gate", doors.positionX, doors.positionY, doors.direction, doors.width);
            }

            for (int j = 0; j < itemData["waterways"][i]["traffic_lights"].Count; j++)
            {
                TL = new TrafficLight(0,0,0,0,0);// This helps to add things to this list

                // Debug.Log("i"+i);
                // Debug.Log("j"+j);
                // Debug.Log("waarde" + ((int)itemData["waterways"][i]["traffic_lights"][j]["position"][0]));

                TL.type = ((int)itemData["waterways"][i]["traffic_lights"][j]["type"]);
                TL.direction = ((int)itemData["waterways"][i]["traffic_lights"][j]["direction"]);
                TL.scale = ((float)itemData["waterways"][i]["traffic_lights"][j]["scale"]);
                TL.positionX = (float)itemData["waterways"][i]["traffic_lights"][j]["position"][0];
                TL.positionY = (float)itemData["waterways"][i]["traffic_lights"][j]["position"][1];

                data_TL.Add(TL);

                // Spawn object
                // SpawnObject("MitreGate", "Mitre gate", doors.positionX, doors.positionY, doors.direction, doors.width);
            }

        }
    }


    private void OnButtonClickedCreate()
    {

        for (int i = 0; i < data_doors.Count; i++)
        {
            SpawnObject("MitreGate", "Mitre gate", data_doors[i].positionX, data_doors[i].positionY, data_doors[i].direction, data_doors[i].width);
        }
        
        for (int i = 0; i < data_TL.Count; i++)
        {
            if (data_TL[i].type == 0)
            {
                SpawnObject("TL_entering", "Traffic light entering", data_TL[i].positionX, data_TL[i].positionY, data_TL[i].direction, data_TL[i].scale);
            }
            else if (data_TL[i].type == 1)
            {
                SpawnObject("TL_leaving", "Traffic light leaving", data_TL[i].positionX, data_TL[i].positionY, data_TL[i].direction, data_TL[i].scale);
            }
                
            
        }

    }


    private void SpawnObject(string object2, string name, float posx, float posy, int direction, float scale)
    {
        // Position
        Vector3 spawnPos = new Vector3(posx, 0f, posy);

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