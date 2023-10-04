using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.Pool;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] informaticsItems;
    [SerializeField] private GameObject[] opticsItems;
    [SerializeField] private GameObject[] electronicsItems;

    [SerializeField] private float radius;
    [SerializeField] private float speed;

    private GameObject[] currentLevelItems;
    private bool shootOutDone = false;
    // Start is called before the first frame update
    private float[] initialAngles;

    [SerializeField] private ProjectorState currentProjectorState;
    public enum Theme
    {
        Informatics, Optics, Electronics
    }
    public enum ProjectorState
    {
        Off,
        ShootOut,
        Rotate
    }
    private void Start()
    {
        currentLevelItems = new GameObject[informaticsItems.Length]; 
        for(int i=0;i<informaticsItems.Length; i++)
        {
            currentLevelItems[i] = Instantiate(informaticsItems[i],transform);
            currentLevelItems[i].SetActive(false);
        }
        initialAngles = new float[informaticsItems.Length];

    }
    private void Update()
    {
        //For Pc testing
        if (Input.GetKeyDown(KeyCode.W))
        {
            ShootOutEvent();
        }else if(Input.GetKeyDown(KeyCode.S))
        {
            ShutDownEvent();
        }

        if (shootOutDone)
        {
            transform.Rotate(Vector3.up, speed * Time.deltaTime);
        }
        
    }

    public void ActivateTheme(Theme theme)
    {
        switch (theme)
        {
            case Theme.Informatics:
                currentLevelItems = informaticsItems;
                break;
            case Theme.Optics:
                currentLevelItems = opticsItems;
                break;
            case Theme.Electronics:
                currentLevelItems = electronicsItems;
                break;
        }
   
    }

  

    private IEnumerator ShootOutAndRotate()
    {
        Debug.Log("shoot");
        float shootOutDuration = 0.5f;  // Time for the shoot out effect in seconds
        float startTime = Time.time;
        Vector3 center = transform.position;  // Replace with the actual center if different

        foreach(GameObject go in currentLevelItems)
        {
            go.SetActive(true);
        }
        float angleStep = 360f / currentLevelItems.Length;
        while (Time.time - startTime < shootOutDuration)
        {
            float t = (Time.time - startTime) / shootOutDuration;  // Normalized time
            for (int i = 0; i < currentLevelItems.Length; i++)
            {
                float angle = i * angleStep;
                Vector3 targetPos = new Vector3(
                    Mathf.Cos(Mathf.Deg2Rad * angle) * radius,
                    0,
                    Mathf.Sin(Mathf.Deg2Rad * angle) * radius
                )+ transform.position + new Vector3(0,0.5f,0);
                currentLevelItems[i].transform.position = Vector3.Lerp(
                    center,
                    targetPos,
                    t
                );
            }
            yield return null;  // Wait for the next frame
        }
        shootOutDone = true;
        for(int i=0;i<currentLevelItems.Length; i++)
        {
            currentLevelItems[i].GetComponent<FloatingBehaviour>().StartFloating((i*4)/10f);
        }
        yield return null;
       /* // Transition to your existing rotation behavior
        while (true)
        {
            ArrangeItemsInCircle();
            yield return null;  // Wait for the next frame
        }*/
    

}

    //Called by Pedestal slot, after placing the theme
    public void ShootOutEvent()
    {
        StartCoroutine(ShootOutAndRotate());
         
    }

    public void ShutDownEvent()
    {
        shootOutDone = false;
        foreach (GameObject go in currentLevelItems)
        {
            go.GetComponent<FloatingBehaviour>().StopFloating();
            go.SetActive(false);
        }
       
    }


}
