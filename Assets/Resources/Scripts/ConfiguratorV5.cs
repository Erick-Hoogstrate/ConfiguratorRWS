// using System.Collections;
// using System.Collections.Generic;
// using System.IO;
// using UnityEngine;
// using LitJson;
// using System.Text.Json;
// using System; //Need this for the convert.Int32

// // [ExecuteInEditMode]
// public class ConfiguratorV5 : MonoBehaviour
// {
//     // Configuration file
//     public TextAsset file;
//     public TextAsset variables;
//     public TextAsset inputs;
//     private string jsonString;
//     private JsonData itemData;

//     UnityEngine.GameObject PreLogicSimulator;
//     UnityEngine.GameObject KinematicsController;
//     UnityEngine.GameObject Components;

//     UnityEngine.GameObject PLC;
//     UnityEngine.GameObject PLC_Actuators;
//     UnityEngine.GameObject PLC_Sensors;
//     UnityEngine.GameObject PLC_TL;
//     UnityEngine.GameObject PLC_TL_Actuators;
//     UnityEngine.GameObject PLC_TL_Sensors;

//     UnityEngine.GameObject TwinCAT;
//     UnityEngine.GameObject TwinCAT_Actuators;
//     UnityEngine.GameObject TwinCAT_Sensors;
//     UnityEngine.GameObject TwinCAT_TL;
//     UnityEngine.GameObject TwinCAT_TL_Actuators;
//     UnityEngine.GameObject TwinCAT_TL_Sensors;


//     private Door doors;
//     private TrafficLight TL;
//     private Wall walls;
//     private Water waters;
//     private Embankment embankments;


//     // Array with all data
//     [System.Serializable]
//     public class Data
//     {
//         public List<Door> data_doors = new List<Door>();
//         public List<TrafficLight> data_TL = new List<TrafficLight>();
//         public List<Wall> data_walls = new List<Wall>();
//         public List<Water> data_waters = new List<Water>();
//         public List<Embankment> data_embankments = new List<Embankment>();
//     }

//     public Data data = new Data();


//     [InspectorButton("OnButtonClickedClear")]
//     public bool Clear;

//     [InspectorButton("OnButtonClickedLoad")]
//     public bool Load;

//     [InspectorButton("OnButtonClickedCreate")]
//     public bool Create;

//     private void OnButtonClickedClear()
//     {
//         data.data_doors = new List<Door>();
//         data.data_TL = new List<TrafficLight>();
//         data.data_walls = new List<Wall>();
//         data.data_waters = new List<Water>();
//         data.data_embankments = new List<Embankment>();
//     }

//     private void OnButtonClickedLoad()
//     {
//         jsonString = File.ReadAllText("Assets/Resources/Configuration/" + file.name + ".json");
//         itemData = JsonMapper.ToObject(jsonString);


//         // Debug.Log(itemData["name"]);
//         // Debug.Log(itemData["cluster"]);
//         // Debug.Log(itemData["waterways"].Count);


//         for (int i = 0; i < itemData["waterways"].Count; i++)
//         {
//             for (int j = 0; j < itemData["waterways"][i]["doors"].Count; j++)
//             {
//                 doors = new Door(0,0,0,0,0,0);// This helps to add things to this list

//                 doors.type = ((int)itemData["waterways"][i]["doors"][j]["type"]);
//                 doors.direction = ((int)itemData["waterways"][i]["doors"][j]["direction"]);
//                 doors.width = ((float)itemData["waterways"][i]["doors"][j]["width"]);
//                 doors.positionX = (float)itemData["waterways"][i]["doors"][j]["position"][0];
//                 doors.positionY = (float)itemData["waterways"][i]["doors"][j]["position"][1];
//                 doors.positionZ = (float)itemData["waterways"][i]["doors"][j]["position"][2];

//                 data.data_doors.Add(doors);
//             }

//             for (int j = 0; j < itemData["waterways"][i]["traffic_lights"].Count; j++)
//             {
//                 TL = new TrafficLight(0,0,0,0,0,0);// This helps to add things to this list

