using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticule : MonoBehaviour
{
    [SerializeField] float _timeToDestroy = 2.0f;

    void Start()
    {
        Destroy(this.gameObject, _timeToDestroy);
    }
}
