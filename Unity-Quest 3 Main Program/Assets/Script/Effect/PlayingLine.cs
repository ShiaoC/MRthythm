using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingLine : MonoBehaviour
{
    public GameObject trailPrefab;
    Vector3 startPoint = new Vector3(0, 1.199f, 9.33f);
    Vector3 endPointLeft = new Vector3(-1.127f, 0, -2.39f);
    Vector3 endPointRight = new Vector3(1.127f, 0, -2.39f);

    public float trailInterval = 2f;
    public float trailMoveDuration = 1f;

    private IEnumerator currentTrailCoroutine;

    void Start()
    {
        // 啟動協程，用於定期生成Trail
        StartCoroutine(GenerateTrailRoutine());
    }

    IEnumerator GenerateTrailRoutine()
    {
        while (true)
        {
            // 等待指定時間
            yield return new WaitForSeconds(trailInterval);

            // 生成左邊的Trail
            GenerateTrail(startPoint, endPointLeft);
            // 生成右邊的Trail
            GenerateTrail(startPoint, endPointRight);
            // 等待Trail移動完成的時間
            yield return new WaitForSeconds(trailMoveDuration);

            // 刪除前一個Trail
            DestroyPreviousTrail();

        }
    }

    void GenerateTrail(Vector3 start, Vector3 end)
    {
        // 實例化TrailPrefab，設定起點和終點
        GameObject trail = Instantiate(trailPrefab, start, Quaternion.identity);

        // 使用Vector3.Lerp移動Trail
        StartCoroutine(MoveTrailRoutine(trail, start, end, trailMoveDuration));

        Destroy(trail, 5);
    }

    void DestroyPreviousTrail()
    {
        // 如果有前一個Trail，則刪除它
        if (currentTrailCoroutine != null)
        {
            StopCoroutine(currentTrailCoroutine);
        }
    }

    IEnumerator MoveTrailRoutine(GameObject trail, Vector3 start, Vector3 end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 使用Vector3.Lerp移動Trail
            trail.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);

            // 更新時間
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 確保Trail最終在終點
        trail.transform.position = end;

        // 將協程引用保存，以便在下一個Trail生成時停止它
        currentTrailCoroutine = null;
    }
}
