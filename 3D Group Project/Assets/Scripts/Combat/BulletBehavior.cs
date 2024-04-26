using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [Header("Bullet Variants")]
    [SerializeField] private bool explosive = false;
    [SerializeField] private bool delayedExplosive = false;

    [Header("Variant Settings")]
    [SerializeField] public GameObject explosionCollider;
    [SerializeField] private int explosionDelay = 5;
    [SerializeField] private float explosionRadius = 5;

    [Header("General Settings")]
    [SerializeField] private bool despawnOnCollision = true;
    [SerializeField] private int bulletDespawnTimer = 1;

    private Rigidbody _rb;
    public bool friendly;
    public bool passiveFriendly = false;
    public bool enemyFriendly = false;
    public string shooter;
    public string weaponName;
    public int damage;
    public int explosionDamage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if(!despawnOnCollision)
        {
            Destroy(gameObject, bulletDespawnTimer);
        }
        else { Destroy(gameObject, 10); }
    }
    public void Fire(float speed, Vector3 direction)
    {
        _rb.velocity = direction * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(explosive)
        {
            if(delayedExplosive)
            {
                StartCoroutine(DelayedExplosionCountdown(explosionDelay));
            }
            else
            {
                GameObject explosion = Instantiate(explosionCollider, gameObject.transform.position, Quaternion.identity);
                explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
                explosion.GetComponent<ExplosionBehavior>().shooter = shooter;
                explosion.GetComponent<ExplosionBehavior>().damage = explosionDamage;
                Destroy(gameObject);
                Destroy(explosion, 2.5f);
            }
        }
        if(despawnOnCollision && !explosive)
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator DelayedExplosionCountdown(int delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject explosion = Instantiate(explosionCollider, gameObject.transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
        explosion.GetComponent<ExplosionBehavior>().shooter = shooter;
        explosion.GetComponent<ExplosionBehavior>().damage = explosionDamage;
        Destroy(gameObject);
        Destroy(explosion, 2.5f);
        StopAllCoroutines();
    }

}