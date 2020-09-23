using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathPower", menuName = "Power/DeathPower")]
public class DeathPowerDown : Power
{
    public override void PickUp()
    {
        GameObject.Find("Arena").GetComponent<ArenaManager>().LostLife();
    }
}
