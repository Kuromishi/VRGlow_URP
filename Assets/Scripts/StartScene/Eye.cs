using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eye : MonoBehaviour
{
    private bool isWatching;
    private float zeroTimer;
    public float timer;
    private Vector3 cube;

    //void OnBecameVisible()
    //{
    //    isWatching = true;
    //    Debug.Log(isWatching);
    //}
    //void OnBecameInvisible()
    //{
    //    isWatching = false;
    //    Debug.Log(isWatching);
    //}

    private void FixedUpdate()
    {
        isInview(cube);

        if (isWatching)
        {
            zeroTimer += Time.deltaTime;
            if (zeroTimer >= timer)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (!isWatching)
        {
            zeroTimer = 0;
        }

        //Debug.Log(zeroTimer);
    }

    private bool isInview(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);

        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1) return isWatching = false;
        else return isWatching = true;
    }
}