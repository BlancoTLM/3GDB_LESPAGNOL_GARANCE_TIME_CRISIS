using UnityEngine;
using UnityEngine.Events;

public class SimpleTimer : MonoBehaviour
{
    public float timerDuration = 1.0f;
	public bool loop = false;

    public UnityEvent EventOnTimerStart;
    public UnityEvent EventOnTimerCanceled;
    public UnityEvent EventOnTimerComplete;

	bool active = false;
    float counter;
	
	
    void Update()
    {
        if (active)
        {
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                EventOnTimerComplete?.Invoke();
				if(loop)
				{
					counter += timerDuration;
				}else
				{
					active = false;
				}
            }
        }
    }

    public void StartTimer()
    {
        active = true;
        counter = timerDuration;
        EventOnTimerStart?.Invoke();
    }

    public void StopTimer()
    {
        active = false;
        if (counter < timerDuration) EventOnTimerCanceled?.Invoke();
    }

    

}