//                 TL.type = ((int)itemData["waterways"][i]["traffic_lights"][j]["type"]);
//                 TL.direction = ((int)itemData["waterways"][i]["traffic_lights"][j]["direction"]);
//                 TL.scale = ((float)itemData["waterways"][i]["traffic_lights"][j]["scale"]);
//                 TL.positionX = (float)itemData["waterways"][i]["traffic_lights"][j]["position"][0];
//                 TL.positionY = (float)itemData["waterways"][i]["traffic_lights"][j]["position"][1];
//                 TL.positionZ = (float)itemData["waterways"][i]["traffic_lights"][j]["position"][2];

//                 data.data_TL.Add(TL);
//             }

//             for (int j = 0; j < itemData["waterways"][i]["walls"].Count; j++)
//             {
//                 walls = new Wall(0,0,0,0);// This helps to add things to this list

//                 // walls.direction = ((int)itemData["waterways"][i]["walls"][j]["direction"]);
//                 walls.length = ((float)itemData["waterways"][i]["walls"][j]["length"]);
//                 walls.positionX = (float)(double)itemData["waterways"][i]["walls"][j]["position"][0];
//                 walls.positionY = (float)itemData["waterways"][i]["walls"][j]["position"][1];

//                 if (itemData["waterways"][i]["walls"][j]["position"][2].GetJsonType() == JsonType.Int)
//                 {
//                     walls.positionZ = (float)itemData["waterways"][i]["walls"][j]["position"][2];
//                 }
//                 else{
//                     walls.positionZ = (float)(double)itemData["waterways"][i]["walls"][j]["position"][2];
//                 }

//                 data.data_walls.Add(walls);
//             }

//             for (int j = 0; j < itemData["waterways"][i]["water"].Count; j++)
//             {
//                 waters = new Water(0,0,0,0,0);// This helps to add things to this list

//                 waters.length = ((float)itemData["waterways"][i]["water"][j]["length"]);
//                 waters.width = ((float)itemData["waterways"][i]["water"][j]["width"]);
//                 waters.positionX = (float)itemData["waterways"][i]["water"][j]["position"][0];
//                 waters.positionY = (float)itemData["waterways"][i]["water"][j]["position"][1];
//                 waters.positionZ = (float)itemData["waterways"][i]["water"][j]["position"][2];

//                 data.data_waters.Add(waters);
//             }

//             for (int j = 0; j < itemData["waterways"][i]["embankments"].Count; j++)
//             {
//                 embankments = new Embankment(0,0,0,0,0,0);// This helps to add things to this list

//                 embankments.length = ((float)itemData["waterways"][i]["embankments"][j]["length"]);
//                 embankments.width = ((float)itemData["waterways"][i]["embankments"][j]["width"]);
//                 embankments.height = ((float)(double)itemData["waterways"][i]["embankments"][j]["height"]);
//                 embankments.positionX = (float)(double)itemData["waterways"][i]["embankments"][j]["position"][0];
//                 embankments.positionY = (float)(double)itemData["waterways"][i]["embankments"][j]["position"][1];
//                 embankments.positionZ = (float)itemData["waterways"][i]["embankments"][j]["position"][2];

//                 data.data_embankments.Add(embankments);
//             }
//         }
//     }


//     private void OnButtonClickedCreate()
//     {
//         // Standard scripts
//         PreLogicSimulator = CreateGameObject("PreLogicSimulator", gameObject);
//         KinematicsController = CreateGameObject("KinematicsController", PreLogicSimulator);
//         Components = CreateGameObject("Components", KinematicsController);

//         // Tags 
//         PLC = CreateGameObject("PLC", KinematicsController);
//         TwinCAT = CreateGameObject("TwinCAT", KinematicsController);


//         // PLC
//         PLC_Actuators = CreateGameObject("Actuators", PLC);
//         PLC_Sensors = CreateGameObject("Sensors", PLC);

