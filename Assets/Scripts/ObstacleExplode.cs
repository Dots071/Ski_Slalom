using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleExplode : Obstacle
{

    public override void PlayerHit()
    {
        base.PlayerHit();
        Destroy(gameObject);
    }

}
