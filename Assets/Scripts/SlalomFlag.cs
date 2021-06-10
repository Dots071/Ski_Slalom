using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlalomFlag : MonoBehaviour
{
    [SerializeField] private float timePenalty;
    [SerializeField] private bool passOnLeft;
    [SerializeField] private Material missedMaterial;
    [SerializeField] private Material passedMaterial;


    private MeshRenderer mRenderer;

    private void Start()
    {
        mRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float distanceFlagToPlayer = gameObject.transform.position.x - other.transform.position.x;

            if (isFlagChecked(distanceFlagToPlayer))
            {
                mRenderer.material = passedMaterial;
            }
            else
            {
                mRenderer.material = missedMaterial;
                Events.Instance.missedFlag.Invoke();
            }
        }
    }

    private bool isFlagChecked(float distance)
    {
        if (passOnLeft)
        {
            return distance > 0 ? true : false;
        }
        else
        {
            return distance < 0 ? true : false;
        }
    }
}
