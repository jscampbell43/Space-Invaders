using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootingOffset;
    private float accumulatedTime = 0f;
    private float totalTime = 0f;

   
    void Update()
    {
        // Fire at a random time interval
        accumulatedTime += Time.deltaTime;
        if (accumulatedTime > Random.Range(0f, 10000f))
        {
            totalTime += 1f;
            accumulatedTime = 0f;
            GameObject shot = Instantiate(bullet, shootingOffset.position, Quaternion.identity);
            Physics2D.IgnoreCollision(shot.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Debug.Log("Bang!");

            Destroy(shot, 10f);
        }
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
      Debug.Log("Collided with: " + collision.gameObject.name);
      if (collision.gameObject.name == "RightWall" || collision.gameObject.name == "LeftWall")
      {
          if (wallCollideEvent != null)
          {
              // Trigger Delegate for Wall Collision
              Debug.Log("Wall Collide Delegate Called");
              wallCollideEvent();
          }
      }
      else if (collision.gameObject.name == "Bullet(Clone)")
      {
          if (this.gameObject.name == "Enemy_10Pts(Clone)")
          {
              if (enemy_10_DestroyedEvent != null)
              {
                  Debug.Log("Enemy 10 Destroyed Delegate Called");
                  enemy_10_DestroyedEvent();
              }
          }
          else if (this.gameObject.name == "Enemy_20Pts(Clone)")
          {
              if (enemy_20_DestroyedEvent != null)
              {
                  Debug.Log("Enemy 20 Destroyed Delegate Called");
                  enemy_20_DestroyedEvent();
              }
          }
          else if (this.gameObject.name == "Enemy_30Pts(Clone)")
          {
              if (enemy_30_DestroyedEvent != null)
              {
                  Debug.Log("Enemy 30 Destroyed Delegate Called");
                  enemy_30_DestroyedEvent();
              }
          }
          else if (this.gameObject.name == "Enemy_Mystery(Clone)")
          {
              if (enemy_mystery_DestroyedEvent != null)
              {
                  Debug.Log("Enemy Mystery Destroyed Delegate Called");
                  enemy_mystery_DestroyedEvent();
              }
          }
          Destroy(this.gameObject);
          Destroy(collision.gameObject);
      }
    }
    
    //Delegate for alerting EnemyMovement to change direction
    public delegate void WallCollideDelegate();

    public event WallCollideDelegate wallCollideEvent;

    // Delegate for alerting EnemyMovement when a single enemy is destoryed
    public delegate void Enemy_10_DestroyedDelegate();

    public event Enemy_10_DestroyedDelegate enemy_10_DestroyedEvent;
    
    public delegate void Enemy_20_DestroyedDelegate();

    public event Enemy_20_DestroyedDelegate enemy_20_DestroyedEvent;
    
    public delegate void Enemy_30_DestroyedDelegate();

    public event Enemy_30_DestroyedDelegate enemy_30_DestroyedEvent;
    
    public delegate void Enemy_mystery_DestroyedDelegate();

    public event Enemy_mystery_DestroyedDelegate enemy_mystery_DestroyedEvent;
}
