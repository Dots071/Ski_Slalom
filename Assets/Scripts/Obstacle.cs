using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHit();
        }
    }

    public virtual void PlayerHit()
    {
        Events.Instance.playerGotHit.Invoke();
    }
}
