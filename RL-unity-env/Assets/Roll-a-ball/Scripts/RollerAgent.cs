using MLAgents;
using UnityEngine;

public class RollerAgent : Agent {

    public Transform target;

    Rigidbody rbody;

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
}
