using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField] private GameObject hazardObject;
    [SerializeField] private float minTimeBetweenTriggers;
    [SerializeField] private float maxTimeBetweenTriggers;
    [SerializeField] private Transform spawnTransform;

    private void Start()
    {
        StartCoroutine(triggerWait());
    }

    private IEnumerator triggerWait()
    {
        yield return new WaitForSeconds(Random.Range(minTimeBetweenTriggers, maxTimeBetweenTriggers));
        Instantiate(hazardObject, spawnTransform);
        StartCoroutine(triggerWait());
    }
}
