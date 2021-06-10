using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip crashSound;
    // [SerializeField] private AudioListener listener;

    private void Start()
    {
        Events.Instance.playerGotHit.AddListener(HandlePlayerGotHit);
    }

    private void HandlePlayerGotHit()
    {
        source.PlayOneShot(crashSound);
    }
}
