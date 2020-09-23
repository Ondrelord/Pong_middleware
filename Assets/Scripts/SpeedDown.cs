using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedDownPower", menuName = "Power/SpeedDownPower")]
public class SpeedDown : Power
{
    public override void PickUp()
    {
        GameObject.Find("Arena").GetComponent<ArenaManager>().ChangeSpeed(0.5f);
    }
}
