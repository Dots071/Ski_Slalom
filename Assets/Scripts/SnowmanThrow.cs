using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour
{
    public List<GameObject> snowBallsPool;
    public GameObject snowBall;
    private GameObject target;
    public float snowBallAmount;
    private int frameInterval = 5;

    public float throwDistance;
    public int throwSpeed;
    private bool justThown = false;

    private Vector3 throwY = new Vector3(0, 0, -0.66f);

    private void Start()
    {
        snowBallsPool = new List<GameObject>();
        GameObject temp;

        target = GameObject.Find("Player");

        for (int i=0; i < snowBallAmount; i++)
        {
            temp = Instantiate(snowBall);
            temp.SetActive(false);
            snowBallsPool.Add(temp);

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManger.Instance.CurrentGameState == GameManger.GameState.RUNNING)
        {
            if (Time.frameCount % frameInterval == 0)
            {
                float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

                if (distanceToTarget < throwDistance && justThown == false)
                {
                    justThown = true;

                    GameObject tempSnowBall = GetPoolSnowBall();

                    if (tempSnowBall)
                    {
                        tempSnowBall.SetActive(true);
                        tempSnowBall.transform.position = transform.position;
                        Rigidbody tempRb = tempSnowBall.GetComponent<Rigidbody>();

                        Vector3 targetDirection = Vector3.Normalize(target.transform.position - transform.position);

                        //Add a small throw angle
                        targetDirection += throwY;

                        tempRb.AddForce(targetDirection * throwSpeed);
                        StartCoroutine(ThrowOver(tempSnowBall));
                    }
                }
            }
        }
    }

    // Returns a snowball object if available, else returns null.
    private GameObject GetPoolSnowBall()
    {
        for (int i = 0; i < snowBallsPool.Count; i++)
        {
            if (!snowBallsPool[i].activeInHierarchy)
            {
                return snowBallsPool[i];
            }
        }

        return null;
    }

    IEnumerator ThrowOver(GameObject tempSnowBall)
    {
        yield return new WaitForSeconds(0.5f);
        justThown = false;
        tempSnowBall.SetActive(false);

    }
}
