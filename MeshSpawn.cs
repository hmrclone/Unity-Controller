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

    private int progress = 0;

    void Start()
    {
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
        print(bricks.Count);
    }



    private void spawn()
    {

    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //bricks[progress].GetComponent<MeshRenderer>().enabled = true ;

            sortedBricks[progress].SetActive(true);
            print(progress);
            print(sortedBricks[progress].transform.position);

        }
        if (Input.GetKeyUp(KeyCode.W)) {
            progress++;
        }
    }
}
