using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    [SerializeField] private Renderer m_Renderer;
    [SerializeField] private Color m_PieceColor;

    // Start is called before the first frame update
    private void Start()
    {
        m_Renderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        m_Renderer.material.color = m_PieceColor;
    }
}
