using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent EventGameStart;
    public UnityEvent EventPlayerEnterArea;
    public UnityEvent EventAreaCleared;
    public UnityEvent EventTimeDepleted;
    public UnityEvent EventStageCleared;

    public GameObject[] areasRoots;
    int currentArea;
    Player player;

    public float baseRemainingTime = 20;
    float remainingTime;
    public float RemainingTime { get { return remainingTime; } }

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }


    private void Start()
    {
        remainingTime = baseRemainingTime;
        EventGameStart.Invoke();
    }

    private void Update()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if(remainingTime < 0)
            {
                remainingTime = 0;
                EventTimeDepleted.Invoke();
            }
        }
    }

    public void ActivateArea(int areaID)
    {
        areasRoots[areaID].SetActive(true);
        player.SetAreaPositions(areasRoots[areaID].transform.Find("PlayerPos"), areasRoots[areaID].transform.Find("CoverPos"));
        EventPlayerEnterArea.Invoke();
    }

    void OnAreaCleared()
    {
        EventAreaCleared.Invoke();
        currentArea++;
        if(currentArea >= areasRoots.Length)
        {
            EventStageCleared.Invoke();
        }
    }

    public void ExtendTime(int extraTime)
    {
        remainingTime += extraTime;
    }
}
