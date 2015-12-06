using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	private int m_maxHealth = 100;
	private int m_curHealth = 100;
	
	public int MaxHealth
	{
		get
		{
			return m_maxHealth;
		}
		set
		{
			m_maxHealth = value;
		}
	}
	
	public int CurHealth
	{
		get
		{
			return m_curHealth;
		}
		set
		{
			m_curHealth = value;
		}
	}
	
	
	void Start () {
	
	}

	void Update () 
	{
		if (MaxHealth < CurHealth) 
		{
			CurHealth = MaxHealth;
		}
		if (CurHealth < 0) CurHealth = 0;
	}

	void OnCollisionEnter(Collision myCollision)
	{
		if (myCollision.gameObject.tag == "Enemy") 
		{
			CurHealth = CurHealth - 10;
		}
	}
}
