using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrapObject : TrapObject
{
    [SerializeField] private float flyingSpeed;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + flyingSpeed * Time.deltaTime * (transform.forward.x < 0 ? 1 : -1), transform.position.y, transform.position.z);
    }
}
