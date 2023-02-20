using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAppear : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float interactRadius;

    private float initialFontSize;

    private void Awake()
    {
        initialFontSize = GetComponent<TextMeshPro>().fontSize;
        GetComponent<TextMeshPro>().fontSize = 0f;
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, interactRadius, playerLayer))
        {
            GetComponent<TextMeshPro>().fontSize = initialFontSize;
        }
        else 
        {
            GetComponent<TextMeshPro>().fontSize = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
