using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

  [SerializeField] float paddleSpeed = 1f;

  bool isLeft = false;
  void Start() {
    isLeft = transform.position.x < 0;
  }

  void Update() {
    float newY = 0f;
    // UP
    if (isLeft && Input.GetKey(KeyCode.W) || !isLeft && Input.GetKey(KeyCode.UpArrow)) {
      newY = transform.position.y + paddleSpeed * Time.deltaTime;
    }

    // DOWN
    else if (isLeft && Input.GetKey(KeyCode.S) || !isLeft && Input.GetKey(KeyCode.DownArrow)) {
      newY = transform.position.y - paddleSpeed * Time.deltaTime;
    }

    if (newY != 0f) {
      transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

  }
}
