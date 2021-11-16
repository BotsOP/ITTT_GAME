using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipHitbox : MonoBehaviour
{
    public float damage;
    
    private bool isHittingEnemy;
    private float lastTime;
    private float delay = 0.1f;

    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - delay > lastTime && isHittingEnemy)
        {
            lastTime = Time.time;
            enemy.GetComponent<IDamagable>().TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            isHittingEnemy = true;
            enemy = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            isHittingEnemy = false;
            enemy = null;
        }
    }
}
