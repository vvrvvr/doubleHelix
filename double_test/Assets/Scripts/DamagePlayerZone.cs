using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerZone : MonoBehaviour, IInteractable
{
    public InteractionOption InteractionOption { get; set; }
    public void Interact()
    {
        GameManager.Instance.ReduceLives();
        Debug.Log("Damage zone");
    }
}
