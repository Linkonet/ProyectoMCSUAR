using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    [SerializeField] public float slowMotionFactor = 0.3f; // Slow motion intensity (0.1-1)
    [SerializeField] private float slowMotionDuration = 3f; // Duration in seconds

    private float originalTimeScale; // Store original time scale for restoration
    private Coroutine slowMotionCoroutine; // Coroutine for managing slow motion

    public bool timeslowed = false;

    private float rechargeTime = 10f;
    private float rechargeElapsed; 

    private bool coroutinefinished = false;

    void Start()
    {
        rechargeElapsed = rechargeTime;// Start fully recharged
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && slowMotionCoroutine == null && rechargeElapsed >= rechargeTime)
        {
            // Start slow motion coroutine
            StartCoroutine(SlowMotionCoroutine());
            timeslowed = true;
            rechargeElapsed = 0;
            coroutinefinished = false;
        }

        if(coroutinefinished)
        {
            rechargeElapsed += Time.deltaTime; // Smoothly recharge over time
        }



        //Debug.Log(rechargeElapsed);
    }

    IEnumerator SlowMotionCoroutine()
    {
        originalTimeScale = Time.timeScale; // Store original time scale
        Debug.Log("SlowTime Start");

        // Gradually slow down time (optional)
        for (float t = 0; t < slowMotionDuration / 2; t += Time.deltaTime)
        {
            Time.timeScale = Mathf.Lerp(originalTimeScale, slowMotionFactor, t / (slowMotionDuration / 2));
            yield return null;
        }

        // Maintain constant slow motion
        yield return new WaitForSeconds(slowMotionDuration / 2);

        // Gradually restore time scale (optional)
        for (float t = 0; t < slowMotionDuration / 2; t += Time.deltaTime)
        {
            Time.timeScale = Mathf.Lerp(slowMotionFactor, originalTimeScale, t / (slowMotionDuration / 2));
            yield return null;
        }

        // Reset time scale and coroutine
        Time.timeScale = originalTimeScale;
        slowMotionCoroutine = null;
        timeslowed = false;
        coroutinefinished = true;
    }

    void FixedUpdate()
    {
       
        
    }
}
