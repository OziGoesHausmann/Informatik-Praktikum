using UnityEngine;
using UnityEngine.UI;

public class ScreenWipe : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 3f)]
    private float wipeSpeed;

    private Image image;
    private GarbagePile garbagePile;
    private enum WipeMode { NotBlocked, WipingToNotBlocked, Blocked, WipingToBlocked }

    private WipeMode wipeMode = WipeMode.NotBlocked;

    private float wipeProgress;
    private float unWipeProgress;

    public bool isDone;

    // NEU
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(gameObject);
    }

    public void ToggleWipe(bool blockScreen)
    {
        isDone = false;
        if (blockScreen)
            wipeMode = WipeMode.WipingToBlocked;
        else
            wipeMode = WipeMode.WipingToNotBlocked;
    }

    private void Update()
    {
        switch (wipeMode)
        {
            case WipeMode.WipingToBlocked:
                WipeToBlocked();
                break;
            case WipeMode.WipingToNotBlocked:
                WipeToNotBlocked();
                break;
            // NEU
            case WipeMode.NotBlocked:
                canvasGroup.blocksRaycasts = false;
                break;
        }

        if (isDone)
        {
            Invoke("ToggleFalse", 1f + wipeSpeed);
            Invoke("RearangeCam", 1f + wipeSpeed);
            isDone = false;
        }
    }

    private void WipeToBlocked()
    {
        wipeProgress += Time.deltaTime * (1f / wipeSpeed);
        image.fillAmount = wipeProgress;
        if (wipeProgress >= 1f)
        {
            //isDone = true;
            wipeMode = WipeMode.Blocked;
        }
        // NEU
        canvasGroup.blocksRaycasts = true;
    }



    private void WipeToNotBlocked()
    {
        wipeProgress -= Time.deltaTime * (1f / wipeSpeed);
        image.fillAmount = wipeProgress;
        if (wipeProgress <= 0)
        {
            //isDone = true;
            wipeMode = WipeMode.NotBlocked;
        }
    }

    void ToggleFalse()
    {
        ToggleWipe(false);
    }

    void RearangeCam()
    {
        Camera.main.transform.position = new Vector3(0, 1, -10);
        Camera.main.GetComponent<Camera>().orthographicSize = 9;
    }



    [ContextMenu("Block")]
    private void Block() { ToggleWipe(true); }
    [ContextMenu("Clear")]
    private void Clear() { ToggleWipe(false); }



}