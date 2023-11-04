using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.SpriteAssetUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
   [SerializeField] private MeshRenderer fieldMeshRenderer;
   [SerializeField] private MeshRenderer bombMeshRenderer;
   [SerializeField] private GameObject timerObject;
   [SerializeField] private GameObject explosion;
   public TextMeshProUGUI timerText;
   public float bombShakePower = 0.1f;

   [Space(15)]
    public int bombTimer = 10;       // Время таймера в секундах

    private float timeRemaining;    // Оставшееся время таймера
    private bool isTimerRunning;    // Флаг для определения, идет ли таймер
    
    void Start()
    {
        fieldMeshRenderer.enabled = true;
        timeRemaining = bombTimer;
        isTimerRunning = true;
        UpdateTimerText();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                isTimerRunning = false;
                OnTimerComplete();
            }

            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        }
    }

    private void OnTimerComplete()
    {
        Explode();
        Debug.Log("Таймер завершен! Время вышло!");
    }

    public void Explode()
    {
        bombMeshRenderer.enabled = false;
        fieldMeshRenderer.enabled = false;
        timerObject.SetActive(false);
        explosion.SetActive(true);
        GameManager.Instance.CameraShake(bombShakePower);
    }

    public void ReduceTime(int seconds)
    {
        if (isTimerRunning)
        {
            timeRemaining -= seconds;
            if (timeRemaining < 0f)
            {
                timeRemaining = 0f;
            }
            UpdateTimerText();
        }
    }
}
