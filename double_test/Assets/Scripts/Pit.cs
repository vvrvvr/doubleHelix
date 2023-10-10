using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour, IInteractable
{
   public void Interact()
   {
      PlayerController.Instance.Death();
   }
}
