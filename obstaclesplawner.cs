using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class obstaclesplawner : MonoBehaviour
{

    public Vector3[] meshpos;
    GameObject player;
    public float speed ;
    GameObject incommingObstacle;
    GameObject childToDestroy;
    public Vector3 startpos;
    public int obstaclenum;
    public int distanceBetweenObstacle = 30;

    public float stopdis;
    GameObject[] clones;
   

    bool passed;
    public int randindex;
    public GameObject child;
    GameObject nn;
    GameObject[] childs;
    public GameObject obstacle;
    bool done;
    Light light1;
    Light light2;
     int score = 0;
    GameObject text;
    public bool add;
    public  Vector3 clonepos;
    public GameObject bugfixpls;
    

    void destroycube(GameObject h)
    {
        h.GetComponent<BoxCollider>().enabled = false;
        h.GetComponent<MeshRenderer>().enabled = false;
        
    }
    void Awake()
    {   
        add = false;
        Application.targetFrameRate = -1;

        player = GameObject.FindGameObjectWithTag("Player");

        text = GameObject.FindGameObjectWithTag("UI");
    }
    void Start()
    {

        makingObstacleStart();

        obstaclenum = 0;


        incommingObstacle = Instantiate(obstacle, startpos + new Vector3(0, 0, distanceBetweenObstacle), Quaternion.identity);
        incommingObstacle.transform.eulerAngles = new Vector3(0, 0, 0);
        incommingObstacle.gameObject.tag = "clone";
        
        
        


        foreach (Transform t in obstacle.transform) { t.gameObject.GetComponent<MeshRenderer>().enabled = false; }

        childs = new GameObject[incommingObstacle.transform.childCount];
        for (int i = 0; i < incommingObstacle.transform.childCount; i++)
        {

            childs[i] = incommingObstacle.transform.GetChild(i).gameObject;


        }
        destroycube(childs[Random.Range(0, childs.Length)]);
        



        
    }
    void makingObstacleupdate()
    {

        
        childs = new GameObject[transform.childCount];


    }
    void makingObstacleStart()
    {

        for (int f = 0; f < 5; f++)
        {
            for (int i = 0; i < 4; i++)
            {
                nn = Instantiate(child, new Vector3(2.5f * i, 2.5f * f, 0), Quaternion.identity);
                nn.transform.SetParent(obstacle.transform);
                nn.tag = "obstacles";



            }
        }
    }




    void Update()
    {



        if (incommingObstacle.transform.Find("Cube") == null)
        {
            if(incommingObstacle.transform.GetChild(0).transform.childCount < 4){
                
                GameObject cubebug = Instantiate(bugfixpls, bugfixpls.transform.position, Quaternion.identity);
                cubebug.transform.SetParent(incommingObstacle.transform);
                cubebug.transform.SetAsFirstSibling();
                cubebug.transform.position = new Vector3(cubebug.transform.position.x, cubebug.transform.position.y, incommingObstacle.transform.position.z -49.8f);
            
            }
            

        }

        Text  frame = text.GetComponent<Text>();

        frame.text = score.ToString();
        light1 = incommingObstacle.transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<Light>();
        light2 = incommingObstacle.transform.GetChild(0).transform.GetChild(4).gameObject.GetComponent<Light>();
        
        if (light1.isActiveAndEnabled )
        {
            light1.enabled = true;
            
        }
        if (light2.isActiveAndEnabled)
        {
            light2.enabled = true;
        }



        obstacle.transform.position = new Vector3(100, 1000, 0);

        if (done)
        {

            foreach (Transform t in obstacle.transform)
            {
                t.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }

        makingObstacleupdate();

        if ((player.transform.position.z + 54) - stopdis > incommingObstacle.transform.position.z)
        {
            speed = speed + 0.125f;
            StartCoroutine(destroy());
          

        }
        else
        {
            add = false;
        }
    }
    IEnumerator spawn()
    {
        done = true;

        incommingObstacle = Instantiate(obstacle, startpos + new Vector3(0, 0, distanceBetweenObstacle * (obstaclenum)), Quaternion.identity);
        incommingObstacle.transform.eulerAngles = new Vector3(0, 0, 0);
        incommingObstacle.gameObject.tag = "clone";
        childs = new GameObject[incommingObstacle.transform.childCount];
        obstaclenum++;

        for (int i = 0; i < incommingObstacle.transform.childCount; i++)
        {

            childs[i] = incommingObstacle.transform.GetChild(i).gameObject;


        }


         
        destroycube(childs[Random.Range(0, childs.Length)]);
      

        yield return null;
    }
    IEnumerator destroy()
    {
        
        clones = GameObject.FindGameObjectsWithTag("clone");
        score = score + 1;
        
        

        foreach (GameObject clone in clones)
        {
            Destroy(clone);
            

        }

        StartCoroutine(spawn());
        

        yield return null;
    }

}

