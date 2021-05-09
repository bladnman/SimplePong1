using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

  static public event Action<Ball> BallHitGoal;

  private void OnTriggerEnter2D(Collider2D other) {
    if (BallHitGoal != null) {
      BallHitGoal(this);
    }
  }
}
