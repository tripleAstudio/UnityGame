using UnityEngine;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (MovementControl))]
public class AIControl : MonoBehaviour
{
	public NavMeshAgent agent { get; private set; }
	public MovementControl character { get; private set; }
	public Transform target;

	private void Start()
	{
		agent = GetComponentInChildren<NavMeshAgent>();
		character = GetComponent<MovementControl>();

		agent.updateRotation = false;
		agent.updatePosition = true;
	}

	private void Update()
	{
		if (target != null) 
		{
			agent.SetDestination (target.position);
			character.Move (agent.desiredVelocity);
		} 
		else 
		{
			character.Move (Vector3.zero);
		}
	}
	
	public void SetTarget(Transform target)
	{
		this.target = target;
	}
}