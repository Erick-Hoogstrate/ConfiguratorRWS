/*using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using System.Text.Json;
using System; //Need this for the convert.Int32

// [ExecuteInEditMode]
public class ConfiguratorV6 : MonoBehaviour
{
    // Configuration file
    public TextAsset file;
    public TextAsset variables;
    public TextAsset inputs;
    private string jsonString;
    private JsonData itemData;

    UnityEngine.GameObject PreLogicSimulator;
    UnityEngine.GameObject KinematicsController;
    UnityEngine.GameObject Components;

    UnityEngine.GameObject PLC;
    UnityEngine.GameObject PLC_Actuators;
    UnityEngine.GameObject PLC_Sensors;
    UnityEngine.GameObject PLC_TL;
    UnityEngine.GameObject PLC_TL_Actuators;
    UnityEngine.GameObject PLC_TL_Sensors;

    UnityEngine.GameObject TwinCAT;
    UnityEngine.GameObject TwinCAT_Actuators;
    UnityEngine.GameObject TwinCAT_Sensors;
    UnityEngine.GameObject TwinCAT_TL;
    UnityEngine.GameObject TwinCAT_TL_Actuators;
    UnityEngine.GameObject TwinCAT_TL_Sensors;


    private Door doors;
    private TrafficLight TL;
    private Wall walls;
    private Water waters;
    private Embankment embankments;
    private Lock locks;


    // Array with all data
    [System.Serializable]
    public class Data
    {
        public string name;
        public float x;
        public float y;
        public float z;
        public List<Lock> position_lock = new List<Lock>();
        public List<Door> data_doors = new List<Door>();
        public List<TrafficLight> data_TL = new List<TrafficLight>();
        public List<Wall> data_walls = new List<Wall>();
        public List<Water> data_waters = new List<Water>();
        public List<Embankment> data_embankments = new List<Embankment>();

        public Data(int index)
        {
            name = "Configuration" + index.ToString();
        }
    }

    public List<Data> data = new List<Data>();
  

    [InspectorButton("OnButtonClickedClear")]
    public bool Clear;

    [InspectorButton("OnButtonClickedLoad")]
    public bool Load;

    [InspectorButton("OnButtonClickedCreate")]
    public bool Create;

    private void OnButtonClickedClear()
    {
        data = new List<Data>();
    }

    private void OnButtonClickedLoad()
    {
        jsonString = File.ReadAllText("Assets/Resources/Configuration/" + file.name + ".json");
        itemData = JsonMapper.ToObject(jsonString);


        // Debug.Log(itemData["name"]);
        // Debug.Log(itemData["cluster"]);
        // Debug.Log(itemData["waterways"].Count);


        for (int i = 0; i < itemData["waterways"].Count; i++)
        {
            Data data_config = new Data(i+1);


            locks = new Lock(0,0,0);// This helps to add things to this list

            locks.positionX = (int)itemData["waterways"][i]["position"][0];
            locks.positionY = (int)itemData["waterways"][i]["position"][1];
            locks.positionZ = (int)itemData["waterways"][i]["position"][2];

            data_config.position_lock.Add(locks);


            for (int j = 0; j < itemData["waterways"][i]["doors"].Count; j++)
            {
                doors = new Door(0,0,0,0,0,0);// This helps to add things to this list

                doors.type = ((int)itemData["waterways"][i]["doors"][j]["type"]);
                doors.direction = ((int)itemData["waterways"][i]["doors"][j]["direction"]);
                doors.width = ((float)itemData["waterways"][i]["doors"][j]["width"]);
                doors.positionX = (float)itemData["waterways"][i]["doors"][j]["position"][0];
                doors.positionY = (float)itemData["waterways"][i]["doors"][j]["position"][1];
                doors.positionZ = (float)itemData["waterways"][i]["doors"][j]["position"][2];

                data_config.data_doors.Add(doors);
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

                data_config.data_TL.Add(TL);
            }

            for (int j = 0; j < itemData["waterways"][i]["walls"].Count; j++)
            {
                walls = new Wall(0,0,0,0);// This helps to add things to this list

                // walls.direction = ((int)itemData["waterways"][i]["walls"][j]["direction"]);
                walls.length = ((float)itemData["waterways"][i]["walls"][j]["length"]);
                walls.positionX = (float)(double)itemData["waterways"][i]["walls"][j]["position"][0];
                walls.positionY = (float)itemData["waterways"][i]["walls"][j]["position"][1];

                if (itemData["waterways"][i]["walls"][j]["position"][2].GetJsonType() == JsonType.Int)
                {
                    walls.positionZ = (float)itemData["waterways"][i]["walls"][j]["position"][2];
                }
                else{
                    walls.positionZ = (float)(double)itemData["waterways"][i]["walls"][j]["position"][2];
                }

                data_config.data_walls.Add(walls);
            }

            for (int j = 0; j < itemData["waterways"][i]["water"].Count; j++)
            {
                waters = new Water(0,0,0,0,0);// This helps to add things to this list

                waters.length = ((float)itemData["waterways"][i]["water"][j]["length"]);
                waters.width = ((float)itemData["waterways"][i]["water"][j]["width"]);
                waters.positionX = (float)itemData["waterways"][i]["water"][j]["position"][0];
                waters.positionY = (float)itemData["waterways"][i]["water"][j]["position"][1];
                waters.positionZ = (float)itemData["waterways"][i]["water"][j]["position"][2];

                data_config.data_waters.Add(waters);
            }

            for (int j = 0; j < itemData["waterways"][i]["embankments"].Count; j++)
            {
                embankments = new Embankment(0,0,0,0,0,0);// This helps to add things to this list

                embankments.length = ((float)itemData["waterways"][i]["embankments"][j]["length"]);
                embankments.width = ((float)itemData["waterways"][i]["embankments"][j]["width"]);
                embankments.height = ((float)(double)itemData["waterways"][i]["embankments"][j]["height"]);
                embankments.positionX = (float)(double)itemData["waterways"][i]["embankments"][j]["position"][0];
                embankments.positionY = (float)(double)itemData["waterways"][i]["embankments"][j]["position"][1];
                embankments.positionZ = (float)itemData["waterways"][i]["embankments"][j]["position"][2];

                data_config.data_embankments.Add(embankments);
            }

            data.Add(data_config);
        }
    }


    private void OnButtonClickedCreate()
    {
        // Standard scripts
        PreLogicSimulator = CreateGameObject("PreLogicSimulator", gameObject);
        KinematicsController = CreateGameObject("KinematicsController", PreLogicSimulator);
        Components = CreateGameObject("Components", KinematicsController);


        //////////////////////// Tags //////////////////////////////
        PLC = CreateGameObject("PLC", KinematicsController);
        TwinCAT = CreateGameObject("TwinCAT", KinematicsController);

        // PLC
        PLC_Actuators = CreateGameObject("Actuators", PLC);
        PLC_Sensors = CreateGameObject("Sensors", PLC);

        PLC_TL = CreateGameObject("Traffic Lights", PLC);
        PLC_TL_Actuators = CreateGameObject("Actuators", PLC_TL);
        PLC_TL_Sensors = CreateGameObject("Sensors", PLC_TL);

        // TwinCAT
        TwinCAT_Actuators = CreateGameObject("Actuators", TwinCAT);
        TwinCAT_Sensors = CreateGameObject("Sensors", TwinCAT);

        TwinCAT_TL = CreateGameObject("Traffic Lights", TwinCAT);
        TwinCAT_TL_Actuators = CreateGameObject("Actuators", TwinCAT_TL);
        TwinCAT_TL_Sensors = CreateGameObject("Sensors", TwinCAT_TL);


        string[] vars = File.ReadAllLines("Assets/Resources/Configuration/" + variables.name + ".txt");
        Tags(vars, true, false, "[default]/TestFolder");
        Tags(vars, true, true, "Main.state0");

        string[] ins = File.ReadAllLines("Assets/Resources/Configuration/" + inputs.name + ".txt");
        Tags(ins, false, false, "[default]/TestFolder");
        Tags(ins, false, true, "Main.state0");

        //////////////////////// Tags end //////////////////////////////


        for (int i = 0; i < itemData["waterways"].Count; i++)
        {
            UnityEngine.GameObject config = CreateGameObject("Configuration" + (i+1).ToString(), Components);

            // Doors
            for (int g = 0; g < data[i].data_doors.Count; g++)
            {
                SpawnObject("Mitre", "Mitre gate", data[i].data_doors[g].positionX, data[i].data_doors[g].positionY, data[i].data_doors[g].positionZ, data[i].data_doors[g].direction, data[i].data_doors[g].width, data[i].data_doors[g].width, data[i].data_doors[g].width, config);
            }

            // Traffic lights
            for (int z = 0; z < data[i].data_TL.Count; z++)
            {
                if (data[i].data_TL[z].type == 0)
                {
                    SpawnObject("TL_entering", "Traffic light entering", data[i].data_TL[z].positionX, data[i].data_TL[z].positionY, data[i].data_TL[z].positionZ, data[i].data_TL[z].direction, data[i].data_TL[z].scale, data[i].data_TL[z].scale, data[i].data_TL[z].scale, config);
                }
                else if (data[i].data_TL[z].type == 1)
                {
                    SpawnObject("TL_leaving", "Traffic light leaving", data[i].data_TL[z].positionX, data[i].data_TL[z].positionY, data[i].data_TL[z].positionZ, data[i].data_TL[z].direction, data[i].data_TL[z].scale, data[i].data_TL[z].scale, data[i].data_TL[z].scale, config);
                }
            }

            // Walls
            for (int g = 0; g < data[i].data_walls.Count; g++)
            {
                SpawnObject("LockWall", "Walls", data[i].data_walls[g].positionX, data[i].data_walls[g].positionY, data[i].data_walls[g].positionZ, 1, data[i].data_walls[g].length, data[i].data_walls[g].length, data[i].data_walls[g].length, config);
            }

            // Waters
            for (int g = 0; g < data[i].data_waters.Count; g++)
            {
                SpawnObject("Water/Water4/Prefabs/Water4Advanced", "Water", data[i].data_waters[g].positionX, data[i].data_waters[g].positionY, data[i].data_waters[g].positionZ, 1, data[i].data_waters[g].width, 1f, data[i].data_waters[g].length, config);
            }

            // Embankments
            for (int g = 0; g < data[i].data_embankments.Count; g++)
            {
                SpawnObject("Embankment", "Embankment", data[i].data_embankments[g].positionX, data[i].data_embankments[g].positionY, data[i].data_embankments[g].positionZ, 1, data[i].data_embankments[g].width, data[i].data_embankments[g].height, data[i].data_embankments[g].length, config);
            }

            config.transform.localPosition = new Vector3(data[i].position_lock[0].positionX, data[i].position_lock[0].positionY, data[i].position_lock[0].positionZ);
        }
    }

    private GameObject CreateGameObject(string name, GameObject parent)
    {

        // Create empty GameObject
        UnityEngine.GameObject objectToSpawn = new GameObject(name);

        // // Making object a child
        objectToSpawn.transform.parent = parent.transform;

        return objectToSpawn;
    }

    private void Tags(string[] lines, bool actuator, bool twincat, string instanceNameRule)
    {
        UnityEngine.GameObject parent;
        if (twincat)
        {
            for (int u = 0; u < lines.Length; u++)
            {
                if (!lines[u].Contains("TL")) 
                {
                    string TagName = lines[u];
                    if (actuator)
                    {
                        parent = TwinCAT_Actuators;
                    }
                    else
                    {
                        parent = TwinCAT_Sensors;
                    }
                    string instance = instanceNameRule;
                    
                    UnityEngine.GameObject Tag = new GameObject(TagName);
                    Tag.transform.parent = parent.transform;
                    Tag.AddComponent<Closed_State_logic>().implicitNamingRule.instanceNameRule = instance;
                    Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].simAddressPathFormat = "{{IO_NAME}}";
                    Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].plcAddressPathFormat = "{{INST_NAME}}.{{IO_NAME}}";
                }
                else
                {
                    string TagName = lines[u];
                    if (actuator)
                    {
                        parent = TwinCAT_TL_Actuators;
                    }
                    else
                    {
                        parent = TwinCAT_TL_Sensors;
                    }
                    string instance = instanceNameRule;
                    UnityEngine.GameObject Tag = new GameObject(TagName);
                    Tag.transform.parent = parent.transform;
                    Tag.AddComponent<Closed_State_logic>().implicitNamingRule.instanceNameRule = instance;
                    Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].simAddressPathFormat = "{{IO_NAME}}";
                    Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].plcAddressPathFormat = "{{INST_NAME}}.{{IO_NAME}}";
                }
            }
        }
        else
        {
            for (int u = 0; u < lines.Length; u++)
            {
                if (!lines[u].Contains("TL")) 
                {
                    string TagName = lines[u];
                    if (actuator)
                    {
                        parent = PLC_Actuators;
                    }
                    else
                    {
                        parent = PLC_Sensors;
                    }
                    string instance = instanceNameRule;
                    
                    UnityEngine.GameObject Tag = new GameObject(TagName);
                    Tag.transform.parent = parent.transform;
                    Tag.AddComponent<Closed_State_logic>().implicitNamingRule.instanceNameRule = instance;
                    Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].simAddressPathFormat = "{{IO_NAME}}";
                    Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].plcAddressPathFormat = "{{INST_NAME}}.{{IO_NAME}}";
                }
                else
                {
                    string TagName = lines[u];
                    if (actuator)
                    {
                        parent = PLC_TL_Actuators;
                    }
                    else
                    {
                        parent = PLC_TL_Sensors;
                    }
                    string instance = instanceNameRule;

                    UnityEngine.GameObject Tag = new GameObject(TagName);
                    Tag.transform.parent = parent.transform;
                    Tag.AddComponent<Closed_State_logic>().implicitNamingRule.instanceNameRule = instance;
                    Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].simAddressPathFormat = "{{IO_NAME}}";
                    Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].plcAddressPathFormat = "{{INST_NAME}}.{{IO_NAME}}";
                }
            }
        }
    }

    private void SpawnObject(string object2, string name, float posx, float posy, float posz, int direction, float width, float height, float length, GameObject parent)
    {
        // Position
        Vector3 spawnPos = new Vector3(posx, posy, posz);

        // Get correct prefab
        UnityEngine.Object objectToSpawn = Resources.Load("Prefabs/" + object2); // note: not .prefab!

        // Instantiate prefab
        GameObject newObject = (GameObject)GameObject.Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

        // Naming object
        newObject.name = name;

        // Making object a child
        newObject.transform.parent = parent.transform;

        // Scale and direction object
        newObject.transform.localScale = new Vector3(
                                            newObject.transform.localScale.x * width,
                                            newObject.transform.localScale.y * height,
                                            newObject.transform.localScale.z * length * direction);
    }


}


// If invert doors +12 in z richting om te compenseren*/