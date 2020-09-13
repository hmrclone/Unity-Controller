using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustTest : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 100, 100, 30), "Health up"))
        {
            GameControl.control.health += 10;
        }
        if (GUI.Button(new Rect(10, 140, 100, 30),"Health down"))
        {
            GameControl.control.health -= 10;
        }
        if (GUI.Button(new Rect(10, 180, 100, 30), "Save"))
        {
            GameControl.control.Save();
        }
        if (GUI.Button(new Rect(10, 200, 100, 30), "Load"))
        {
            GameControl.control.Load();
        }
    }
}
