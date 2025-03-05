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
        // �Ұʨ�{�A�Ω�w���ͦ�Trail
        StartCoroutine(GenerateTrailRoutine());
    }

    IEnumerator GenerateTrailRoutine()
    {
        while (true)
        {
            // ���ݫ��w�ɶ�
            yield return new WaitForSeconds(trailInterval);

            // �ͦ����䪺Trail
            GenerateTrail(startPoint, endPointLeft);
            // �ͦ��k�䪺Trail
            GenerateTrail(startPoint, endPointRight);
            // ����Trail���ʧ������ɶ�
            yield return new WaitForSeconds(trailMoveDuration);

            // �R���e�@��Trail
            DestroyPreviousTrail();

        }
    }

    void GenerateTrail(Vector3 start, Vector3 end)
    {
        // ��Ҥ�TrailPrefab�A�]�w�_�I�M���I
        GameObject trail = Instantiate(trailPrefab, start, Quaternion.identity);

        // �ϥ�Vector3.Lerp����Trail
        StartCoroutine(MoveTrailRoutine(trail, start, end, trailMoveDuration));

        Destroy(trail, 5);
    }

    void DestroyPreviousTrail()
    {
        // �p�G���e�@��Trail�A�h�R����
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
            // �ϥ�Vector3.Lerp����Trail
            trail.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);

            // ��s�ɶ�
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // �T�OTrail�̲צb���I
        trail.transform.position = end;

        // �N��{�ޥΫO�s�A�H�K�b�U�@��Trail�ͦ��ɰ��
        currentTrailCoroutine = null;
    }
}
