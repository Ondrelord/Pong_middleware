using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power", menuName = "Power/Power")]
public class Power : ScriptableObject
{
    public Color powerColor;

    public virtual void PickUp()
    {
    }
}
