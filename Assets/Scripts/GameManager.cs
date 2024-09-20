using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class gAMEmANAGER : MonoBehaviour
{
    public GameObject startScreen;
    public UnityEvent OnGameStart;

    private bool m_IsGameActive = false;
    private SpawnManager m_SpawnManager;

    // Start is called before the first frame update
    void Start()
    {
        m_SpawnManager = FindObjectOfType<SpawnManager>();
        var elevators = FindObjectsOfType<Elevator>();

        for (int i = 0; i < elevators.Length; i++)
        {
            OnGameStart.AddListener(elevators[i].OnGameStart);
        }
	}
     
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    public void StartGame()
    {
        OnGameStart.Invoke();
		m_SpawnManager.StartSpawning();
        startScreen.SetActive(false); 
	}
}
