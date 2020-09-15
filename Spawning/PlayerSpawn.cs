using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    List<GameObject> playerGhosts = new List<GameObject>();
    public GameObject ghostPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.control.playerSpawn == true) {
            GhostSpawn();
            GameControl.control.playerSpawn = false;
        }
    }

    void GhostSpawn() {
        GameObject a = Instantiate(ghostPrefab) as GameObject;
        a.transform.parent = gameObject.transform;
        //a.GetComponent<MeshRenderer>().enabled = false;
        playerGhosts.Add(a);
    }
}
