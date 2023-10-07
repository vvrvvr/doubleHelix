using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovingPlatform : MonoBehaviour
{
    public bool isCarryPlayer = false;
    public bool isGrounded = false;
    private PlayerPointer _playerPointer;
    private void OnTriggerStay(Collider other)
    {
        if (!isCarryPlayer && other.CompareTag("PlayerPoint"))
        {
            _playerPointer = other.gameObject.GetComponent<PlayerPointer>();
            if ( _playerPointer.isGrounded)
            {
                Rotation.Instance.currentCenter.transform.SetParent(this.transform);
                isCarryPlayer = true;
                Debug.Log("on platform");
            }
        }
    }

    private void Update()
    {
        if (isCarryPlayer && !_playerPointer.isGrounded)
        {
            isCarryPlayer = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if (isCarryPlayer && isGrounded == false)
        // {
        //     Rotation.Instance.currentCenter.transform.SetParent(null);
        //     isCarryPlayer = false;
        //     Debug.Log("exit platform 2");
        // }
    }
}
