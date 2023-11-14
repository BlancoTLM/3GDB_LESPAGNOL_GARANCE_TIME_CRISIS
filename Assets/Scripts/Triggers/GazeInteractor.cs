using UnityEngine;
using UnityEngine.Events;

public class GazeInteractor : MonoBehaviour
{
    public UnityEvent EventGazeEnter;
    public UnityEvent EventGazeValidated;
    public UnityEvent EventGazeExit;
    public UnityEvent EventGazeCancelled;

    public Collider gazeDetector;
    public float maxDistance;
    public float gazeValidationDelay = 0;

    Transform cameraTransform;

    // we keep track of previous frame gaze detection to detect enter/exit events
    bool lastFrameGazeDetection = false;
    // this boolean tells if the gaze has been validated (ie: the activation delay has passed)
    bool gazeValidated = false;

    public bool DEBUG = false;
    
    protected virtual void Start()
    {
        cameraTransform = Camera.main.transform; 
    }

    protected virtual void Update()
    {
        bool gazeDetectedThisFrame = false;

        RaycastHit hit;
        gazeDetectedThisFrame = gazeDetector.Raycast(new Ray(cameraTransform.position, cameraTransform.forward), out hit, maxDistance);

        if(!lastFrameGazeDetection && gazeDetectedThisFrame)
        {
            EventGazeEnter.Invoke();
            // validation should occur N seconds after gaze entering
            Invoke("OnGazeValidated", gazeValidationDelay); // we use mono's Invoke instead of a timer
        }
        if(lastFrameGazeDetection && !gazeDetectedThisFrame)
        {
            if(gazeValidated)
            {
                // gaze already validated, we have an exit event
                EventGazeExit.Invoke();
            }else
            {
                // gaze not validated yet, we have a cancel event
                CancelInvoke("OnGazeValidated");
                EventGazeCancelled.Invoke();
            }
        }

        lastFrameGazeDetection = gazeDetectedThisFrame;
    }

    // after gaze enter, if gazeValidationDelay has passed
    protected virtual void OnGazeValidated()
    {
        gazeValidated = true;
        EventGazeValidated.Invoke();
    }

    protected virtual void OnGazeExit()
    {
        gazeValidated = false;
    }


    void OnDrawGizmos()
    {
        if(DEBUG)
        {
            Gizmos.DrawWireSphere(transform.position, maxDistance);
            Gizmos.color = Color.blue;
            if(cameraTransform != null)
            {
                Gizmos.DrawWireSphere(cameraTransform.position, 10);
                Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * maxDistance);
            }
        }
    }
}
