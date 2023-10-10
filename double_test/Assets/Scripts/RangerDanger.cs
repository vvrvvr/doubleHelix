using UnityEngine;

public class RangerDanger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player" ))
        {
            PlayerController.Instance.isRotatingClockwise = !PlayerController.Instance.isRotatingClockwise;
            PlayerController.Instance.Death();
        }
    }
}
