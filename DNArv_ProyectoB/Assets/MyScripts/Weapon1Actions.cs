using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1Actions : MonoBehaviour
{

    public GameObject proyectile;
    public float proyectileForce;
    private Transform mira;
    private AudioSource aud;
    private void Start()
    {
        mira = transform.Find("ShootPoint");
        aud = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Shoot();
        }

    }

    public void Shoot() 
    {
        aud.Play();
        GameObject mibala = GameObject.Instantiate(proyectile, mira.position, Quaternion.identity);
        mibala.GetComponent<Rigidbody>().AddForce(-transform.up * proyectileForce, ForceMode.Impulse);

    }
}
