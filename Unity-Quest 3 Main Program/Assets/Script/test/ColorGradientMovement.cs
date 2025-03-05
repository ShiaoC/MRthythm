using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGradientMovement : MonoBehaviour
{
    public Color[] gradientColors; // �C�⺥�ܰ}�C
    public float moveSpeed = 1.0f; // ���ʳt��

    private SpriteRenderer spriteRenderer;
    private int currentColorIndex = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �]�m��l�C��
        if (gradientColors.Length > 0)
        {
            spriteRenderer.color = gradientColors[currentColorIndex];
        }
    }

    void Update()
    {
        // ����
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // �P�_�O�_��F�U�@���C��
        if (Mathf.Abs(transform.position.x) >= 1.0f)
        {
            // ����U�@���C��
            currentColorIndex = (currentColorIndex + 1) % gradientColors.Length;
            spriteRenderer.color = gradientColors[currentColorIndex];

            // ���m��m
            transform.position = new Vector2(-Mathf.Sign(transform.position.x), 0.0f);
        }
    }
}
