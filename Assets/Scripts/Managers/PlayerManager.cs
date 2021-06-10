using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{ 
    private bool onGround;
    private bool takingHit = false;

    [SerializeField] private bool isMoving;

    [SerializeField] private float turnSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;


    [SerializeField] private float turnAccelaration;
    [SerializeField] private float turnDeccelaration;

    [SerializeField] private float obstaclePushBack;
    [SerializeField] private float hitRecoveryTime;

    [SerializeField] private KeyCode left, right;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;
    private float playerRotationAngle;
    private Rigidbody playerRB;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Events.Instance.playerGotHit.AddListener(HandlePlayerGotHit);

    }

    private void Update()
    {
        if (GameManger.Instance.CurrentGameState == GameManger.GameState.RUNNING)
        {
            onGround = Physics.Linecast(transform.position, groundChecker.position, groundLayer);

            if (onGround && isMoving && !takingHit)
            {
                if (Input.GetKey(left))
                {
                    TurnLeft();
                }
                if (Input.GetKey(right))
                {
                    TurnRight();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameManger.Instance.CurrentGameState == GameManger.GameState.RUNNING)
        {
            ControllSpeed();

            if (isMoving && !takingHit)
            {
                //Accelarates or deccelarates the player's speed according to the turn angle.

                playerRotationAngle = Mathf.Abs(180 - gameObject.transform.rotation.eulerAngles.y);
                playerSpeed += Remap(0, 90, turnAccelaration, -turnDeccelaration, playerRotationAngle);

                //playerRB.AddRelativeForce(Vector3.forward * playerSpeed * Time.deltaTime);

                Vector3 velocity = transform.forward * playerSpeed * Time.deltaTime;
                velocity.y = playerRB.velocity.y;
                playerRB.velocity = velocity;
            }
        }
    }

    private void HandlePlayerGotHit()
    {
        StartCoroutine(TakeHit());
    }

    IEnumerator TakeHit()
    {
        takingHit = true;
        playerRB.AddRelativeForce(Vector3.back * obstaclePushBack * Time.deltaTime, ForceMode.Impulse);
        Debug.Log("Player got hit");
        yield return new WaitForSeconds(hitRecoveryTime);
        takingHit = false;
        Debug.Log("Player got hit 5 seconds ago");
    }

    // Makes sure the speed is between max and min values   
    private void ControllSpeed()
    {
        if (playerSpeed > maxSpeed)
            playerSpeed = maxSpeed;

        if (playerSpeed < minSpeed)
            playerSpeed = minSpeed;
    }

    private void TurnLeft()
    {
        if (transform.eulerAngles.y < 269)
        transform.Rotate(new Vector3(0, turnSpeed, 0) * Time.deltaTime, Space.Self);
    }

    private void TurnRight()
    {
        if (transform.eulerAngles.y > 91)
        gameObject.transform.Rotate(new Vector3(0, -turnSpeed, 0) * Time.deltaTime, Space.Self);
    }

    // remaps a number from a given range into a new range
    private float Remap(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
        return (NewValue);
    }
}
