using UnityEngine;

public class RangerDanger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player" ))
        {
            Rotation.Instance.isRotatingClockwise = !Rotation.Instance.isRotatingClockwise;
            Rotation.Instance.Death();
        }
    }
}
