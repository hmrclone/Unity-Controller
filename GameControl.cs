using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary; //need to use binary files for serialization - more secure than txt
using System.IO;

public class GameControl : MonoBehaviour
{
    public static GameControl control; //makes this a singleton

    public float health;
    public float experience;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject); //if attached to a GO, this behaviour will apply to the GO in question and any variable values set there will persist across scenes
            control = this; //this will become the one object that’s referenced by the ‘control’ variable
        }
        else if (control != this)
        {
            Destroy(gameObject); //do not want to make another control object if one already exists
        }
        print(Application.persistentDataPath);
    }

    //ANYTHING ELSE YOU DO IN THIS SCRIPT WILL THEN ALSO HAPPEN ACROSS SCENES, E.G. ADDING GUI ELEMENTS WITH void OnGUI(){}!
    //The public static nature means you can just call GameControl.control.health etc and modify from anywhere without any Get etc. - remember this important thing from last time!

    public void Save() //public so other classes can save and load if we like
    {
        BinaryFormatter bf = new BinaryFormatter(); //necessary
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); //saving to default persistent data path - could have bunch of different files for different data
        PlayerData data = new PlayerData();
        data.health = health; //putting local values in container to serialize
        data.experience = experience;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load() //Can use in other script as GameControl.control.Load();
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        //check file exists first to avoid error
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file); //creating an object from file data and casting to PlayerData type from generic type from deserialization
            file.Close();

            health = data.health;
            experience = data.experience;
        }
    } 
}

[Serializable] //class just used as data container, we literally just have to say it's serializable here and then it is
class PlayerData
{
    public float health;
    public float experience;
}