using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    [SerializeField] private GameController m_GameController;
    [SerializeField] private Renderer m_Renderer;
    [SerializeField] private Color m_PieceColor;
    [SerializeField] private sbyte m_PositionOnBoard;
    [SerializeField] private bool m_IsAttacker;
    [SerializeField] private bool m_IsSelected;

    public bool IsSelected
    {
        get
        {
            return m_IsSelected;
        }

        set
        {
            m_IsSelected = value;
        }
    }

    //[SerializeField] BitArray

    // Start is called before the first frame update
    private void Start()
    {
        m_IsSelected = false;
    }

    // Update is called once per frame
    private void Update()
    {      
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name);
        if(!m_IsSelected)
        {
            if(m_GameController.CurrentSelected)
            {
                m_GameController.CurrentSelected.IsSelected = false;
                m_GameController.CurrentSelected = null;
            }

            m_GameController.CurrentSelected = this;
        }
    }

    public void MoveTo(Vector3 position)
    {
        this.transform.position = position;
    }

    public void SetProperty(bool isAttacker, sbyte positionOnBoard)
    {
        m_IsAttacker = isAttacker;
        m_PositionOnBoard = positionOnBoard;
        m_Renderer = this.GetComponent<Renderer>();
        m_Renderer.material.color = m_IsAttacker ? Color.black : Color.white;
    }
}
