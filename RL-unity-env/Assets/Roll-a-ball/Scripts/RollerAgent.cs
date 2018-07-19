using MLAgents;
using UnityEngine;

public class RollerAgent : Agent {

    public Transform target;

    Rigidbody rbody;

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
}
