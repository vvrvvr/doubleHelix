using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Quest
{
    
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, если объект с тэгом "Player" входит в триггер
        if (other.CompareTag("Player" ))
        {
           
            Debug.Log("trigger да");
            ConditionComplete();

        }
    }
}
