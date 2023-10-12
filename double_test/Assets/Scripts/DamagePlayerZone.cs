using Cinemachine;
using UnityEngine;

public class DamagePlayerZone : MonoBehaviour, IInteractable
{
    private CinemachineImpulseSource impulseSource;
    public InteractionOption InteractionOption { get; set; }
    public void Interact()
    {
        GameManager.Instance.ReduceLives();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        if(impulseSource !=null)
            impulseSource.GenerateImpulse();
        Debug.Log("Damage zone");
    }
}
