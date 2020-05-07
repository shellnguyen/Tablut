using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    [SerializeField] private GridController m_Grid;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 position = Utilities.Instance.GetWorldPosition(Camera.main, Input.mousePosition);

            if(position != Vector3.zero)
            {
                this.transform.position = m_Grid.GetNearestPointOnGrid(position);
            }
        }
    }
}
