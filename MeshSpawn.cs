using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        if (progress <= sortedBricks.Count)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //sortedBricks[progress].GetComponent<MeshRenderer>().enabled = true ;
                sortedBricks[progress].SetActive(true);
                print(progress);
                print(sortedBricks[progress].transform.position);

            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                progress++;
                GameControl.control.progress = progress;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameControl.control.Save();
            print("save");
        }
    }
}
