using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//LivingEntity
public class LivingEntity : MonoBehaviour
{
    public enum State
    {
        Idle,
        Run,
        Attack,
        Dead,
        Stay,
        IsHitting
    }
    private State state;

}
