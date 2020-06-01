using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private GameController m_GameController;
    [SerializeField] private float m_Size;

    private void Start()
    {
        m_Size = 1.0f;
        GenerateGrid();
        m_GameController.GeneretePieces();
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name);
        if(m_GameController.CurrentSelected)
        {
            RaycastHit hit;
            if (Utilities.Instance.GetRaycastHit(Camera.main, Input.mousePosition, out hit))
            {
                m_GameController.MovePiece(GetNearestPointOnGrid(hit.point));
            }
        }
    }

    public Vector3 GetNearestPointOnGrid(Vector3 target)
    {
        target -= transform.position;

        int xCount = Mathf.RoundToInt(target.x / m_Size);
        int yCount = Mathf.RoundToInt(target.y / m_Size);
        int zCount = Mathf.RoundToInt(target.z / m_Size);

        Vector3 result = new Vector3((float)xCount * m_Size, target.y, (float)zCount * m_Size);

        result += transform.position;

        return result;
    }

    private void GenerateGrid()
    {
        int index = 0;
        for (float x = 0; x > -9; x -= m_Size)
        {
            for (float z = -8; z > -17; z -= m_Size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0.2f, z));
                m_GameController.GenerateMoveMarks(index, point);
                index++;
            }
        }
    }
}