//         PLC_TL = CreateGameObject("Traffic Lights", PLC);
//         PLC_TL_Actuators = CreateGameObject("Actuators", PLC_TL);
//         PLC_TL_Sensors = CreateGameObject("Sensors", PLC_TL);

//         // TwinCAT
//         TwinCAT_Actuators = CreateGameObject("Actuators", TwinCAT);
//         TwinCAT_Sensors = CreateGameObject("Sensors", TwinCAT);

//         TwinCAT_TL = CreateGameObject("Traffic Lights", TwinCAT);
//         TwinCAT_TL_Actuators = CreateGameObject("Actuators", TwinCAT_TL);
//         TwinCAT_TL_Sensors = CreateGameObject("Sensors", TwinCAT_TL);

//         string[] vars = File.ReadAllLines("Assets/Resources/Configuration/" + variables.name + ".txt");
//         Tags(vars, true, false, "[default]/TestFolder");
//         Tags(vars, true, true, "Main.state0");

//         string[] ins = File.ReadAllLines("Assets/Resources/Configuration/" + inputs.name + ".txt");
//         Tags(ins, false, false, "[default]/TestFolder");
//         Tags(ins, false, true, "Main.state0");

//         // Doors
//         for (int g = 0; g < data.data_doors.Count; g++)
//         {
//             SpawnObject("MitreGateBackup", "Mitre gate", data.data_doors[g].positionX, data.data_doors[g].positionY, data.data_doors[g].positionZ, data.data_doors[g].direction, data.data_doors[g].width, data.data_doors[g].width, data.data_doors[g].width);
//         }

//         // Traffic lights
//         for (int z = 0; z < data.data_TL.Count; z++)
//         {
//             if (data.data_TL[z].type == 0)
//             {
//                 SpawnObject("TL_entering", "Traffic light entering", data.data_TL[z].positionX, data.data_TL[z].positionY, data.data_TL[z].positionZ, data.data_TL[z].direction, data.data_TL[z].scale, data.data_TL[z].scale, data.data_TL[z].scale);
//             }
//             else if (data.data_TL[z].type == 1)
//             {
//                 SpawnObject("TL_leaving", "Traffic light leaving", data.data_TL[z].positionX, data.data_TL[z].positionY, data.data_TL[z].positionZ, data.data_TL[z].direction, data.data_TL[z].scale, data.data_TL[z].scale, data.data_TL[z].scale);
//             }
//         }

//         // Walls
//         for (int g = 0; g < data.data_walls.Count; g++)
//         {
//             SpawnObject("LockWall", "Walls", data.data_walls[g].positionX, data.data_walls[g].positionY, data.data_walls[g].positionZ, 1, data.data_walls[g].length, data.data_walls[g].length, data.data_walls[g].length);
//         }

//         // Waters
//         for (int g = 0; g < data.data_waters.Count; g++)
//         {
//             SpawnObject("Water/Water4/Prefabs/Water4Advanced", "Water", data.data_waters[g].positionX, data.data_waters[g].positionY, data.data_waters[g].positionZ, 1, data.data_waters[g].width, 1f, data.data_waters[g].length);
//         }

//         // Embankments
//         for (int g = 0; g < data.data_embankments.Count; g++)
//         {
//             SpawnObject("Embankment", "Embankment", data.data_embankments[g].positionX, data.data_embankments[g].positionY, data.data_embankments[g].positionZ, 1, data.data_embankments[g].width, data.data_embankments[g].height, data.data_embankments[g].length);
//         }
//     }

//     private GameObject CreateGameObject(string name, GameObject parent)
//     {

//         // Create empty GameObject
//         UnityEngine.GameObject objectToSpawn = new GameObject(name);

//         // // Making object a child
//         objectToSpawn.transform.parent = parent.transform;

//         return objectToSpawn;
//     }

