using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{

  public Vector3 playerStartPosition = new Vector3(0.0f, 0.0f, 0.0f);
  private bool moveState = false;
  private Vector2 direction;
  private Rigidbody2D body;
  private Animator animator;
  public SpriteRenderer sprite;
  private BoxCollider2D boxCollider;
  public LevelManager levelManager;
  public LayerMask enemyMask;
  public float offset;
  public Vector2 hitBoxSize;
  public UnityEvent<int> TakeDamage;

  public float countdownDuration = 0.7f; // in seconds
  private float countdownTimeLeft;
  private bool countdownActive = false;
  public int Damage = 13;
  private int giveDamageHit = 5;

  // Start is called before the first frame update
  void Start()
  {
    body = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    boxCollider = GetComponent<BoxCollider2D>();
    sprite = GetComponent<SpriteRenderer>();
    countdownTimeLeft = countdownDuration;
  }

  // Update is called once per frame
  void Update()
  {
    if (countdownActive)
    {
      if (countdownTimeLeft > 0)
      {
        countdownTimeLeft -= Time.deltaTime;
      }
      else
      {
        countdownActive = false;
        countdownTimeLeft = countdownDuration;
      }
    }
  }

  void FixedUpdate()
  {
    if (levelManager.playerAlive && moveState)
    {
      move(direction);
    }
  }

  public void CheckMove(Vector2 value)
  {
    if (value.magnitude == 0)
    {
      moveState = false;
      body.velocity = value;
    }
    else
    {
      moveState = true;
      direction = value;
      flipSprite((int)value.x);
      move(value);
    }
  }

  void move(Vector2 value)
  {
    // Debug.Log("movement " + value);
    Vector2 movement = new Vector2(value.x * levelManager.playerSpeed, value.y * levelManager.playerSpeed);
    body.velocity = movement;
  }

  private void flipSprite(int value)
  {
    if (value <= -1)
    {
      sprite.flipX = false;
    }
    else if (value >= 1)
    {
      sprite.flipX = true;
    }
  }
  public void Melee()
  {
    float direction = sprite.flipX ? 1 : -1;
    RaycastHit2D hit = Physics2D.BoxCast(transform.position, hitBoxSize, 0, transform.right * direction, offset, enemyMask);
    if (hit.collider != null)
    {
      Debug.Log("melee hit with object " + hit.collider.gameObject.name);
      hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(giveDamageHit);
    }
  }
  void OnDrawGizmos()
  {
    float direction = sprite.flipX ? 1 : -1;
    Gizmos.color = Color.yellow;
    Gizmos.DrawCube(transform.position + transform.right * offset * direction, hitBoxSize);
  }

  void OnTriggerStay2D(Collider2D other)
  {
    // Debug.Log(countdownActive);
    if (other.CompareTag("Enemy") && !countdownActive)
    {
      countdownActive = true;
      TakeDamage.Invoke(Damage);
    }
  }
}
