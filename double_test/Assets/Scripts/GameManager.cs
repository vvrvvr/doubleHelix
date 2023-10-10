using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject StartPosition;
    
    private static GameManager _instance;
    private int lives = 3;
    private Transform currentCheckpoint;
    
    
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
        livesText.text = "Lives: " + lives;
        currentCheckpoint = null;
        StartPosition.GetComponent<Renderer>().enabled = false;
        PlayerController.Instance.SpawnPlayer(StartPosition.transform.position);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) // удалить потом
        {
            PlayerController.Instance.SpawnPlayer(StartPosition.transform.position);
        }
    }

    public void ReduceLives()
    {
        lives -= 1;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            Death();
        }
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
        currentCheckpoint = null;
        PlayerController.Instance.SpawnPlayer(StartPosition.transform.position);
    }

    public void RestartLevelFromCheckpoint()
    {
        if (currentCheckpoint != null)
            PlayerController.Instance.SpawnPlayer(currentCheckpoint.position);
        else
            RestartLevel();
    }
    
    public void Checkpoint(Transform position)
    {
        currentCheckpoint = position;
    }
}
