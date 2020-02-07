using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private enum PlayerAnimatorLayer
    {
        Jog = 0,
        Boost = 1,
        Walk = 2
    }

    [SerializeField]
    private float m_timeToStop = .2f;

    private Animator m_animator;

    private bool m_isMoving;

    private float m_lastMovement;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_animator.SetLayerWeight((int)PlayerAnimatorLayer.Jog, 0);
            m_animator.SetLayerWeight((int)PlayerAnimatorLayer.Boost, 1);
            m_animator.SetLayerWeight((int)PlayerAnimatorLayer.Walk, 0);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            m_animator.SetLayerWeight((int)PlayerAnimatorLayer.Jog, 0);
            m_animator.SetLayerWeight((int)PlayerAnimatorLayer.Boost, 0);
            m_animator.SetLayerWeight((int)PlayerAnimatorLayer.Walk, 1);
        }
        else
        {
            m_animator.SetLayerWeight((int)PlayerAnimatorLayer.Jog, 1);
            m_animator.SetLayerWeight((int)PlayerAnimatorLayer.Boost, 0);
            m_animator.SetLayerWeight((int)PlayerAnimatorLayer.Walk, 0);
        }

        Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        m_animator.SetFloat("Xvelocity", velocity.x);
        m_animator.SetFloat("Zvelocity", velocity.y);

        if (velocity != Vector2.zero)
        {
            m_isMoving = true;
            m_lastMovement = Time.time;
        }
        else if (Time.time >= m_lastMovement + m_timeToStop)
        {
            m_isMoving = false;
        }

        m_animator.SetBool("IsMoving", m_isMoving);
    }

    private void LateUpdate()
    {
        
    }
}
