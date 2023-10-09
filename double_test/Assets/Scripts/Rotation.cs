using Unity.VisualScripting;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject center1;
    public GameObject center2;
    public GameObject body;
    public GameObject red;
    public GameObject blue;
    private Transform position1;
    private Transform position2;
    public Rigidbody rb1;
    public Rigidbody rb2;
    public float rotationSpeed = 100.0f;
    public float rotationSpeedMax = 200.0f; // Максимальная скорость вращения
    public float accelerationTime = 1.0f;
    public float deaccelerationTime = 0.5f;// Время ускорения до максимальной скорости

    private Rigidbody rb;
    public bool isRotatingClockwise = true;
    private float normalRotationSpeed; // Нормальная скорость вращения
    private float currentRotationSpeed; // Текущая скорость вращения
    private float accelerationTimer; // Таймер ускорения

    private Rigidbody currentRb;
    //public GameObject currentCenter = null;
    public GameObject currentCenter = null;
    private float interactionDistance = 2f;

    private static Rotation _instance;
    public static Rotation Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Rotation>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("YourScriptSingleton");
                    _instance = singletonObject.AddComponent<Rotation>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        position1 = red.transform;
        position2 = blue.transform;
        normalRotationSpeed = rotationSpeed;
        currentRb = rb1;
        center1.transform.SetParent(null);
        center1.transform.position = new Vector3(position1.position.x,0f,position1.position.z );
        center1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        body.transform.SetParent(center1.transform);
        center2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        center2.transform.position = new Vector3(position2.position.x,0f,position2.position.z );
        center2.transform.SetParent(null);
        currentRotationSpeed = normalRotationSpeed;
        red.GetComponent<PlayerPointer>().isGrounded = true;
        currentCenter = center1;
    }

    private void Update()
    {
        currentRb.velocity = Vector3.zero;
        currentRb.transform.rotation = Quaternion.Euler(0f, currentRb.transform.rotation.eulerAngles.y, 0f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRotatingClockwise = !isRotatingClockwise;
            if (currentCenter == center1)
                currentCenter = center2;
            else
                currentCenter = center1;

            if (currentCenter == center1)
            {   
                blue.GetComponent<PlayerPointer>().isGrounded = false;
                red.GetComponent<PlayerPointer>().isGrounded = true;
                //center1.transform.SetParent(null);
                center1.transform.position = new Vector3(position1.position.x,0f,position1.position.z );
                center1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                body.transform.SetParent(center1.transform);
                center2.transform.SetParent(null);
                currentRb = rb1;
            }
            else
            {
                red.GetComponent<PlayerPointer>().isGrounded = false;
                blue.GetComponent<PlayerPointer>().isGrounded = true;
                //center2.transform.SetParent(null);
                center2.transform.position = new Vector3(position2.position.x,0f,position2.position.z );
                center2.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                body.transform.SetParent(center2.transform);
                center1.transform.SetParent(null);
                currentRb = rb2;
            }

            currentRotationSpeed = normalRotationSpeed;
            accelerationTimer = 0f;
            
            InteractWithFloor();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (accelerationTimer < accelerationTime)
            {
                accelerationTimer += Time.deltaTime;
                currentRotationSpeed = Mathf.Lerp(normalRotationSpeed, rotationSpeedMax, accelerationTimer / accelerationTime);
            }
        }
        else
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, normalRotationSpeed, Time.deltaTime / deaccelerationTime);
        }
    }

    private void FixedUpdate()
    {
        if (isRotatingClockwise)
        {
            currentRb.angularVelocity = Vector3.up * currentRotationSpeed * Time.fixedDeltaTime;
        }
        else
        {
            currentRb.angularVelocity = -Vector3.up * currentRotationSpeed * Time.fixedDeltaTime;
        }
    }

    public void Death()
    {
        Debug.Log("death");
        // Destroy(currentCenter); 
        // Destroy(this);
    }

    private void InteractWithFloor()
    {
        if (currentCenter != null)
        {
            RaycastHit hit;
            Ray ray = new Ray(currentCenter.transform.position, Vector3.down);
            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.gameObject != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    //Здесь будут интерфейсы
                }
            }
        }
    }
    
}
