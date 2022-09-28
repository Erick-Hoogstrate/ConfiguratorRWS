using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ConfiguratorV2 : EditorWindow
{
    string data = "";
    public TextAsset textJSON;


    [MenuItem("Tools/Lock creator V2")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ConfiguratorV2));      //GetWindow is a method inherited from the EditorWindow class
    }



    [System.Serializable]
    public class MitreGate
    {

        public int direction;
        public float width;
        public float[] position;
        
    }

    [System.Serializable]
    public class MitreGateList
    {

        public MitreGate[] Gate;
        
    }


    public MitreGateList myMitreGateList = new MitreGateList();



    private void OnGUI()
    {
        data = EditorGUI.TextField(new Rect(10, 25, position.width - 20, 20), "Name data file:", data);

        GUILayout.Space(50);

        if (GUILayout.Button("Spawn Object"))
        {

            string path = "Assets/Resources/Configuration/" + data + ".json";

            StreamReader reader = new StreamReader(path);

            // Debug.Log(reader.GetType());
            // Debug.Log(reader.ReadToEnd());
            //Generator(reader);



            myMitreGateList = JsonUtility.FromJson<MitreGateList>(reader.ReadToEnd());

            Debug.Log(myMitreGateList);

            Debug.Log(myMitreGateList);

            reader.Close();
        }
    }
/*
    private void Generator(StreamReader data)
    {
        Debug.Log("Hey");
        Debug.Log(data.ReadToEnd());

        data = data.ReadToEnd().ToString();

        Debug.Log(data);
    }*/


    private void SpawnObject(string object2, float[] position, string name)
    {
        // Position
        Vector3 spawnPos = new Vector3(position[0], 0f, position[1]);

        // Get correct prefab
        UnityEngine.Object objectToSpawn = Resources.Load("Prefabs/" + object2); // note: not .prefab!

        // Instantiate prefab
        GameObject newObject = (GameObject)GameObject.Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

        // Naming object
        newObject.name = name;

        // Scale object
        newObject.transform.localScale = newObject.transform.localScale;

    }

    // private void SpawnObject(string options_prefabs, int index, string name_prefab)
    // {

    //     // Position
    //     Vector3 spawnPos = new Vector3(spawnCircle.x, 0f, spawnCircle.y);

    //     // Get correct prefab
    //     UnityEngine.Object objectToSpawn = Resources.Load("Prefabs/" + options_prefabs); // note: not .prefab!

    //     // Instantiate prefab
    //     GameObject newObject = (GameObject)GameObject.Instantiate(objectToSpawn, spawnPos, Quaternion.identity);

    //     // Naming object
    //     newObject.name = name_prefab + " " + objectID;

    //     // Scale object
    //     newObject.transform.localScale = newObject.transform.localScale;

    //     objectID++;
    // }

}
