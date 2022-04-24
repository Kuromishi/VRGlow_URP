using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eye : MonoBehaviour
{
    private float zeroTimer;
    public float timer;
    private Vector3 cube;

    private void FixedUpdate()
    {
        if (isInview(cube))
        {
            zeroTimer += Time.deltaTime;
            if (zeroTimer >= timer)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (!isInview(cube))
        {
            zeroTimer = 0;
        }
    }

    private bool isInview(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);

        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1) return true;
        else return false;
    }
}