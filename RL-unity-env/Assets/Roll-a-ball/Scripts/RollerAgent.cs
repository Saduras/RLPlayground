using MLAgents;
using UnityEngine;

public class RollerAgent : Agent {

    public Transform target;
    public float speed = 6f;

    Rigidbody rbody;
    float prevDist;
    const float plattformBounds = 5f;

	void Start ()
    {
        rbody = GetComponent<Rigidbody>();
	}

    public override void AgentReset()
    {
        if(this.transform.position.y < -1.0) {
            // The agent fell
            this.transform.position = Vector3.zero;
            this.rbody.angularVelocity = Vector3.zero;
            this.rbody.velocity = Vector3.zero;
        }
        else {
            // Move target to new position
            var range = 8f;

            target.position = new Vector3(Random.value * range - range / 2, 0.5f, Random.value * range - range / 2);
        }
    }

    public override void CollectObservations()
    {
        var relativePos = target.position - this.transform.position;

        // Observe target position
        AddVectorObs(relativePos.x / plattformBounds);
        AddVectorObs(relativePos.y / plattformBounds);

        // Observe agent distance to edges
        AddVectorObs((this.transform.position.x + plattformBounds) / plattformBounds);
        AddVectorObs((this.transform.position.x - plattformBounds) / plattformBounds);
        AddVectorObs((this.transform.position.y + plattformBounds) / plattformBounds);
        AddVectorObs((this.transform.position.y - plattformBounds) / plattformBounds);

        // Observe agent velocity
        AddVectorObs(rbody.velocity.x / plattformBounds);
        AddVectorObs(rbody.velocity.y / plattformBounds);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        var controlSignal = new Vector3(vectorAction[0], 0f, vectorAction[1]);
        rbody.AddForce(controlSignal * speed);

        // Reward for reaching target
        var distToTarget = Vector3.Distance(this.transform.position, target.position);
        if(distToTarget < 1.42f) {
            AddReward(1.0f);
            Done();
        }

        // Getting closer to target
        if(distToTarget < prevDist) {
            AddReward(0.1f);
        }
        prevDist = distToTarget;

        // Time penalty
        AddReward(-0.05f);

        // Death penalty
        if(this.transform.position.y < -1.0) {
            AddReward(-1.0f);
            Done();
        }
    }
}
