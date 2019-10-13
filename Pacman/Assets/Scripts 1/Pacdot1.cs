using UnityEngine;

public class Pacdot1 : MonoBehaviour
{
    public bool isSuperPacdot = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacman1")
        {
            if (isSuperPacdot)
            {
                //tell manager i am super pacdot and have been eatten
                //make pacman change to super pacman and it can eat ghost
                GameManager1.Instance.OnEatPacdot(gameObject);
                GameManager1.Instance.OnEatSuperPacdot();
                Destroy(gameObject);
            }
            else
            {
                //if not destory
                GameManager1.Instance.OnEatPacdot(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
