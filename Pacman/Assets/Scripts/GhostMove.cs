using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMove : MonoBehaviour
{
    //get these wayPoints
    public GameObject[] wayPointsGos;
    public float speed = 0.2f;
    //store in the List
    private List<Vector3> wayPoints = new List<Vector3>();
    //Currently on the way to another waypoint
    private int index = 0;
    private Vector3 startPos;

    private void Start()
    {
        //Ghost will move up 3 points when game start
        startPos = transform.position + new Vector3(0, 3, 0);
        LoadAPath(wayPointsGos[GameManager.Instance.usingIndex[GetComponent<SpriteRenderer>().sortingOrder - 2]]);
    }

    private void FixedUpdate()
    {
        //Start another position after reaching a position
        if (transform.position != wayPoints[index])
        {
            //Get the coordinates to move to the next position
            Vector2 temp = Vector2.MoveTowards(transform.position, wayPoints[index], speed);
            //Set the position of the object with a rigidbody
            GetComponent<Rigidbody2D>().MovePosition(temp);
        }
        else
        {
            //Cycle through the next wayPoint
            index++;
            if (index >= wayPoints.Count)
            {
                index = 0;
                LoadAPath(wayPointsGos[Random.Range(0, wayPointsGos.Length)]);
            }
        }
        //Get the direction of movement
        Vector2 dir = wayPoints[index] - transform.position;
        //Set the obtained moving direction to the animation
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    private void LoadAPath(GameObject go)
    {
        //Child objects are stored in the collection
        wayPoints.Clear();
        foreach (Transform t in go.transform)
        {
            wayPoints.Add(t.position);
        }
        //Each ghost will move up 3 points 
        wayPoints.Insert(0, startPos);
        wayPoints.Add(startPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacman")
        {
                collision.gameObject.SetActive(false);
                GameManager.Instance.gamePanel.SetActive(false);//Hidden score panel
                Instantiate(GameManager.Instance.gameoverPrefab);
                Invoke("ReStart", 3f);//restart after 3s
        }
    }

    private void ReStart()
    {
        SceneManager.LoadScene(0);
    }
}
