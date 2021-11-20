using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamagable
{
    public float maxHealth;
    private float health;
    public Slider healthSlider;
    private bool running;
    private bool waiting;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(idle());
        if (waiting)
        {
            transform.position += direction / 100;
        }
    }

    private IEnumerator idle()
    {
        if (!running)
        {
            running = true;
            waiting = false;
            
            direction = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)).normalized;
            
            yield return new WaitForSeconds(2f);
            waiting = true;
            yield return new WaitForSeconds(2f);
            running = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health / 100;
        if (health <= 0)
        {
            Debug.Log("Died");
        }
    }
}
