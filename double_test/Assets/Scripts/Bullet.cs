using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    public float lifetime = 5.0f; 

    public void SetDirection(Vector3 dir, float bulletSpeed)
    {
        direction = dir.normalized;
        speed = bulletSpeed;
    }

    void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.fixedDeltaTime);
        lifetime -= Time.fixedDeltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Turret"))
        {
            Destroy(gameObject);
           // GameManager.Instance.ReduceLives(1, 1);
        }
    }
}