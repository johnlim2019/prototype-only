using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  int health = 5;

  Rigidbody2D rigidBody;
  BoxCollider2D boxCollider;

  void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
    boxCollider = GetComponent<BoxCollider2D>();
  }

  void Update()
  {
    if (health <= 0)
    {
      die();
    }
  }

  public void TakeDamage(int damage)
  {
    health -= damage;
  }

  private void die()
  {
    Debug.Log("enemy dead");
    // animate
    rigidBody.bodyType = RigidbodyType2D.Static;
    boxCollider.enabled = false;
    // delete
    Destroy(this.gameObject);
  }


}
