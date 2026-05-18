using UnityEngine;

public class LineCollision : MonoBehaviour
{
    public bool isColliding;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}
