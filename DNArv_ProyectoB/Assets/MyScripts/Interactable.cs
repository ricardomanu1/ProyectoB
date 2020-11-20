using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    // Serializar la variable
    [HideInInspector]
    public Hand activo = null;

    public Vector3 poss = Vector3.zero;
    public Quaternion rot = Quaternion.identity;
    private Vector3 posicion;
    public GameObject bala;

    public virtual void Action()
    {
        if (this.gameObject.name=="gun")
        {
            GameObject gunObject = GameObject.Find("gun");
            Weapon1Actions weapon1Actions = (Weapon1Actions)gunObject.GetComponent(typeof(Weapon1Actions));
            weapon1Actions.Shoot();
        }
    }
    

    public void ApplyOffSet(Transform hand)
    {
        transform.SetParent(hand);
        transform.localRotation = rot;
        transform.localPosition = poss;
        transform.SetParent(null);
    }
}