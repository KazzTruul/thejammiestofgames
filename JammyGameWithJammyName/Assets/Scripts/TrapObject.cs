using UnityEngine;

public class TrapObject : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    [SerializeField] private float deathTimer;
    private void Start()
    {
        Destroy(gameObject, deathTimer);

        if(gameObject.tag == "Boulder")
        {
            transform.position = new Vector3(Random.Range(-17f, 17f), transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterBase character = collision.GetComponent<CharacterBase>();
        if(character != null)
        {
            character.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterBase character = collision.collider.GetComponent<CharacterBase>();
        if (character != null)
        {
            character.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
        if (collision.collider.tag == "BoundryBox" || collision.collider.tag == "Platform" || collision.collider.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}