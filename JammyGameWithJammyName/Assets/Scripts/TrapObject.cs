using UnityEngine;

public class TrapObject : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private float deathTimer;
    private void Start()
    {
        Destroy(gameObject, deathTimer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterBase character = collision.GetComponent<CharacterBase>();
        if(character != null)
        {
            character.TakeDamage(damageAmount);
            Destroy(gameObject);
        }else if(collision.tag == "BoundryBox")
        {
            Destroy(gameObject);
        }
    }
}