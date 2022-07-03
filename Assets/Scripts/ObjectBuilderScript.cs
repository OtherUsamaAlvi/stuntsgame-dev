using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class ObjectBuilderScript : MonoBehaviour
{
    [Header("Meta")]
    public string persisterName;
    [Header("Scriptable Objects")]
    public List<ScriptableObject> objectsToPersist;

    [Header("Scriptable Default Objects")]
    public List<ScriptableObject> objectsToReset;
    public void DeleteData()
    {
        for (int i = 0; i < objectsToPersist.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i));
                Debug.Log("Data Delete");
            }
            else
            {
                Debug.Log("No file found");
            }
        }

        PlayerPrefs.DeleteAll();
    }

    public void ResetData()
    {
        for (int i = 0; i < objectsToReset.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i));
            var json = JsonUtility.ToJson(objectsToReset[i]);
            

            bf.Serialize(file, json);


            file.Close();
        }
        for (int i = 0; i < objectsToPersist.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i), FileMode.Open);

                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), objectsToPersist[i]);

                file.Close();

            }
            else
            {
                //Do Nothing
            }
        }
    }
}
