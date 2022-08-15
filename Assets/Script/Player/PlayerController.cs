using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 20f;												// 점프 힘
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;					// 앉기 속도
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;		// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;												// 공중에서 조작 가능 여부 체크
	[SerializeField] private LayerMask m_WhatIsGround;										// 땅바닥 체크
	[SerializeField] private Transform m_GroundCheck;                                          // 지면 체크
	[SerializeField] private Transform m_CeilingCheck;											// 천장 체크
	[SerializeField] private Collider2D m_CrouchDisableCollider;								// 충돌 시 앉은 상태 비활성화

	const float k_GroundedRadius = .2f;																	// Radius of the overlap circle to determine if grounded
	[HideInInspector] public bool m_Grounded;														// 플레이어가 지면에 있는가?
	const float k_CeilingRadius = .2f;																		// 플레이어가 일어날 수 있는 확인하기 위함
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;																		// 플레이어가 현재 향하는 방향
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	[Header("Icon")]
	public GameObject interactIcon;
	private Vector2 boxSize = new Vector2(1.0f, 1.0f);

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

    private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}
		
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void OpenInteractableIcon()
    {
		
		interactIcon.SetActive(true);
    }

	public void CloseInteractableIcon()
    {
		interactIcon.SetActive(false);
    }

	public void CheckInteraction()
    {
		RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

		if(hits.Length > 0)
        {
			foreach (RaycastHit2D rc in hits)
            {
				if (rc.collider.tag == "DataTerminal")
                {
					rc.IsOpenable();
                }

				if (rc.IsInteractable())
                {
					rc.Interact();
					return;
                }
            }
        }
    }
}