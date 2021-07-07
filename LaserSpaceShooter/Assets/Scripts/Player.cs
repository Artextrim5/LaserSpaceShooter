using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configurations parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip dethPlayerSFX;
    [SerializeField] [Range(0, 1)] float explosionSoundStraingth = 0.7f;

    [Header("Projectile")]
    [SerializeField] GameObject liserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFierPeriod = 0.2f;
    [SerializeField] AudioClip shootPlayerSFX;
    [SerializeField] [Range(0, 1)] float shootSoundStraingth = 0.25f;

    Coroutine fieringCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fier();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fier()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fieringCoroutine = StartCoroutine (FireContinously());            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fieringCoroutine);
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject laser = Instantiate(liserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootPlayerSFX, Camera.main.transform.position, shootSoundStraingth);
            yield return new WaitForSeconds(projectileFierPeriod);
        }
    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDiller damageDiller = other.gameObject.GetComponent<DamageDiller>();
        if (!damageDiller) { return; }
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
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(dethPlayerSFX, Camera.main.transform.position, explosionSoundStraingth);
    }

    public int GetHealth()
    {
        return health;
    }

}
