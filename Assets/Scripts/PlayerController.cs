using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] float m_Speed;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Vector3 direction;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update ()
	{
		var axisX = Input.GetAxis("Horizontal");
		var axisZ = Input.GetAxis("Vertical");
		
		direction = new Vector3(axisX, 0, axisZ);

        m_Rigidbody.MovePosition
            (m_Rigidbody.position + 
            (direction * (Time.deltaTime * m_Speed)));

        if(direction != Vector3.zero)
        {
            m_Animator.SetBool("isMove", true);
        }
        else
        {
            m_Animator.SetBool("isMove", false);
        }
    }

    private void FixedUpdate()
    {
        m_Rigidbody.MovePosition
            (m_Rigidbody.position +
            (direction * (Time.deltaTime * m_Speed)));
    }
}
