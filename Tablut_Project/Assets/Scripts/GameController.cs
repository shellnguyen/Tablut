﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Mark object
    [SerializeField] private GameObject m_MarksObject;
    [SerializeField] private GameObject m_MarkPrefab;
    [SerializeField] private GameObject[] m_MoveMarks;
    //

    //Piece object
    [SerializeField] private GameObject m_PiecePrefab;
    [SerializeField] private GameObject m_PieceObjects;
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
        m_AttackerPos = new BitArray(81);
        m_DefenderPos = new BitArray(81);
        m_CurrentSelected = null;
        m_IsAttackerTurn = true;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void GeneretePieces()
    {
        for(int i = 3; i <= 5; ++i)
        {
            if(i % 2 == 0) //Center Attacker Pieces
            {
                //Top
                GameObject attacker = Instantiate(m_PiecePrefab, m_MoveMarks[i].transform.position, Quaternion.identity, m_PieceObjects.transform);
                attacker.GetComponent<PieceController>().SetProperty(true, (sbyte)i, this);
                m_AttackerPos.Set(i, true);

                //Left
                attacker = Instantiate(m_PiecePrefab, m_MoveMarks[i * 9].transform.position, Quaternion.identity, m_PieceObjects.transform);
                attacker.GetComponent<PieceController>().SetProperty(true, (sbyte)i, this);
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
                        centerPiece.GetComponent<PieceController>().SetProperty(false, (sbyte)(topOffset), this);
                        continue;
                    }
                    //Top Center Column
                    GameObject piece1 = Instantiate(m_PiecePrefab, m_MoveMarks[topOffset].transform.position, Quaternion.identity, m_PieceObjects.transform);

                    //Left Center Column
                    GameObject piece2 = Instantiate(m_PiecePrefab, m_MoveMarks[leftOffset].transform.position, Quaternion.identity, m_PieceObjects.transform);

                    if (j == 8 || j == 1)
                    {
                        //Set pieces as Attacker
                        piece1.GetComponent<PieceController>().SetProperty(true, (sbyte)topOffset, this);
                        m_AttackerPos.Set(topOffset, true);
                        piece2.GetComponent<PieceController>().SetProperty(true, (sbyte)leftOffset, this);
                        m_AttackerPos.Set(leftOffset, true);
                    }
                    else
                    {
                        //Set pieces as Defender
                        piece1.GetComponent<PieceController>().SetProperty(false, (sbyte)(topOffset), this);
                        m_DefenderPos.Set(topOffset, true);
                        piece2.GetComponent<PieceController>().SetProperty(false, (sbyte)(leftOffset), this);
                        m_DefenderPos.Set(leftOffset, true);
                    }
                }
            }
            else
            {
                //Generate Attacker
                //Top
                GameObject piece = Instantiate(m_PiecePrefab, m_MoveMarks[i].transform.position, Quaternion.identity, m_PieceObjects.transform);
                piece.GetComponent<PieceController>().SetProperty(true, (sbyte)i, this);
                m_AttackerPos.Set(i, true);

                //Opposite (bottom)
                piece = Instantiate(m_PiecePrefab, m_MoveMarks[i + 72].transform.position, Quaternion.identity, m_PieceObjects.transform);
                piece.GetComponent<PieceController>().SetProperty(true, (sbyte)(i + 72), this);
                m_AttackerPos.Set(i + 72, true);

                //Left
                piece = Instantiate(m_PiecePrefab, m_MoveMarks[i * 9].transform.position, Quaternion.identity, m_PieceObjects.transform);
                piece.GetComponent<PieceController>().SetProperty(true, (sbyte)(i * 9), this);
                m_AttackerPos.Set(i * 9, true);

                //Right
                piece = Instantiate(m_PiecePrefab, m_MoveMarks[i * 9 + 8].transform.position, Quaternion.identity, m_PieceObjects.transform);
                piece.GetComponent<PieceController>().SetProperty(true, (sbyte)(i * 9 + 8), this);
                m_AttackerPos.Set(i * 9 + 8, true);

                //
            }
        }
    }

    public void GenerateMoveMarks(int index, Vector3 position)
    {
        m_MoveMarks[index] = Instantiate(m_MarkPrefab, position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        m_MoveMarks[index].transform.SetParent(m_MarksObject.transform);
        //m_MoveMarks[index].SetActive(false);
    }

    public void CheckPossibleMove(sbyte position)
    {
        
    }
}
