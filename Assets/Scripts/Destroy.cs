using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public void DestroyObject()
    {
        GameObject.Destroy(this.gameObject);
    }
}
