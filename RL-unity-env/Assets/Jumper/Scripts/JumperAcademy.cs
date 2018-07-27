using MLAgents;
using UnityEngine;

namespace Jumper
{
    public class JumperAcademy : Academy
    {
        public float obstacleSpeed = 3f;
        public Transform[] obstacles;
        public float range = 3f;

        Vector3[] startPositions;

        public override void InitializeAcademy()
        {
            startPositions = new Vector3[obstacles.Length];
            for (int i = 0; i < obstacles.Length; i++) {
                startPositions[i] = obstacles[i].position;
            }
        }

        public override void AcademyStep()
        {
            foreach (var obstacle in obstacles) {
                obstacle.Translate(Vector3.left * obstacleSpeed * Time.fixedDeltaTime);
            }
        }

        public override void AcademyReset()
        {
            for (int i = 0; i < obstacles.Length; i++) {
                obstacles[i].position = startPositions[i] + new Vector3(Random.Range(0, range),0,0);
            }
        }
    }
}

