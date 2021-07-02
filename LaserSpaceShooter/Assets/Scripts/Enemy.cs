using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDiller damageDiller = other.gameObject.GetComponent<DamageDiller>();
        ProcessHit(damageDiller); // (damageDiller) мы сообщаем с каким аргументом мы запускаем данный метод
    }

    private void ProcessHit(DamageDiller damageDiller) // если ктото запрашивает этот метод то он должен предствыить переменную damageDiller типа DamageDiller
    {
        health -= damageDiller.GetDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
