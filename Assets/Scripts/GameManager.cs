using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField] GameObject ballPrefab;

  [SerializeField] float secondsPerBall = 5f;
  [SerializeField] int maxBalls = 3;
  [SerializeField] float ballLaunchMin = 2f;
  [SerializeField] float ballLaunchMax = 7f;
  [SerializeField] TMP_Text leftScoreLabel;
  [SerializeField] TMP_Text rightScoreLabel;

  int leftScore = 0;
  int rightScore = 0;
  float lastLaunchTime = 0f;
  bool IsBallQueued = true;
  int ballCount = 0;


  void Start()
  {
    lastLaunchTime = Time.time - 2; // 2 seconds to launch first ball
    Ball.BallHitGoal += HandleGoal;
    UpdateScores();
  }
  private void OnDestroy()
  {
    Ball.BallHitGoal -= HandleGoal;
  }
  void HandleGoal(Ball ball)
  {
    bool isLeftGoal = ball.transform.position.x < 0f;
    if (isLeftGoal)
    {
      rightScore++;
    }
    else
    {
      leftScore++;
    }
    ballCount--;
    Destroy(ball);

    UpdateScores();
  }
  void UpdateScores()
  {
    leftScoreLabel.text = $"{leftScore}";
    rightScoreLabel.text = $"{rightScore}";
  }
  bool FlipIsHeads()
  {
    return Random.Range(0, 1) == 1;
  }
  void SpawnBall()
  {

    if (ballCount >= maxBalls) return;

    Vector2 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    var dist = Camera.main.transform.position.z * -1;
    Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, dist));
    var ball = Instantiate(ballPrefab, centerPos, Quaternion.identity);

    var rb = ball.GetComponent<Rigidbody2D>();
    var vx = UnityEngine.Random.Range(ballLaunchMin, ballLaunchMax);
    var vy = UnityEngine.Random.Range(ballLaunchMin, ballLaunchMax);
    vx = FlipIsHeads() ? vx : vx * -1;
    vy = FlipIsHeads() ? vy : vy * -1;
    rb.velocity = new Vector2(vx, vy);

    lastLaunchTime = Time.time;
    ballCount++;
  }

  private void Update()
  {
    // spawn ever x
    if (Time.time - lastLaunchTime > secondsPerBall)
    {
      SpawnBall();
    }
  }

}
