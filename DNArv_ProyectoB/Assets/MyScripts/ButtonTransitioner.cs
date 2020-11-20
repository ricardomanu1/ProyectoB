using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    public GameController GC;

    public Color32 m_NormalColor = Color.white;
    public Color32 m_HoverColor = Color.grey;
    public Color32 m_DownColor = Color.white;

    public float x;
    public float y;
    public float z;

    private Image m_Image = null;

    private void Awake() 
    {
        GC = GameObject.Find("GameController").GetComponent<GameController>();
        m_Image = GetComponent<Image>();            
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        // Sobre objetivo
        //print("Enter");
        m_Image.color = m_HoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Fuera del objetivo
        //print("Exit");
        m_Image.color = m_NormalColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Presiona
        //print("Up");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Suelta
        //print("Down");
        m_Image.color = m_DownColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("Click!!");
        // Click
        if (this.name == "Start")
        {
            GC.StartGame();
        }
        else if(this.name == "Difficulty")
        {
            GC.ChangeDifficulty();
        }
        
        m_Image.color = m_HoverColor;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
