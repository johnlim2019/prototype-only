using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  readonly float speed = 0.1f;
  public GameObject player;
  private Rigidbody2D rigidBody;
  // Start is called before the first frame update
  void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    rigidBody.AddForce(getDirection() * speed);
  }

  Vector2 getDirection()
  {
    Vector2 p2 = player.transform.position;
    Vector2 p1 = this.transform.position;
    Vector2 direction = p2 - p1;
    // Debug.Log(direction.normalized);
    return direction.normalized;
  }
}
