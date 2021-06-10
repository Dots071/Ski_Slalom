using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceFinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Events.Instance.RaceFinished.Invoke();
    }
}