//     private void Tags(string[] lines, bool actuator, bool twincat, string instanceNameRule)
//     {
//         UnityEngine.GameObject parent;
//         if (twincat)
//         {
//             for (int u = 0; u < lines.Length; u++)
//             {
//                 if (!lines[u].Contains("TL")) 
//                 {
//                     string TagName = lines[u];
//                     if (actuator)
//                     {
//                         parent = TwinCAT_Actuators;
//                     }
//                     else
//                     {
//                         parent = TwinCAT_Sensors;
//                     }
//                     string instance = instanceNameRule;
                    
//                     UnityEngine.GameObject Tag = new GameObject(TagName);
//                     Tag.transform.parent = parent.transform;
//                     Tag.AddComponent<Closed_State_logic>().implicitNamingRule.instanceNameRule = instance;
//                     Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].simAddressPathFormat = "{{IO_NAME}}";
//                     Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].plcAddressPathFormat = "{{INST_NAME}}.{{IO_NAME}}";
//                 }
//                 else
//                 {
//                     string TagName = lines[u];
//                     if (actuator)
//                     {
//                         parent = TwinCAT_TL_Actuators;
//                     }
//                     else
//                     {
//                         parent = TwinCAT_TL_Sensors;
//                     }
//                     string instance = instanceNameRule;
//                     UnityEngine.GameObject Tag = new GameObject(TagName);
//                     Tag.transform.parent = parent.transform;
//                     Tag.AddComponent<Closed_State_logic>().implicitNamingRule.instanceNameRule = instance;
//                     Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].simAddressPathFormat = "{{IO_NAME}}";
//                     Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].plcAddressPathFormat = "{{INST_NAME}}.{{IO_NAME}}";
//                 }
//             }
//         }
//         else
//         {
//             for (int u = 0; u < lines.Length; u++)
//             {
//                 if (!lines[u].Contains("TL")) 
//                 {
//                     string TagName = lines[u];
//                     if (actuator)
//                     {
//                         parent = PLC_Actuators;
//                     }
//                     else
//                     {
//                         parent = PLC_Sensors;
//                     }
//                     string instance = instanceNameRule;
                    
//                     UnityEngine.GameObject Tag = new GameObject(TagName);
//                     Tag.transform.parent = parent.transform;
//                     Tag.AddComponent<Closed_State_logic>().implicitNamingRule.instanceNameRule = instance;
//                     Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].simAddressPathFormat = "{{IO_NAME}}";
//                     Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].plcAddressPathFormat = "{{INST_NAME}}.{{IO_NAME}}";
//                 }
//                 else
//                 {
//                     string TagName = lines[u];
//                     if (actuator)
//                     {
//                         parent = PLC_TL_Actuators;
//                     }
//                     else
//                     {
//                         parent = PLC_TL_Sensors;
//                     }
//                     string instance = instanceNameRule;

//                     UnityEngine.GameObject Tag = new GameObject(TagName);
//                     Tag.transform.parent = parent.transform;
//                     Tag.AddComponent<Closed_State_logic>().implicitNamingRule.instanceNameRule = instance;
//                     Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].simAddressPathFormat = "{{IO_NAME}}";
//                     Tag.GetComponent<Closed_State_logic>().signalNamingRuleOverrides[0].plcAddressPathFormat = "{{INST_NAME}}.{{IO_NAME}}";
//                 }
//             }
//         }
        
        
//     }

//     private void SpawnObject(string object2, string name, float posx, float posy, float posz, int direction, float width, float height, float length)
//     {
//         // Position
//         Vector3 spawnPos = new Vector3(posx, posy, posz);

//         // Get correct prefab
//         UnityEngine.Object objectToSpawn = Resources.Load("Prefabs/" + object2); // note: not .prefab!

//         // Instantiate prefab
//         GameObject newObject = (GameObject)GameObject.Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

//         // Naming object
//         newObject.name = name;

//         // Making object a child
//         newObject.transform.parent = Components.transform;

//         // Scale and direction object
//         newObject.transform.localScale = new Vector3(
//                                             newObject.transform.localScale.x * width,
//                                             newObject.transform.localScale.y * height,
//                                             newObject.transform.localScale.z * length * direction);
//     }


// }


// // If invert doors +12 in z richting om te compenseren