using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Configurator : EditorWindow
{
    // Editor
    int toolbarIndex = 0;
    string[] toolbarStrings = {"Cluster 1", "Cluster 2", "Cluster 3", "Cluster 4", "Cluster 5", "Cluster 6", "Cluster 7"};

    // Prefabs
    // string objectBaseName = "";
    private int objectID = 1;
    GameObject objectToSpawn;
    float objectLockScale, objectEnvironmentScale;
    private float spawnRadius = 50f;
    public string[] prefabLockOptions = new string[] {"Mitre gate", "Walls", "Traffic light entering", "Traffic light leaving"};
    public int prefabLockIndex = -1;
    public string[] prefabEnvironmentOptions = new string[] {"Water", "Kade", "Road", "Train track", "Traffic light", "Bridge"};
    public int prefabEnvironmentIndex = -1;

    // Prefabs spawning
    private string[] spawnLockOptions = new string[] {"MitreGate", "lock_wall", "TL_entering", "TL_leaving"};
    private string[] spawnEnvironmentOptions = new string[] {"Water/Water4/Prefabs/Water4Advanced", "Kade", "road", "train_track", "Traffic Light", "brug"};

    [MenuItem("Tools/Lock creator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(Configurator));      //GetWindow is a method inherited from the EditorWindow class
    }

    private void OnGUI()
    {
        toolbarIndex = GUILayout.Toolbar(toolbarIndex, toolbarStrings);
        switch(toolbarIndex)
        {

            case 3:
                GUILayout.Label("Basic Modules", EditorStyles.largeLabel);

                EditorGUILayout.Popup("Object to Spawn:", -1, prefabLockOptions);
                objectLockScale = EditorGUILayout.Slider("Object Scale", objectLockScale, 0.5f, 3f);
                
                if (GUILayout.Button("Spawn Object"))
                {
                    // SpawnObject();
                }
                break;

            case 4:
                GUILayout.Label("Lock components:", EditorStyles.boldLabel);
                
                prefabLockIndex = EditorGUILayout.Popup("Object to Spawn:", prefabLockIndex, prefabLockOptions);
                objectLockScale = EditorGUILayout.Slider("Object Scale", objectLockScale, 0.5f, 3f) ;

                if (GUILayout.Button("Spawn Object"))
                {
                    SpawnObject(spawnLockOptions[prefabLockIndex], prefabLockIndex, prefabLockOptions[prefabLockIndex]);
                    // Debug.Log(prefabLockIndex);
                    // Debug.Log(prefabLockOptions[prefabLockIndex]);
                }

                GUILayout.Space(20);
                
                GUILayout.Label("Environment components:", EditorStyles.boldLabel);
                
                prefabEnvironmentIndex = EditorGUILayout.Popup("Object to Spawn:", prefabEnvironmentIndex, prefabEnvironmentOptions);
                objectEnvironmentScale = EditorGUILayout.Slider("Object Scale", objectEnvironmentScale, 0.5f, 3f) ;

                if (GUILayout.Button("Spawn Object"))
                {
                    SpawnObject(spawnEnvironmentOptions[prefabEnvironmentIndex], prefabEnvironmentIndex, prefabEnvironmentOptions[prefabEnvironmentIndex]);
                    // Debug.Log(prefabEnvironmentIndex);
                    // Debug.Log(prefabEnvironmentOptions[prefabEnvironmentIndex]);
                }
                break; //Used for switch/case
        }

    }

    private void SpawnObject(string options_prefabs, int index, string name_prefab)
    {
        if(index == -1)
        {
            Debug.LogError("Error: Please assign an object to be spawned.");
            return;
        }

        Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = new Vector3(spawnCircle.x, 0f, spawnCircle.y);


        UnityEngine.Object objectToSpawn = Resources.Load("Prefabs/" + options_prefabs); // note: not .prefab!
        // Debug.Log(objectToSpawn);

        GameObject newObject = (GameObject)GameObject.Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

        newObject.name = name_prefab + " " + objectID;
        // newObject.transform.localScale = Vector3.one * objectLockScale;
        newObject.transform.localScale = newObject.transform.localScale;

        // if (options_prefabs == "TF_two")
        // {
        //     newObject.transform.Rotate(-90,0,0);
        // }

        objectID++;
    }


    // private void SpawnObject(options_prefabs, index, name_prefab)
    // {
    //     if(prefabLockIndex == -1)
    //     {
    //         Debug.LogError("Error: Please assign an object to be spawned.");
    //         return;
    //     }

    //     Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
    //     Vector3 spawnPos = new Vector3(spawnCircle.x, 0f, spawnCircle.y);


    //     UnityEngine.Object objectToSpawn = Resources.Load("Prefabs/" + spawnLockOptions[prefabLockIndex]); // note: not .prefab!
    //     Debug.Log(objectToSpawn);

    //     GameObject newObject = (GameObject)GameObject.Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

    //     newObject.name = prefabLockOptions[prefabLockIndex] + " " + objectID;
    //     newObject.transform.localScale = Vector3.one * objectLockScale;

    //     if (spawnLockOptions[prefabLockIndex] == "TF_two")
    //     {
    //         newObject.transform.Rotate(-90,0,0);
    //     }

    //     objectID++;
    // }


    // Use this to create a folder structure
    // Perhaps create all folders the first time you press one of the spawn buttons, change bool variable which says the folders are already created
    // Next place each gameobject in each corresponding folder

    // private void getInfo()
    // {
    //     foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
    //     {
    //         if (obj.transform.parent == null && obj.name != "Main Camera" && obj.name != "Directional Light")
    //         {
    //             string checker = "test";
    //             GameObject objToSpawn;
    //             if (obj.name.Contains(checker))
    //             {
    //                 Debug.Log("Exist");
                    
    //                 objToSpawn = new GameObject("folder");
    //                 // Debug.Log(obj.name);
    //             }
    //             else
    //             {
    //                 Debug.Log("Does not exist");
    //             }
    //         }
    //     }
    // }
}
