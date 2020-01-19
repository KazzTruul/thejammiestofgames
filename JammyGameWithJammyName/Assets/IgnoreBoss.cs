using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBoss : MonoBehaviour
{
    [SerializeField] GameObject boss;
    private void Update()
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), boss.GetComponent<Collider2D>());
    }
}
