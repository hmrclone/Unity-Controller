using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSpawn : MonoBehaviour
{
    Mesh mesh;
    public List<Vector3> verts;

    public GameObject brickPrefab;
    public float spawnTime = 1.0f;
    //List<GameObject> bricks;

    void Start()
    {
        mesh = this.GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            GameObject a = Instantiate(brickPrefab) as GameObject;
            a.transform.parent = gameObject.transform;
            a.transform.position = vertices[i];
            //a.GetComponent<MeshRenderer>().enabled = false;
            //bricks.Add(a);
        }
        print(vertices.Length);
    }


    private void spawn() {
        
    }


    void Update()
    {
        
    }
}
