using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{

    public float force = 0f;
    public float size = 0f;
    public Vector3 prob = Vector3.zero;
    //private Vector3 pointer1 = Vector3.zero;
    //private Vector3 pointer2 = Vector3.zero;

    private Rigidbody myRigidbody = null;

    public void SetParameters(float iForce, float iSize, Vector3 prob /*, Vector3 pointer1, Vector3 pointer2*/)
    {
        print("Hola");
        force = iForce;
        size = iSize;
        this.prob = prob;
        //this.pointer1 = pointer1;
        //this.pointer2 = pointer2;

    }

    public void StartLaunch()
    {
        transform.localScale = transform.localScale * size;

        myRigidbody = GetComponent<Rigidbody>();

        Vector3 direction = (/*pointer1.normalized*/ Vector3.forward * prob[0] + Vector3.up * prob[1] + /*pointer2.normalized*/ Vector3.left * prob[2]).normalized;

        myRigidbody.AddForce(direction * force, ForceMode.Impulse);
    }

    public void Hit(bool puntos)
    {
        if (puntos)
        {
            FindObjectOfType<GameController>().AddPoints(1);
        }

        Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Flor")
        {
            Hit(false);
        }
    }
}
