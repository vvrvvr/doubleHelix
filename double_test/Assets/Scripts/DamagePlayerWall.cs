using UnityEngine;

public class DamagePlayerWall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, если объект с тэгом "Player" входит в триггер
        if (collision.gameObject.CompareTag("Player" ))
        {
            GameManager.Instance.ReduceLives();
        }
    }
}
