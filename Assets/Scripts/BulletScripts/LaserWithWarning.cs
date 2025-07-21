using UnityEngine;

public class LaserWithWarning : MonoBehaviour
{
    [SerializeField] private GameObject leftWarningLine;
    [SerializeField] private GameObject rightWarningLine;
    [SerializeField] private GameObject topWarningLine;
    [SerializeField] private GameObject bottomWarningLine;
    [SerializeField] private GameObject actualLaser;

    [SerializeField] float startTime = 1f;
    [SerializeField] float growDuration = 1f;
    [SerializeField] float maxHeight = 10f;

    private float timer = 0f;
    private bool laserStarted = false;
    private bool laserFinished = false;

    void Start()
    {
        actualLaser.transform.localScale = new Vector3(1f, 0f, 1f);
        actualLaser.SetActive(false);
        SoundManager.instance.playLaserSound();
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (!laserStarted && timer >= startTime)
        {
            actualLaser.SetActive(true);

            leftWarningLine.SetActive(false);
            rightWarningLine.SetActive(false);
            topWarningLine.SetActive(false);
            bottomWarningLine.SetActive(false);

            laserStarted = true;
            timer = 0f;
        }

        if (laserStarted && !laserFinished)
        {
            float t = Mathf.Clamp01(timer / growDuration);
            actualLaser.transform.localScale = new Vector3(1f, t * maxHeight, 1f);

            if (t >= 1f)
            {
                laserFinished = true;
            }
        }
        if (laserFinished)
        {
            Destroy(gameObject);
        }
    }
}
