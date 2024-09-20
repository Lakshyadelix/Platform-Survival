using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour, IPausable
{
    private float m_TravelDistance = 0;
    private float m_MaxTravelDistance = 15;
    private float m_Speed = 5.0f;

    private Coroutine m_ReverseCoroutine;
    private Rigidbody m_Rb;

	private void Awake()
	{
		m_Rb = GetComponent<Rigidbody>();
        enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        //this will keep increasing accutime per frame

        if (m_TravelDistance >= m_MaxTravelDistance) //if 0+1+2+3 increases 3 then stop the elevator and move it downwards
        {
            if (m_ReverseCoroutine == null)
            {
				m_ReverseCoroutine = StartCoroutine(nameof(ReverseElevator));
            }
        }

        else
        {
            float distanceStep = m_Speed * Time.fixedDeltaTime;
			m_TravelDistance += Mathf.Abs(distanceStep);

            Vector3 elevatorPost = m_Rb.position;
            elevatorPost.y += distanceStep;

            m_Rb.MovePosition(elevatorPost);
			//transform.Translate(0, m_Speed * Time.deltaTime, 0); //if 0+1+2+3 increases 3 then run the elevator //moves it on the y axis
        }
    }

    public void OnGameStart()
    {
        StartCoroutine(StartElevator());
    }

	private IEnumerator StartElevator()
	{

		yield return new WaitForSeconds(3f);
		enabled = true;
	}

	private IEnumerator ReverseElevator()
	{
        yield return new WaitForSeconds(3.0f);
        //wait for 3 secs until this code is executed
        m_TravelDistance = 0;
        m_Speed = -m_Speed;
        m_ReverseCoroutine = null;
	}
	
}
