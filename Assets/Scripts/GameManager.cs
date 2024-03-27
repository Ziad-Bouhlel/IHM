using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerControls playerControls;
    public AIControls[] aiControls;
    public LapManager lapTracker;
    public TricolorLights tricolorLights;
    public CameraFollow followPlayer;

    public AudioSource audioSource; // Reference to AudioSource
    public AudioClip lowBeep; // Low pitch sound clip
    public AudioClip highBeep; // High pitch sound clip
    public Animator cameraIntroAnimator;

    private void Awake()
    {
        StartIntro();
    }
    public void StartIntro()
    {
        followPlayer.enabled = false;
        cameraIntroAnimator.enabled = true;
        FreezePlayers(true);
    }
    public void StartCountdown()
    {
        followPlayer.enabled = true;
        cameraIntroAnimator.enabled = false;
        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {

        yield return new WaitForSeconds(1);
        Debug.Log("3");
        tricolorLights.SetProgress(1);
        // Play low pitch sound
        PlaySound(lowBeep);

        yield return new WaitForSeconds(1);
        Debug.Log("2");
        tricolorLights.SetProgress(2);
        // Play low pitch sound
        PlaySound(lowBeep);

        yield return new WaitForSeconds(1);
        Debug.Log("1");
        tricolorLights.SetProgress(3);
        // Play low pitch sound
        PlaySound(lowBeep);

        yield return new WaitForSeconds(1);
        Debug.Log("GO");
        tricolorLights.SetProgress(4);
        // Play high pitch sound
        PlaySound(highBeep);

        StartRacing();
        yield return new WaitForSeconds(2f);
        tricolorLights.SetAllLightsOff();
    }

    public void StartRacing()
    {
        FreezePlayers(false);
    }

    void FreezePlayers(bool freeze)
    {
        playerControls.enabled = !freeze;
        foreach (AIControls ai in aiControls)
        {
            ai.enabled = !freeze;
        }
    }

    void PlaySound(AudioClip clip, int times = 1)
    {
        for (int i = 0; i < times; i++)
        {
            audioSource.PlayOneShot(clip);
            // Wait for the duration of the clip before playing it again
            if (i < times - 1)
                StartCoroutine(WaitForSound(clip.length));
        }
    }

    IEnumerator WaitForSound(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
