using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class Hand : MonoBehaviour
{
    // Accion del mando (agarre)
    public SteamVR_Action_Boolean gripButton = null;

    // Accion del mando (gatillo)
    public SteamVR_Action_Boolean trackpadPress = null;


    // Establecer automaticamente la posicion y rotacion 
    private SteamVR_Behaviour_Pose m_Pose = null;
    // Conectar dos objetos sin parentesco
    private FixedJoint m_Joint = null;

    private Interactable objetoActual = null;
    public List<Interactable> listaObjetos = new List<Interactable>();

    void Awake()
    {
        // Mirar la documentacion de los componentes
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Si se mantiene presionado
        if (gripButton.GetStateDown(m_Pose.inputSource))
        {
            Pickup();
        }
        // Si se suelta
        if (gripButton.GetStateUp(m_Pose.inputSource))
        {
            Drop();
        }
        // Si hay accion
        if (objetoActual != null && trackpadPress.stateDown)
        {
            objetoActual.Action();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Colision de dos objetos, se añade a la lista el objeto interactuable
        if (!other.gameObject.CompareTag("Interactable")) return;
        listaObjetos.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {
        // Colision de dos objetos, se remueve de la lista el objeto interactuable
        if (!other.gameObject.CompareTag("Interactable")) return;
        listaObjetos.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {
        // Interactuamos con el mas cercano
        objetoActual = GetNearestInteractable();
        // Caso nulo
        if (!objetoActual) return;
        // Ya activo (queremos cogerlo con la otra mano)
        if (objetoActual.activo) objetoActual.activo.Drop();
        // Posicion         
        objetoActual.ApplyOffSet(transform);
        // Unir objeto a la mano
        Rigidbody objetoFisica = objetoActual.GetComponent<Rigidbody>();
        m_Joint.connectedBody = objetoFisica;
        // Activar objeto
        objetoActual.activo = this;
    }
    public void Drop()
    {
        
        // Caso nulo
        if (!objetoActual) return;
        // Aplicar velocidad (lanzamiento)
        Rigidbody objetoFisica = objetoActual.GetComponent<Rigidbody>();
        objetoFisica.velocity = m_Pose.GetVelocity();
        objetoFisica.angularVelocity = m_Pose.GetAngularVelocity();
        // Separar el objeto de la mano
        m_Joint.connectedBody = null;
        // Limpiar valores (desactivar)
        objetoActual.activo = null;
        objetoActual = null;
        
    }
    private Interactable GetNearestInteractable()
    {
        Interactable cercano = null;
        float dMin = float.MaxValue;
        float distancia = 0.0f;
        // Comprobar el objeto que se ecuentra mas cerca de una lista de objetos interactuables
        foreach (Interactable interactable in listaObjetos)
        {
            distancia = (interactable.transform.position - transform.position).sqrMagnitude;
            if (distancia < dMin)
            {
                dMin = distancia;
                cercano = interactable;
            }
        }
        return cercano;
    }
}