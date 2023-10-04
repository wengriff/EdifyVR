using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float lerpTime;

    private Vector3 startPos;
    private bool readyToFloat=false;
    private float currentLerpTime = 0f;
    private float sineOffset;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToFloat)
        {
            if (readyToFloat)
            {
                if (currentLerpTime < lerpTime)
                {
                    currentLerpTime += Time.deltaTime;
                }

                float t = currentLerpTime / lerpTime;
                t = Mathf.Clamp01(t);

                float desiredY = startPos.y + range * Mathf.Sin((Time.time + sineOffset) * speed);
                float newY = Mathf.Lerp(transform.position.y, desiredY, t);

                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
        
    }

    public void StartFloating(float offset)
    {
        readyToFloat = true;
        startPos = transform.position;
        currentLerpTime = 0f;
        sineOffset = offset;
        Debug.Log("offset: " + offset);
    }

    public void StopFloating()
    {
        readyToFloat = true;
    }
}
