using System;

using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{   float speed;
    public float speedmove;
    public float radius = 2;
    Ray ray;
    RaycastHit hit;
    Vector3 moveforward;
    int score;
    public Vector3 holepos;
    bool canmove;
    public Camera cam;
    bool done;
    public Joystick joystick;
    public string mode;



    GameObject spawner;

     void Start()
    {


        
        
        
        
        done = false;
        spawner =  GameObject.FindGameObjectWithTag("GameController");
        
        canmove = true;
        



       

    }
    void MoveSelection(string mode)
    {
        if (mode == "touch")
        {
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit))
                {

                    if (transform.position.x > -2 && transform.position.x < 6 && transform.position.y > -9 && transform.position.y < 1.7f)
                    {

                        transform.position = new Vector3(Mathf.Lerp(transform.position.x, hit.point.x, 0.1f), Mathf.Lerp(transform.position.y, hit.point.y, speedmove), transform.position.z);
                    }
                    else if (!(transform.position.x > -2))
                    {
                        transform.position = new Vector3(transform.position.x + 2 * Mathf.Abs(transform.position.x - (-2)), transform.position.y, transform.position.z);
                    }
                    else if (!(transform.position.x < 6))
                    {
                        transform.position = new Vector3(transform.position.x - 2 * Mathf.Abs(transform.position.x - 6), transform.position.y, transform.position.z);
                    }
                    else if (!(transform.position.y > -9))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 2 * Mathf.Abs(transform.position.y - (-9)), transform.position.z);
                    }
                    else if (!(transform.position.y < 1.7f))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y - 2 * Mathf.Abs(transform.position.y - 1.7f), transform.position.z);
                    }
                }
            }
        }
        else if (mode == "infinity")
        {  if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit))
                {

                    if (transform.position.x > -2 && transform.position.x < 6 && transform.position.y > -9 && transform.position.y < 1.7f)
                    {
                        float x = joystick.Horizontal;
                        float y = joystick.Vertical;
                        Vector3 move = transform.right * x + transform.up * y;
                        transform.Translate(move * speed * Time.deltaTime);
                       
                    }

                    else if (!(transform.position.x > -2))
                    {
                        transform.position = new Vector3(transform.position.x + 2 * Mathf.Abs(transform.position.x - (-2)), transform.position.y, transform.position.z);
                    }
                    else if (!(transform.position.x < 6))
                    {
                        transform.position = new Vector3(transform.position.x - 2 * Mathf.Abs(transform.position.x - 6), transform.position.y, transform.position.z);
                    }
                    else if (!(transform.position.y > -9))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 2 * Mathf.Abs(transform.position.y - (-9)), transform.position.z);
                    }
                    else if (!(transform.position.y < 1.7f))
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y - 2 * Mathf.Abs(transform.position.y - 1.7f), transform.position.z);
                    }
                }
            }
        }
      }

    void Update()
    {
        mode = PlayerPrefs.GetString("mode");
      
        speed = spawner.GetComponent<obstaclesplawner>().speed;

        MoveSelection(mode);

        if (cam.transform.position.y > 1.1f && !done)
        {
            done = true;
            if (done)
            {
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 0.3f, cam.transform.position.z);
                done = false;

            }

        }

        moveforward = Vector3.forward * speed * Time.deltaTime;


        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (canmove)
        {
            transform.Translate(moveforward);
        }
    }
       
         


    private void OnCollisionEnter(Collision collision)
    {
       if(collision.collider.gameObject.tag == "obstacles" && Time.timeSinceLevelLoad > 1) {
            canmove = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            FindObjectOfType<gamemanager>().EndGame(); }        
    }

}