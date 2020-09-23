using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MultiplyBallPower", menuName = "Power/MultiplyBallPower")]
public class MultiplyBall : Power
{
    public override void PickUp()
    {
        FindObjectOfType<ArenaManager>().CreateBall(true);
    }
}
