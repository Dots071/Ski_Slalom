using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlag : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        Events.Instance.RaceStarted.Invoke();
    }

}
