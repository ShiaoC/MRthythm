using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGradientMovement : MonoBehaviour
{
    public Color[] gradientColors; // 顏色漸變陣列
    public float moveSpeed = 1.0f; // 移動速度

    private SpriteRenderer spriteRenderer;
    private int currentColorIndex = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 設置初始顏色
        if (gradientColors.Length > 0)
        {
            spriteRenderer.color = gradientColors[currentColorIndex];
        }
    }

    void Update()
    {
        // 移動
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // 判斷是否到達下一個顏色
        if (Mathf.Abs(transform.position.x) >= 1.0f)
        {
            // 換到下一個顏色
            currentColorIndex = (currentColorIndex + 1) % gradientColors.Length;
            spriteRenderer.color = gradientColors[currentColorIndex];

            // 重置位置
            transform.position = new Vector2(-Mathf.Sign(transform.position.x), 0.0f);
        }
    }
}
