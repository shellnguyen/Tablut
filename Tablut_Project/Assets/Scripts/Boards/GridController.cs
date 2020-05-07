using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private float m_Size = 1.0f;

    private void OnAwake()
    {
        m_Size = 1.0f;
    }

    public Vector3 GetNearestPointOnGrid(Vector3 target)
    {
        target -= transform.position;

        int xCount = Mathf.RoundToInt(target.x / m_Size);
        //int yCount = Mathf.RoundToInt(target.y / m_Size);
        int zCount = Mathf.RoundToInt(target.z / m_Size);

        Vector3 result = new Vector3((float)xCount * m_Size, 0.8f, (float)zCount * m_Size);

        result += transform.position;

        return result;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;

    //    for (float x = -2; x > -11; x -= m_Size)
    //    {
    //        for (float z = -8; z > -15; z -= m_Size)
    //        {
    //            var point = GetNearestPointOnGrid(new Vector3(x, 1f, z));
    //            Gizmos.DrawSphere(point, 0.1f);
    //        }
    //    }
    //}
}
