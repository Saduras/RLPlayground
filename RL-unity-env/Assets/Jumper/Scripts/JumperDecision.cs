using System.Collections.Generic;
using MLAgents;
using UnityEngine;

public class JumperDecision : MonoBehaviour, Decision
{
    public float[] Decide(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
    {
        var actions = new float[1];
        var obstacleDistance = vectorObs[0];
        actions[0] = obstacleDistance < 2.5f ? 1f : 0f;
        return actions;
    }

    public List<float> MakeMemory(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
    {
        return new List<float>();
    }
}
