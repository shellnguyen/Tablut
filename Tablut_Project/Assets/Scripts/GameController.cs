using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject m_MarksObject;
    [SerializeField] private GameObject m_MarkPrefab;
    [SerializeField] private GameObject[] m_MoveMarks;
    [SerializeField] private BitArray m_AttackerPos;
    [SerializeField] private BitArray m_DefenderPos;
    [SerializeField] private bool m_IsAttackerTurn;
    [SerializeField] private PieceController m_CurrentSelected;

    public PieceController CurrentSelected
    {
        get
        {
            return m_CurrentSelected;
        }

        set
        {
            m_CurrentSelected = value;
        }
    }


    // Start is called before the first frame update
    private void Awake()
    {
        m_MoveMarks = new GameObject[81];
        m_AttackerPos = new BitArray(81);
        m_DefenderPos = new BitArray(81);
        m_IsAttackerTurn = true;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void GenerateMoveMarks(int index, Vector3 position)
    {
        m_MoveMarks[index] = Instantiate(m_MarkPrefab, position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        m_MoveMarks[index].transform.SetParent(m_MarksObject.transform);
        //m_MoveMarks[index].SetActive(false);
    }
}
