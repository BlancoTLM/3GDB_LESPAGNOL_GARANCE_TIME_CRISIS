using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Requires 2 children : one with a trigger collider for entering and one for exiting
[RequireComponent(typeof(Rigidbody))]
public class HysteresisTrigger : MonoBehaviour
{
    public GameObject enterCollider;
    public GameObject exitCollider;

    public UnityEvent EventTriggerEnter;
    public UnityEvent EventTriggerExit;

    public List<string> reactToTags = new List<string>();

    void Start()
    {
        // check if we have the colliders as children and emit warnings if not
        if (transform.Find(enterCollider.name) == null) Debug.LogWarning($"collider {enterCollider} should be a child of {this.name}");
        if (transform.Find(exitCollider.name) == null) Debug.LogWarning($"collider {exitCollider} should be a child of {this.name}");

        // make sure exit collider is deactivated
        exitCollider.gameObject.SetActive(false);
        enterCollider.gameObject.SetActive(true);

        GetComponent<Rigidbody>().isKinematic = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (this.enabled && EventTriggerEnter != null && (reactToTags.Count == 0 || reactToTags.Contains(other.tag)))
        {
            EventTriggerEnter.Invoke();
            enterCollider.gameObject.SetActive(false);
            exitCollider.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (this.enabled && EventTriggerExit != null && (reactToTags.Count == 0 || reactToTags.Contains(other.tag)))
        {
            EventTriggerExit.Invoke();
            exitCollider.gameObject.SetActive(false);
            enterCollider.gameObject.SetActive(true);
        }
    }
}
