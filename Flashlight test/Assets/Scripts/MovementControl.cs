using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class MovementControl : MonoBehaviour
{
	public float m_MovingTurnSpeed = 360;
	public float m_StationaryTurnSpeed = 180;
	public float m_MoveSpeedMultiplier = 1f;
	public float m_AnimSpeedMultiplier = 1f;

	Rigidbody m_Rigidbody;
	Animator m_Animator;
	float m_TurnAmount;
	float m_ForwardAmount;
	Vector3 m_CapsuleCenter;

	void Start()
	{
		m_Animator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody>();
				
		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}

	public void Move(Vector3 move)
	{
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);
		m_TurnAmount = Mathf.Atan2(move.x, move.z);
		m_ForwardAmount = move.z;

		ApplyExtraTurnRotation();
		
		UpdateAnimator(move);
	}

	void UpdateAnimator(Vector3 move)
	{
		m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
		m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);

		if (move.magnitude > 0)
		{
			m_Animator.speed = m_AnimSpeedMultiplier;
		}
		else
		{
			m_Animator.speed = 1;
		}
	}

	void ApplyExtraTurnRotation()
	{
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}

	public void OnAnimatorMove()
	{
		if (Time.deltaTime > 0)
		{
			Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}
	}
}







