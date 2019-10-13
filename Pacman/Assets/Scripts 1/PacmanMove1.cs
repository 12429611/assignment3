using UnityEngine;

public class PacmanMove1 : MonoBehaviour
{
    //pacman speed
    public float speed = 0.35f;
    //pacman next position of movement
    private Vector2 dest = Vector2.zero;

    private void Start()
    {
        //make sure pacman stop at start the game
        dest = transform.position;
    }

    private void FixedUpdate()
    {
        //The value gets the next movement coordinate to move to the dest position
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);
        //Set the position of the object by the rigidbody
        GetComponent<Rigidbody2D>().MovePosition(temp);
        if ((Vector2)transform.position == dest)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Valid(Vector2.up))
            {
                dest = (Vector2)transform.position + Vector2.up;
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Valid(Vector2.down))
            {
                dest = (Vector2)transform.position + Vector2.down;
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Valid(Vector2.left))
            {
                dest = (Vector2)transform.position + Vector2.left;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Valid(Vector2.right))
            {
                dest = (Vector2)transform.position + Vector2.right;
            }
            //Get the direction of movement
            Vector2 dir = dest - (Vector2)transform.position;
            //Set the obtained moving direction to the animation
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }

    //Check if the location to go is reachable
    private bool Valid(Vector2 dir)
    {
        //Record the current location
        Vector2 pos = transform.position;
        //Transmit a ray from the location to be reached to the current location and store the ray information
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        //Return whether this ray hits the collider on Pac-Man himself
        return (hit.collider == GetComponent<Collider2D>());
    }
}
