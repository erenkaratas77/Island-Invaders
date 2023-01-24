using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.37";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream,data);
        stream.Close();
    }
    public static PlayerData LoadPlayer()
    {
        try
        {
            string path = Application.persistentDataPath + "/player.37";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.Log("Save file not found in " + path);

                return null;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Error Loading Save ");
            return null;
        }


    }

    public static void SaveBase(Base baseSc, int id)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/base"+id+".37";
        FileStream stream = new FileStream(path, FileMode.Create);

        BaseData data = new BaseData(baseSc);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static BaseData LoadBase(int id)
    {
        try
        {
            string path = Application.persistentDataPath + "/base" + id + ".37";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                BaseData data = formatter.Deserialize(stream) as BaseData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.Log("Save file not found in " + path);
                return null;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Error Loading Save ");
            return null;
        }

    }

    public static void SaveWeapon(WeaponManager wm, int id)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/weapon"+id+".37";
        
        FileStream stream = new FileStream(path, FileMode.Create);

        WeaponData data = new WeaponData(wm);

        formatter.Serialize(stream, data);
        stream.Close();
    }  
    public static WeaponData LoadWeapon(int id)
    {
        try
        {
            string path = Application.persistentDataPath + "/weapon" + id + ".37";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                WeaponData data = formatter.Deserialize(stream) as WeaponData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.Log("Save file not found in " + path);
                return null;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Error Loading Save ");
            return null;
        }
        
    }

    public static void SaveBridge(bridgeTrigger bridge, int id)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/bridge" + id + ".37";

        FileStream stream = new FileStream(path, FileMode.Create);

        BridgeData data = new BridgeData(bridge);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static BridgeData LoadBridge(int id)
    {
        try
        {
            string path = Application.persistentDataPath + "/bridge" + id + ".37";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                BridgeData data = formatter.Deserialize(stream) as BridgeData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.Log("Save file not found in " + path);
                return null;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Error Loading Save ");
            return null;
        }

    }

}
