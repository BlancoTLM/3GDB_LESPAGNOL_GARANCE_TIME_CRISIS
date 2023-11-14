using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class TriggerEvents : MonoBehaviour
{
    public UnityEvent EventTriggerEnter;
    public UnityEvent EventTriggerExit;

    public bool onlyOnce = false;
    public bool hasPhysics = false;

    public List<string> reactToTags = new List<string>();

    // dans les deux d�clarations suivantes, new permet d'�craser la d�finition des variables collider et rigidbody qui existent d�j� dans MonoBehaviour pour des raisons de compatibilit�
    // (elles ne sont plus utilis�es par Unity depuis Unity 5 mais restent pour ne pas casser de vieux projets qui les utiliseraient)
    private new Collider collider;
    private new Rigidbody rigidbody;

    void Awake()
    {
        Initialize();
    }

    private void OnValidate()
    {
        Initialize();
    }

    void Initialize()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = !hasPhysics;
    }

    void Start()
    {
        if (!collider.isTrigger)
        {
            // la notation de Log avec un $ avant la cha�ne de caract�res permet d'ins�rer des noms de variables directement � l'aide d'accolades.
            // (ce qui permet d'�viter les alternances de "chaine "+var+" chaine"+var, etc
            Debug.LogWarning($"Collider of {gameObject.name} not set as Trigger. Events will not work.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (this.enabled && (reactToTags.Count == 0 || reactToTags.Contains(other.tag)))
        {
            EventTriggerEnter.Invoke();
            if(onlyOnce)
            {
                // ici on prend le parti de d�sactiver un trigger only once sur trigger enter.
                // il y a bien s�r des cas o� on voudra avoir des trigger exit, il faudrait pouvoir donner le choix � l'utilisateur.
                this.enabled = false;
                EventTriggerEnter.RemoveAllListeners(); // si on d�sactive, autant vider les listeners
                EventTriggerExit.RemoveAllListeners(); // si on d�sactive, autant vider les listeners
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (this.enabled && (reactToTags.Count == 0 || reactToTags.Contains(other.tag)))
        {
            EventTriggerExit.Invoke();
        }
    }
}
