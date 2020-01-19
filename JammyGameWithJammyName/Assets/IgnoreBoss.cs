using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBoss : MonoBehaviour
{
    [SerializeField] Collider2D boss;
    private void Update()
    {
        Physics2D.IgnoreCollision(boss, GetComponent<Collider2D>(), true);
    }
}
