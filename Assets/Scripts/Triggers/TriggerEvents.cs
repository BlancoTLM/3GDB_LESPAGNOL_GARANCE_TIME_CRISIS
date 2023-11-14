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

    // dans les deux déclarations suivantes, new permet d'écraser la définition des variables collider et rigidbody qui existent déjà dans MonoBehaviour pour des raisons de compatibilité
    // (elles ne sont plus utilisées par Unity depuis Unity 5 mais restent pour ne pas casser de vieux projets qui les utiliseraient)
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
            // la notation de Log avec un $ avant la chaîne de caractères permet d'insérer des noms de variables directement à l'aide d'accolades.
            // (ce qui permet d'éviter les alternances de "chaine "+var+" chaine"+var, etc
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
                // ici on prend le parti de désactiver un trigger only once sur trigger enter.
                // il y a bien sûr des cas où on voudra avoir des trigger exit, il faudrait pouvoir donner le choix à l'utilisateur.
                this.enabled = false;
                EventTriggerEnter.RemoveAllListeners(); // si on désactive, autant vider les listeners
                EventTriggerExit.RemoveAllListeners(); // si on désactive, autant vider les listeners
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
