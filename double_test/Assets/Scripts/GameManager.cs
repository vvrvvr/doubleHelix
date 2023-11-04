using System;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject StartPosition;
    [SerializeField] private GameObject StartDialogue;
    [SerializeField] private GameObject EndDialogue;
    
    
    private static GameManager _instance;
    public int lives = 3;
    public Vector3 currentCheckpoint;
    private CinemachineImpulseSource impulseSource;
    
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("YourScriptSingleton");
                    _instance = singletonObject.AddComponent<GameManager>();
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
    
    
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        livesText.text = "Lives: " + lives;
        currentCheckpoint = Vector3.zero;
        StartPosition.GetComponent<Renderer>().enabled = false;
        StartLevel();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)) // удалить потом
        {
            RestartLevelFromCheckpoint();
            //EndLevel();
        }
    }
    
    public void ReduceLives(int damage, float impulsePower)
    {
        impulseSource.GenerateImpulse(impulsePower);
        lives -= damage;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            Death();
        }
    }

    public void CameraShake(float impulsePower)
    {
        impulseSource.GenerateImpulse(impulsePower);
    }

    public void IncreaseLives()
    {
        lives += 1;
        livesText.text = "Lives: " + lives;
    }

    public void Death()
    {
        lives = 0;
        livesText.text = "Lives: " + lives;
        Debug.Log("death");
        PlayerController.Instance.Death();
    }

    public void RestartLevel()
    {
        lives = 3;
        livesText.text = "Lives: " + lives;
        currentCheckpoint = Vector3.zero;
        PlayerController.Instance.SpawnPlayer(StartPosition.transform.position);
    }

    public void StartLevel()
    {
        if (StartDialogue.activeSelf)
        {
            StartDialogue.GetComponent<SceneStartDialogue>().StartDialog();
        }
        else
        {
            SpawnPlayerAtStart();
        }
    }

    public void EndLevel()
    {
        if (EndDialogue.activeSelf)
        {
            Death();
            EndDialogue.GetComponent<SceneStartDialogue>().StartDialog();
        }
        else
        {
            Death();
        }
    }

    public void SpawnPlayerAtStart()
    {
        PlayerController.Instance.SpawnPlayer(StartPosition.transform.position);
    }

    public void RestartLevelFromCheckpoint()
    {
        if (currentCheckpoint != Vector3.zero)
        {
            lives = 3;
            livesText.text = "Lives: " + lives;
            PlayerController.Instance.SpawnPlayer(currentCheckpoint);
        }
        else
            RestartLevel();
    }
    
    public void SetCheckpoint(Vector3 position)
    {
        currentCheckpoint = position;
    }
}
