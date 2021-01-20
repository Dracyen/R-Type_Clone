using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    void Death(float Damage = 0.075f);
}