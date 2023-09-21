using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public enum Limbs {Head,Body,Arms }
    public Limbs limbType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float value)
    {
        healthPoint -= value;
        if(healthPoint <= 0)
        {
            Destroy(gameObject);
            gameManager.score += scorePoint;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player != null)
            {
                gameManager.score -= scorePoint / 2;
            }
        }
        catch
        {

        }
    }
}
