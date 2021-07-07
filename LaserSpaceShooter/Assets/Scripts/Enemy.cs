using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    [Header("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] GameObject destroyPartcl;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip explosionEnemySFX;
    [SerializeField] [Range(0, 1)] float explosionSoundStraingth = 0.7f;
    [SerializeField] int scoreValue = 100;

    [Header("Projectile")]
    [SerializeField] float minTimeBrtweenShots = 2f;
    [SerializeField] float maxTimeBrtweenShots = 4f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] AudioClip shootEnemySFX;
    [SerializeField] [Range(0, 1)] float shootSoundStraingth = 0.7f;



    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBrtweenShots, maxTimeBrtweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        if (laserPrefab != null)
        {
            CountDownAndShoot();
        }
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fier();
            shotCounter = Random.Range(minTimeBrtweenShots, maxTimeBrtweenShots);
        }
    }

    private void Fier()
    {
        GameObject laser  = Instantiate (laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootEnemySFX, Camera.main.transform.position, shootSoundStraingth);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDiller damageDiller = other.gameObject.GetComponent<DamageDiller>();
        if (!damageDiller) { return; } // допольнительная защита, если нет  компонента damageDiller то выйдти из метода
        ProcessHit(damageDiller); // (damageDiller) мы сообщаем с каким аргументом мы запускаем данный метод
    }

    private void ProcessHit(DamageDiller damageDiller) // если ктото запрашивает этот метод то он должен предствыить переменную damageDiller типа DamageDiller
    {
        health -= damageDiller.GetDamage();
        damageDiller.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(gameObject);
        GameObject destroyVFX = Instantiate(destroyPartcl, transform.position, Quaternion.identity) as GameObject;
        Destroy(destroyVFX, durationOfExplosion);
        AudioSource.PlayClipAtPoint(explosionEnemySFX, Camera.main.transform.position, explosionSoundStraingth);
    }
}
