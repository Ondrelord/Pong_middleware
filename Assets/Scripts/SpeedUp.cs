using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpPower", menuName = "Power/SpeedUpPower")]
public class SpeedUp : Power
{
    public override void PickUp()
    {
        GameObject.Find("Arena").GetComponent<ArenaManager>().ChangeSpeed(2);
    }
}
