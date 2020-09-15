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
        if (progress <= sortedBricks.Count)
        {
            for (int i = 1; i < progress; i++)
            {
                sortedBricks[i].SetActive(true);
            }
        }
        else if (progress > sortedBricks.Count)
        {
            for (int i = 1; i < sortedBricks.Count; i++)
            {
                sortedBricks[i].SetActive(true);
            }
        }
    }



    private void spawn()
    {

    }


    void Update()
    {
        if (GameControl.control.progress != progress)
        {
            if (progress >= 0 && progress <= sortedBricks.Count)
            {
                //Render those behind game
                if (progress < GameControl.control.progress)
                {
                    for (int i = progress; i <= GameControl.control.progress; i++)
                    {
                        sortedBricks[i].SetActive(true);
                        print(sortedBricks[i].transform.position);
                    }
                    //render those between progress and GameControl
                }
                //Un-render those ahead of game
                else if (progress > GameControl.control.progress)
                {
                    for (int i = GameControl.control.progress; i <= progress; i++)
                        //un-render those in between 
                        sortedBricks[i].SetActive(false);
                }
                //Take progress from game
                progress = GameControl.control.progress;
                //if (Input.GetKey(KeyCode.W)) //WANT TO DO THIS WHEN MESSAGE COMES IN, WITHOUT DIFFICUILT LOGIC!
                //if gamecontrol... NOT EQUAL to progress, i.e. doesn't have to be lopsided?
                //{
                //sortedBricks[progress].GetComponent<MeshRenderer>().enabled = true ;
                //}
                /*if (Input.GetKeyUp(KeyCode.W))
                {
                    progress++;
                    GameControl.control.progress = progress;
                }*/
            }
        }
    }
    private void OnApplicationQuit()
    {
        GameControl.control.Save();
        print("saved");
    }
}