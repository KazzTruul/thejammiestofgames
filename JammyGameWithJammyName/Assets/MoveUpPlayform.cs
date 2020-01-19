using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpPlayform : MonoBehaviour
{
    [SerializeField] GameObject standingPlatformReference;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            standingPlatformReference.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            standingPlatformReference.SetActive(true);
        }
    }
}
