using MLAgents;
using UnityEngine;

public class EndlessRunnerAcademy : Academy {

    public float obstacleSpeed = 3f;
    public Transform obstacle;

    private Vector3 obstacleStartPos;
    private float randomRange = 2f;

	public override void InitializeAcademy()
	{
        obstacleStartPos = obstacle.position;
	}

	public override void AcademyStep()
	{
        obstacle.Translate(Vector3.left * obstacleSpeed * Time.fixedDeltaTime);
        if (obstacle.position.x < 0)
            PlaceObstacle();
    }

	public override void AcademyReset()
	{
        PlaceObstacle();
    }

    private void PlaceObstacle()
    {
        obstacle.position = obstacleStartPos + new Vector3(Random.Range(-randomRange, randomRange), 0f, 0f);
    }
}
