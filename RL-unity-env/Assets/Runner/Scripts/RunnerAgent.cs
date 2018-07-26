﻿using System.Collections.Generic;
using MLAgents;
using UnityEngine;

namespace Runner
{
    public class RunnerAgent : Agent
    {

        public float observationDist = 10f;
        public float jumpForce = 3f;

        Rigidbody rbody;
        Vector3 startPos;
        RunnerAcademy academy;

        void Start()
        {
            rbody = GetComponent<Rigidbody>();
            startPos = this.transform.position;

            academy = FindObjectOfType<RunnerAcademy>();
        }

        public override void AgentReset()
        {
            this.transform.position = startPos;
            rbody.velocity = Vector3.zero;
            rbody.angularVelocity = Vector3.zero;

            academy.AcademyReset();
        }

        public override void CollectObservations()
        {
            var obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            var dists = new List<float>();
            foreach (var obstacle in obstacles) {
                var dist = obstacle.transform.position.x - transform.position.x;
                if (dist > 0 && dist < 10f)
                    dists.Add(dist);
            }

            // Observe closest obstacle
            dists.Sort();
            if (dists.Count > 0) {
                AddVectorObs(dists[0]);
            }
            else {
                AddVectorObs(float.PositiveInfinity);
            }
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            if (Vector3.Distance(startPos, this.transform.position) < 0.1f && rbody.velocity.sqrMagnitude < 0.1f) {
                rbody.AddForce(Vector3.up * Mathf.Clamp01(vectorAction[0]) * jumpForce, ForceMode.Impulse);
            }

            // Time reward
            AddReward(0.001f);

            Monitor.Log("cum. reward", GetCumulativeReward().ToString("F2"), this.transform);
            Monitor.Log("action", vectorAction[0], this.transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            // Death on collision
            if (other.CompareTag("Obstacle")) {
                AddReward(-1.0f);
                Done();
            }

            // Completed level
            if(other.CompareTag("Finish")) {
                AddReward(1.0f);
                Done();
            }
        }
    }
}