using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrapObject : TrapObject
{
    [SerializeField] private float flyingSpeed;
    private float currentSpeed;
    private bool hasStopped;
    [SerializeField] private Transform stopPos;

    private void Start()
    {
        currentSpeed = flyingSpeed;
    }

    private void Update()
    {
        transform.position = new Vector3(currentSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        if(transform.position.x <= stopPos.position.x && !hasStopped)
        {
            WaitForLaunch();
        }
    }

    private IEnumerator WaitForLaunch()
    {
        hasStopped = true;
        currentSpeed = 0f;
        yield return new WaitForSeconds(Random.Range(3f, 3.3f));
        currentSpeed = flyingSpeed;
    }
}
