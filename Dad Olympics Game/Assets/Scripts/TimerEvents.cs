using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerEvents : MonoBehaviour {

    public float duration;

    public UnityEvent OnStartTimer;
    public UnityEvent OnEndTimer;

    private bool _timing;
    private float time;

	public void StartTimer ()
    {
        if (!_timing)
        {
            Debug.Log(gameObject.name + " started");
            _timing = true;
            OnStartTimer.Invoke();
        }
    }

    public void StopTimer()
    {
        _timing = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(_timing)
        {
            time += Time.deltaTime;
            if (time >= duration)
            {
                OnEndTimer.Invoke();
                Debug.Log(gameObject.name + " ended");
                _timing = false;
            }
        }
	}
}
