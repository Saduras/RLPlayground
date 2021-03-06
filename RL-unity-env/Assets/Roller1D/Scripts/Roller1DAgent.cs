﻿using MLAgents;
using UnityEngine;

namespace Roller1D
{
    public class Roller1DAgent : Agent
    {
        const float plattformBounds = 5f;

        public Transform target;
        public float speed = 6f;

        Rigidbody rbody;
        float prevDist;
        Vector3 center;

        int deathCounter;
        int goalCounter;

        void Start()
        {
            rbody = GetComponent<Rigidbody>();
            center = this.transform.position - new Vector3(0, 0.5f, 0);
        }

        public override void AgentReset()
        {
            if (this.transform.position.y < -1.0) {
                // The agent fell
                this.transform.position = center;
                this.rbody.angularVelocity = Vector3.zero;
                this.rbody.velocity = Vector3.zero;
            }
            else {
                // Move target to new position
                var range = 8f;

                target.position = new Vector3(Random.value * range - range / 2, 0.5f, 0f) + center;
            }
        }

        public override void CollectObservations()
        {
            var relativePos = target.position - this.transform.position;

            // Observe target position
            AddVectorObs(relativePos.x / plattformBounds);
            Monitor.Log("d:", relativePos.x / plattformBounds, target);

            // Observe agent distance to edges
            AddVectorObs((this.transform.position.x + plattformBounds) / plattformBounds);
            AddVectorObs((this.transform.position.x - plattformBounds) / plattformBounds);

            // Observe agent velocity
            AddVectorObs(rbody.velocity.x / plattformBounds);
            Monitor.Log("v:", rbody.velocity.x / plattformBounds, this.transform);
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            var controlSignal = new Vector3(vectorAction[0], 0f, 0f);
            rbody.AddForce(controlSignal * speed);

            // Reward for reaching target
            var distToTarget = Vector3.Distance(this.transform.position, target.position);
            if (distToTarget < 1.42f) {
                AddReward(1.0f);
                goalCounter++;
                Done();
            }

            // Getting closer to target
            if (distToTarget < prevDist) {
                AddReward(0.1f);
            }
            prevDist = distToTarget;

            // Time penalty
            AddReward(-0.05f);

            // Death penalty
            if (this.transform.position.y < -1.0) {
                AddReward(-1.0f);
                deathCounter++;
                Done();
            }

            Monitor.Log("goals:", goalCounter.ToString(), this.transform);
            Monitor.Log("deaths:", deathCounter.ToString(), this.transform);
            Monitor.Log("cul. rewards:", GetCumulativeReward().ToString("F2"), this.transform);
        }
    }
}