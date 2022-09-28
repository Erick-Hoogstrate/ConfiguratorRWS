/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using LitJson;
using System.Text.Json;
using System; //Need this for the convert.Int32

//[CustomEditor(typeof(ListTester))]
public class ConfiguratorV3 : EditorWindow
{
    [SerializeField]
    public List<Door> data = new List<Door>();

    public Door doors;

    private string jsonString;
    private JsonData itemData;


    [MenuItem("Tools/Lock creator V3")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ConfiguratorV3));      //GetWindow is a method inherited from the EditorWindow class
    }

    private void OnGUI()
    {

        GUILayout.Space(50);
        

        if (GUILayout.Button("Spawn Object"))
        {
            Debug.Log("Hey");
            jsonString = File.ReadAllText("Assets/Resources/Configuration/data.json");
            itemData = JsonMapper.ToObject(jsonString);


            Debug.Log(itemData["name"]);
            Debug.Log(itemData["cluster"]);

            for (int i = 0; i < itemData["waterways"].Count; i++)
            {
                for (int j = 0; j < itemData["waterways"][0]["doors"].Count; j++)
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

                    data.Add(doors);

                    // Spawn object
                    // SpawnObject("MitreGate", "Mitre gate", doors.positionX, doors.positionY, doors.direction, doors.width);
                }
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
*/