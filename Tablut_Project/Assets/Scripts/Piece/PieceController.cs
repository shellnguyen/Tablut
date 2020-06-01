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
    [SerializeField] private bool m_IsKing;
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

    public bool IsAttacker
    {
        get
        {
            return m_IsAttacker;
        }

        set
        {
            m_IsAttacker = value;
        }
    }

    public sbyte PositionOnBoard
    {
        get
        {
            return m_PositionOnBoard;
        }

        set
        {
            m_PositionOnBoard = value;
        }
    }

    public bool IsKing
    {
        get
        {
            return m_IsKing;
        }

        set
        {
            m_IsKing = value;
        }
    }

    private void Awake()
    {
        m_IsKing = false;
    }

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
        if(m_GameController.IsAttackerTurn == m_IsAttacker && !m_GameController.IsGameEnd())
        {
            if (!m_IsSelected)
            {
                if (m_GameController.CurrentSelected)
                {
                    m_GameController.CurrentSelected.IsSelected = false;
                    m_GameController.CurrentSelected = null;
                }

                m_GameController.CurrentSelected = this;
                m_GameController.CheckPossibleMove(this.m_PositionOnBoard);
            }
        }
    }

    public void MoveTo(Vector3 position, sbyte positionOnBoard)
    {
        this.transform.position = position;
        m_PositionOnBoard = positionOnBoard;

    }

    public void OnBeingCaptured()
    {
        Destroy(gameObject);
    }

    public void SetProperty(bool isAttacker, sbyte positionOnBoard, GameController controller)
    {
        this.gameObject.name = "Piece" + positionOnBoard;
        m_GameController = controller;
        m_IsAttacker = isAttacker;
        m_PositionOnBoard = positionOnBoard;
        m_Renderer = this.GetComponent<Renderer>();
        m_Renderer.material.color = IsAttacker ? Color.black : Color.white;
    }
}
