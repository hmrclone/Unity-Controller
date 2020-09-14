using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class MeshSpawn : MonoBehaviour
{
    Mesh mesh;
    public List<Vector3> verts;

    public GameObject brickPrefab;
    public float spawnTime = 1.0f;
    List<GameObject> bricks = new List<GameObject>();
    List<GameObject> sortedBricks = new List<GameObject>();

    public int progress;

    void Start()
    {
        progress = GameControl.control.progress;
        mesh = this.GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            GameObject a = Instantiate(brickPrefab) as GameObject;
            a.transform.parent = gameObject.transform;
            a.transform.localPosition = vertices[i];
            //a.GetComponent<MeshRenderer>().enabled = false;
            a.SetActive(false);
            bricks.Add(a);
        }
        //IEnumerable<GameObject> brickSorted = bricks.OrderBy(GameObject => GameObject.transform.localPosition.y);
        sortedBricks = bricks.OrderBy(brick => brick.transform.localPosition.z).ToList();
        print(vertices.Length);
        print(sortedBricks.Count);
        bricks = new List<GameObject>();
        if (progress <= sortedBricks.Count) {
            for (int i = 1; i < progress; i++) {
                sortedBricks[i].SetActive(true);
            }
        }
        else if (progress>sortedBricks.Count) {
            for (int i = 1; i < sortedBricks.Count; i++) {
                sortedBricks[i].SetActive(true);
            }
        }
    }



    private void spawn()
    {

    }


    void Update()
    {
        if (progress < GameControl.control.progress) {
            progress = GameControl.control.progress;
            if (progress <= sortedBricks.Count)
            {
                for (int i = GameControl.control.progress; i <= progress; i++)
                {
                    //sortedBricks[progress].GetComponent<MeshRenderer>().enabled = true ;
                    sortedBricks[i].SetActive(true);
                    print(i);
                    print(sortedBricks[i].transform.position);
                }
            }
            GameControl.control.Save();
            print("save");
        }
        if (progress > GameControl.control.progress) {
            if (progress <= sortedBricks.Count)
            {
                for (int i = GameControl.control.progress; i <= progress; i++)
                {
                    //sortedBricks[progress].GetComponent<MeshRenderer>().enabled = true ;
                    sortedBricks[i].SetActive(true);
                    print(i);
                    print(sortedBricks[i].transform.position);
                }
            }
            GameControl.control.progress = progress;
            GameControl.control.Save();
            print("save");
        }
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            progress++;
            GameControl.control.progress = progress;

        }
    }

    private void OnApplicationQuit()
    {
        GameControl.control.Save();
    }
}
