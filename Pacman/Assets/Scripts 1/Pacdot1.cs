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
                GameManager1.Instance.OnEatPacdot(gameObject);
                GameManager1.Instance.OnEatSuperPacdot();
                Destroy(gameObject);
            }
            else
            {
                GameManager1.Instance.OnEatPacdot(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
