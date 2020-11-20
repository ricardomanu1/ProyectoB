using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class VRController : MonoBehaviour
{
    public float gravedad = 90.0f;
    public float sensibilidad = 0.1f;
    public float vMax = 0.8f;

    // Accion del mando
    public SteamVR_Action_Vector2 touchpad = null;

    private float velocidad = 0.0f;

    private CharacterController player = null;
    // Posicion, rotacion y escala de un objeto
    private Transform p_Head = null;

    private void Awake()
    {
        player = GetComponent<CharacterController>();

    }
    // Start is called before the first frame update
    private void Start()
    {
        // Posicion local del HMD
        p_Head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    private void Update()
    {
        HMDPosition();
        CalculateMovement();

    }

    private void HMDPosition()
    {
        // Posicionar la cabeza en el espacio local (limitado)
        float headHeight = Mathf.Clamp(p_Head.localPosition.y, 1, 2);
        player.height = headHeight;

        // Cortar por la mitad
        Vector3 newCenter = Vector3.zero;
        newCenter.y = player.height / 2;
        newCenter.y += player.skinWidth;

        // Mover la capsula en el espacio local (area de movimiento)
        newCenter.x = p_Head.localPosition.x;
        newCenter.z = p_Head.localPosition.z;

        // Aplicar
        player.center = newCenter;

    }
    private void CalculateMovement()
    {
        // Orientacion del movimiento
        Quaternion orientacion = CalculateOrientation();
        Vector3 movimiento = Vector3.zero;

        // Si no hay movimiento
        if (touchpad.axis.magnitude == 0)
            velocidad = 0;

        // Añadir y restringir
        velocidad += touchpad.axis.magnitude * sensibilidad;
        velocidad = Mathf.Clamp(velocidad, -vMax, vMax);

        // Movimiento en base a la orientacion frontal (0,0,1)
        movimiento += orientacion * (velocidad * Vector3.forward);

        // Movimiento en el eje y (gravedad)
        movimiento.y -= gravedad * Time.deltaTime;

        // Aplicar movimiento al objeto
        player.Move(movimiento * Time.deltaTime);

    }
    private Quaternion CalculateOrientation()
    {
        // Angulo en radianes entre el eje x y un vector 0->(x,y)
        float rotation = Mathf.Atan2(touchpad.axis.x, touchpad.axis.y);
        rotation *= Mathf.Rad2Deg; //a grados

        // Calculo de la orientacion del HMD en el eje y
        Vector3 orientationEuler = new Vector3(0, p_Head.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);

    }

}
