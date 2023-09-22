using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

  public Transform player;
  public Transform start;
  public Transform end;
  private float offset; // initial x-offset between camera and Mario
  private float startX; // smallest x-coordinate of the Camera
  private float endX; // largest x-coordinate of the camera
  private float startY;
  private float endY;
  private float viewportHalfWidth;
  private float viewportHalfHeight;

  void Start()
  {
    // get coordinate of the bottomleft of the viewport
    // z doesn't matter since the camera is orthographic
    viewportHalfWidth = Mathf.Abs(start.position.x - this.transform.position.x);
    viewportHalfHeight = Mathf.Abs(start.position.y - this.transform.position.y);
    offset = this.transform.position.x - player.position.x;
    startX = start.transform.position.x + viewportHalfWidth;
    endX = end.transform.position.x - viewportHalfWidth;
    startY = start.transform.position.y + viewportHalfHeight;
    endY = end.transform.position.y - viewportHalfHeight;
  }

  void Update()
  {
    float desiredX = player.position.x + offset;
    float desiredY = player.position.y - offset;
    // Debug.Log(desiredX + " " + desiredY);
    if (desiredX > startX && desiredX < endX && desiredY > startY && desiredY < endY)
      this.transform.position = new Vector3(desiredX, desiredY, this.transform.position.z);
  }

}
