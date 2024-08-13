using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public event Action<float> GotDamage;

    public virtual void GetDamage(float value)
    {
        GotDamage?.Invoke(value);
    }
}
