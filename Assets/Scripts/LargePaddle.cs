using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LargePaddlePower", menuName = "Power/LargePaddlePower")]
public class LargePaddle : Power
{
    public override void PickUp()
    {
        GameObject.Find("Paddle").transform.localScale *= 2;
    }
}
