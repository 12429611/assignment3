using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;

    void Start()
    {
        dest = transform.position;   
    }

     void FixedUpdate()
    {
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(temp);
        if((Vector2)transform.position == dest)
        {
            if ((Vector2)transform.position == dest)
            {
                if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Valid(Vector2.up))
                {
                    dest = (Vector2)transform.position + Vector2.up;
                }
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Valid(Vector2.up))
                {
                    dest = (Vector2)transform.position + Vector2.right;
                }
                if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Valid(Vector2.up))
                {
                    dest = (Vector2)transform.position + Vector2.down;
                }
                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Valid(Vector2.up))
                {
                    dest = (Vector2)transform.position + Vector2.left;
                }
                Vector2 dir = dest - (Vector2)transform.position;
                GetComponent<Animator>().SetFloat("DirX", dir.x);
                GetComponent<Animator>().SetFloat("DirY", dir.y);
            }

        }
     }
    private bool Valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }
}
