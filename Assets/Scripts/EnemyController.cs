using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody m_Rb;
    private GameObject m_FollowTarget;
    public float pushRadius;
    private bool m_IsRecharched;

    [SerializeField] private float speed;


	private void Awake()
	{
        AddCircle();
		m_Rb = GetComponent<Rigidbody>();
        m_IsRecharched = true;
	}
	// Start is called before the first frame update
	void Start()
    {
        m_FollowTarget = GameObject.Find("player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveTowards = m_FollowTarget.transform.position - transform.position; //move towards player
        moveTowards.y = 0; //so that the enemy cant come up after falling
        m_Rb.AddForce(moveTowards * speed);

        if (Mathf.Abs(moveTowards.magnitude) <=pushRadius && m_IsRecharched) 
        {
            m_IsRecharched = false;
			m_Rb.AddForce(moveTowards * speed * 0.5f, ForceMode.Impulse);
            Invoke(nameof(Recharge),2f);
		}

		if (transform.position.y <= -20f)
        {
            Destroy(gameObject);
        }
    }

    void Recharge()
    {
        m_IsRecharched = true;
    }

    void AddCircle()
    {
		GameObject go = new GameObject
		{
			name = "Circle"
		};

		Vector3 circlePosition = Vector3.zero;
        circlePosition.y = -0.12f;

		go.transform.parent = transform;
		go.transform.localPosition = circlePosition;

		go.DrawCircle(pushRadius, 0.02f);


	}

}
