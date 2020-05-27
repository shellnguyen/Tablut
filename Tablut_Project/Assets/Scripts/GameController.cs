using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Mark object
    [SerializeField] private GameObject m_MarksObject;
    [SerializeField] private GameObject m_MarkPrefab;
    [SerializeField] private GameObject[] m_MoveMarks;
    [SerializeField] private List<sbyte> m_CurrentActiveMarks;
    //

    //Piece object
    [SerializeField] private GameObject m_PiecePrefab;
    [SerializeField] private GameObject m_PieceObjects;
    [SerializeField] private Dictionary<sbyte, PieceController> m_Pieces;
    //

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
        m_CurrentActiveMarks = new List<sbyte>();
        m_AttackerPos = new BitArray(81, false);
        m_DefenderPos = new BitArray(81, false);
        m_Pieces = new Dictionary<sbyte, PieceController>(25);
        m_CurrentSelected = null;
        m_IsAttackerTurn = true;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    #region Generate functions
    public void GeneretePieces()
    {
        for(int i = 3; i <= 5; ++i)
        {
            if(i % 2 == 0) //Center Attacker Pieces
            {
                //Top
                GameObject attacker = Instantiate(m_PiecePrefab, m_MoveMarks[i].transform.position, Quaternion.identity, m_PieceObjects.transform);
                m_Pieces.Add((sbyte)i, attacker.GetComponent<PieceController>());
                m_Pieces[(sbyte)i].SetProperty(true, (sbyte)i, this);
                
                m_AttackerPos.Set(i, true);

                //Left
                attacker = Instantiate(m_PiecePrefab, m_MoveMarks[i * 9].transform.position, Quaternion.identity, m_PieceObjects.transform);
                m_Pieces.Add((sbyte)(i * 9), attacker.GetComponent<PieceController>());
                m_Pieces[(sbyte)(i * 9)].SetProperty(true, (sbyte)(i * 9), this);
                m_AttackerPos.Set(i * 9, true);

                for (int j = 1; j < 9; ++j)
                {
                    //column offser
                    int topOffset = i + (9 * j);
                    int leftOffset = (i * 9) + j;
                    if (j == 4)
                    {
                        //Center pieces
                        GameObject centerPiece = Instantiate(m_PiecePrefab, m_MoveMarks[topOffset].transform.position, Quaternion.identity, m_PieceObjects.transform);
                        m_Pieces.Add((sbyte)(topOffset), centerPiece.GetComponent<PieceController>());
                        m_Pieces[(sbyte)(topOffset)].SetProperty(false, (sbyte)(topOffset), this);
                        m_Pieces[(sbyte)(topOffset)].IsKing = true;
                        continue;
                    }
                    //Top Center Column
                    GameObject piece1 = Instantiate(m_PiecePrefab, m_MoveMarks[topOffset].transform.position, Quaternion.identity, m_PieceObjects.transform);
                    m_Pieces.Add((sbyte)(topOffset), piece1.GetComponent<PieceController>());

                    //Left Center Column
                    GameObject piece2 = Instantiate(m_PiecePrefab, m_MoveMarks[leftOffset].transform.position, Quaternion.identity, m_PieceObjects.transform);
                    m_Pieces.Add((sbyte)(leftOffset), piece2.GetComponent<PieceController>());

                    if (j == 8 || j == 1 || j == 7)
                    {
                        //Set pieces as Attacker
                        m_Pieces[(sbyte)(topOffset)].SetProperty(true, (sbyte)topOffset, this);
                        m_AttackerPos.Set(topOffset, true);
                        m_Pieces[(sbyte)(leftOffset)].SetProperty(true, (sbyte)leftOffset, this);
                        m_AttackerPos.Set(leftOffset, true);
                    }
                    else
                    {
                        //Set pieces as Defender
                        m_Pieces[(sbyte)(topOffset)].SetProperty(false, (sbyte)(topOffset), this);
                        m_DefenderPos.Set(topOffset, true);
                        m_Pieces[(sbyte)(leftOffset)].SetProperty(false, (sbyte)(leftOffset), this);
                        m_DefenderPos.Set(leftOffset, true);
                    }
                }
            }
            else
            {
                //Generate Attacker
                //Top
                GameObject piece = Instantiate(m_PiecePrefab, m_MoveMarks[i].transform.position, Quaternion.identity, m_PieceObjects.transform);
                m_Pieces.Add((sbyte)i, piece.GetComponent<PieceController>());
                m_Pieces[(sbyte)i].SetProperty(true, (sbyte)i, this);
                m_AttackerPos.Set(i, true);

                //Opposite (bottom)
                piece = Instantiate(m_PiecePrefab, m_MoveMarks[i + 72].transform.position, Quaternion.identity, m_PieceObjects.transform);
                m_Pieces.Add((sbyte)(i + 72), piece.GetComponent<PieceController>());
                m_Pieces[(sbyte)(i + 72)].SetProperty(true, (sbyte)(i + 72), this);
                m_AttackerPos.Set(i + 72, true);

                //Left
                piece = Instantiate(m_PiecePrefab, m_MoveMarks[i * 9].transform.position, Quaternion.identity, m_PieceObjects.transform);
                m_Pieces.Add((sbyte)(i * 9), piece.GetComponent<PieceController>());
                m_Pieces[(sbyte)(i * 9)].SetProperty(true, (sbyte)(i * 9), this);
                m_AttackerPos.Set(i * 9, true);

                //Right
                piece = Instantiate(m_PiecePrefab, m_MoveMarks[i * 9 + 8].transform.position, Quaternion.identity, m_PieceObjects.transform);
                m_Pieces.Add((sbyte)(i * 9 + 8), piece.GetComponent<PieceController>());
                m_Pieces[(sbyte)(i * 9 + 8)].SetProperty(true, (sbyte)(i * 9 + 8), this);
                m_AttackerPos.Set(i * 9 + 8, true);

                //
            }
        }

        //Set both bitboard of Attacker and Defender center cell to 1 since they can use center piece to corner and capture enemy
        //m_AttackerPos.Set(40, true);
        m_DefenderPos.Set(40, true);
    }

    public void GenerateMoveMarks(int index, Vector3 position)
    {
        m_MoveMarks[index] = Instantiate(m_MarkPrefab, position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        m_MoveMarks[index].transform.SetParent(m_MarksObject.transform);
        m_MoveMarks[index].SetActive(false);
    }
    #endregion

    #region Check possible moves functions
    public void CheckPossibleMove(sbyte position)
    {
        //Hide old possible moves
        RemoveOldPossibleMoves();

        //Check move
        Debug.Log("m_AttackerPos");
        Utilities.Instance.PrintBitArray(m_AttackerPos);
        Debug.Log("m_DefenderPos");
        Utilities.Instance.PrintBitArray(m_DefenderPos);
        BitArray allPieces = new BitArray(m_AttackerPos);
        allPieces.Or(m_DefenderPos);
        Debug.Log("allPieces");
        Utilities.Instance.PrintBitArray(allPieces);

        TraverseLeft((sbyte)(position - 1), allPieces);
        TraverseRight((sbyte)(position + 1), allPieces);
        TraverseTop((sbyte)(position - 9), allPieces);
        TraverseDown((sbyte)(position + 9), allPieces);
    }

    private void RemoveOldPossibleMoves()
    {
        for (int i = 0; i < m_CurrentActiveMarks.Count; ++i)
        {
            m_MoveMarks[m_CurrentActiveMarks[i]].SetActive(false);
        }
        m_CurrentActiveMarks.Clear();
    }

    private sbyte TraverseLeft(sbyte nextLeftPos, BitArray bitboard)
    {
        if((nextLeftPos + 1) % 9 == 0)
        {
            return 0;
        }

        if(nextLeftPos < 0)
        {
            return 0;
        }

        if(nextLeftPos % 9 == 0)
        {
            m_MoveMarks[nextLeftPos].SetActive(true);
            m_CurrentActiveMarks.Add(nextLeftPos);
            return 0;
        }

        if(bitboard.Get(nextLeftPos))
        {
            return 0;
        }

        m_MoveMarks[nextLeftPos].SetActive(true);

        m_CurrentActiveMarks.Add(nextLeftPos);
        return TraverseLeft((sbyte)(nextLeftPos - 1), bitboard);
    }

    private sbyte TraverseRight(sbyte nextRightPos, BitArray bitboard)
    {
        if (nextRightPos % 9 == 0 || nextRightPos > 80)
        {
            return 0;
        }

        if (bitboard.Get(nextRightPos))
        {
            return 0;
        }

        m_MoveMarks[nextRightPos].SetActive(true);

        m_CurrentActiveMarks.Add(nextRightPos);
        return TraverseRight((sbyte)(nextRightPos + 1), bitboard);
    }

    private sbyte TraverseTop(sbyte nextTopPos, BitArray bitboard)
    {
        if (nextTopPos < 0)
        {
            return 0;
        }

        if (bitboard.Get(nextTopPos))
        {
            return 0;
        }

        m_MoveMarks[nextTopPos].SetActive(true);
        m_CurrentActiveMarks.Add(nextTopPos);
        return TraverseTop((sbyte)(nextTopPos - 9), bitboard);
    }

    private sbyte TraverseDown(sbyte nextDownPos, BitArray bitboard)
    {
        if (nextDownPos > 80)
        {
            return 0;
        }

        if (bitboard.Get(nextDownPos))
        {
            return 0;
        }

        m_MoveMarks[nextDownPos].SetActive(true);
        m_CurrentActiveMarks.Add(nextDownPos);
        return TraverseDown((sbyte)(nextDownPos + 9), bitboard);
    }
    #endregion

    public void MovePiece(Vector3 newPosition)
    {
        int row = Mathf.RoundToInt(newPosition.x);
        int columns = Mathf.RoundToInt(newPosition.z + 8); //Because in world pos, columns start at -8

        sbyte index = (sbyte)((row * 9 + columns) * (-1));
        if(m_CurrentActiveMarks.Contains(index) && index != 40) //
        {
            if(m_CurrentSelected.IsAttacker)
            {
                m_AttackerPos.Set(m_CurrentSelected.PositionOnBoard, false);
                m_AttackerPos.Set(index, true);
            }
            else
            {
                if(m_CurrentSelected.IsKing)
                {
                    m_AttackerPos.Set(40, true);
                }
                m_DefenderPos.Set(m_CurrentSelected.PositionOnBoard, false);
                m_DefenderPos.Set(index, true);
            }

            m_Pieces.Remove(m_CurrentSelected.PositionOnBoard);
            m_CurrentSelected.MoveTo(newPosition, index);
            m_Pieces.Add(index, m_CurrentSelected);
        }

        RemoveOldPossibleMoves();
        CheckCapturePiece(index);
        m_CurrentSelected.IsSelected = false;
        m_CurrentSelected = null;
    }

    private void CheckCapturePiece(sbyte position)
    {
        //Debug.Log("m_AttackerPos");
        //Utilities.Instance.PrintBitArray(m_AttackerPos);
        //Debug.Log("m_DefenderPos");
        //Utilities.Instance.PrintBitArray(m_DefenderPos);
        BitArray friendlyBoards;
        BitArray enemyBoards;
        if (m_CurrentSelected.IsAttacker)
        {
            friendlyBoards = m_AttackerPos;
            enemyBoards = m_DefenderPos;
        }
        else
        {
            friendlyBoards = m_DefenderPos;
            enemyBoards = m_AttackerPos;
        }

        //Debug.Log("FriendlyBoard");
        //Utilities.Instance.PrintBitArray(friendlyBoards);
        //Debug.Log("EnemyBoard");
        //Utilities.Instance.PrintBitArray(enemyBoards);

        //Check Top
        if ((position - 18 > 0) && enemyBoards.Get(position - 9) && friendlyBoards.Get(position - 18))
        {
            enemyBoards.Set(position - 9, false);
            m_Pieces[(sbyte)(position - 9)].OnBeingCaptured();
            m_Pieces.Remove((sbyte)(position - 9));
        }

        //Check Down
        if((position + 18 <= 80) && enemyBoards.Get(position + 9) && friendlyBoards.Get(position + 18))
        {
            enemyBoards.Set(position + 9, false);
            m_Pieces[(sbyte)(position + 9)].OnBeingCaptured();
            m_Pieces.Remove((sbyte)(position + 9));
        }

        //Check Left
        if(((position - 1) % 9 != 0) && enemyBoards.Get(position - 1) && friendlyBoards.Get(position - 2))
        {
            enemyBoards.Set(position - 1, false);
            m_Pieces[(sbyte)(position - 1)].OnBeingCaptured();
            m_Pieces.Remove((sbyte)(position - 1));
        }

        //Check Right
        if (((position + 2) % 9 != 0) && enemyBoards.Get(position + 1) && friendlyBoards.Get(position + 2))
        {
            enemyBoards.Set(position + 1, false);
            m_Pieces[(sbyte)(position + 1)].OnBeingCaptured();
            m_Pieces.Remove((sbyte)(position + 1));
        }
    }
}
