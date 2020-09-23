using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmallPaddlePower", menuName = "Power/SmallPaddlePower")]
public class SmallPaddle : Power
{
    public override void PickUp()
    {
        GameObject.Find("Paddle").transform.localScale /= 2;
    }
}
