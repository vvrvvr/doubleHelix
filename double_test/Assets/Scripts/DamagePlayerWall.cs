using UnityEngine;

public class DamagePlayerWall : MonoBehaviour
{
    [SerializeField] private float bounceSpeedMultiplier = 1f;
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, если объект с тэгом "Player" входит в триггер
        if (collision.gameObject.CompareTag("Player" ))
        {
            GameManager.Instance.ReduceLives();
            PlayerController.Instance.currentRotationSpeed *= bounceSpeedMultiplier;
        }
    }
}
