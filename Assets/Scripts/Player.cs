using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameObject bullet;

  public Transform shootingOffset;
  
  public float movementPerSecond = 1f;

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      GameObject shot = Instantiate(bullet, shootingOffset.position, Quaternion.identity);
      Debug.Log("Bang!");

      Destroy(shot, 10f);

    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    Debug.Log("Collided with: " + collision.gameObject.name);
    if (collision.gameObject.name == "EnemyBullet(Clone)")
    {
      Destroy(collision.gameObject);
      Destroy(this.gameObject);
    }
  }

  void FixedUpdate()
  {
    // Using Input Manager to get Horizontal Axis for Up/Down movement of Paddle
    float movementAxis = Input.GetAxis("Horizontal");

    //Transform transform = GetComponent<Transform>();
    // Get Rigid Body Component
    Rigidbody2D rbody = GetComponent<Rigidbody2D>();

    // Add force to rigid body for movement
    Vector3 force = Vector3.right * movementAxis * movementPerSecond * Time.deltaTime;
    rbody.AddForce(force, ForceMode2D.Impulse);
  }
}
