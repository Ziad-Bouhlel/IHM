using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LapManager : MonoBehaviour
{
    public List<Checkpoint> checkpoints;
    public int totalLaps = 3;
    private Dictionary<CarIdentity, int> carLaps = new Dictionary<CarIdentity, int>();
    private Dictionary<CarIdentity, int> carLastCheckpointIndex = new Dictionary<CarIdentity, int>();
    public UIManager uiManager;

    public UnityEvent onPlayerFinished = new UnityEvent();

    void Start()
    {
        ListenCheckpoints(true);
    }

    private void ListenCheckpoints(bool subscribe)
    {
        if (checkpoints == null)
        {
            Debug.LogError("Checkpoints list is null!");
            return;
        }
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (subscribe) checkpoint.onCheckpointEnter.AddListener(CheckpointActivated);
            else checkpoint.onCheckpointEnter.RemoveListener(CheckpointActivated);
        }
    }

    public void CheckpointActivated(GameObject carObject, Checkpoint checkpoint)
    {
        CarIdentity carIdentity = carObject.GetComponent<CarIdentity>();
        if (carIdentity == null)
        {
            Debug.LogError("CarIdentity component not found on car object: " + carObject.name);
            return;
        }

        if (!carLaps.ContainsKey(carIdentity))
        {
            carLaps.Add(carIdentity, 0);
            carLastCheckpointIndex.Add(carIdentity, -1);
        }

        int checkpointNumber = checkpoints.IndexOf(checkpoint);
        int lastCheckpointIndex = carLastCheckpointIndex[carIdentity];
        bool startingFirstLap = checkpointNumber == 0 && lastCheckpointIndex == -1;
        bool lapIsFinished = checkpointNumber == 0 && lastCheckpointIndex >= checkpoints.Count - 1;

        if (startingFirstLap || lapIsFinished)
        {
            carLaps[carIdentity]++;
            carLastCheckpointIndex[carIdentity] = 0;

            if (carLaps[carIdentity] > totalLaps)
            {
                Debug.Log(carIdentity.carName + " won!");
                uiManager.UpdateLapText("Game Over! " + carIdentity.carName + " won!");
                // Disable all checkpoints
                ListenCheckpoints(false);

                // Appel de l'événement onPlayerFinished après un délai de 5 secondes
                StartCoroutine(DelayedFinishEvent());
            }
            else
            {
                Debug.Log(carIdentity.carName + " : Lap " + carLaps[carIdentity]);
                string lapInfo = "";

                // Iterate over the carLaps dictionary and append lap information for each car
                foreach (var kvp in carLaps)
                {
                    lapInfo += kvp.Key.carName + " : Lap " + kvp.Value + "/3" + "\n";
                }

                // Update the lap text with the formatted lap information
                uiManager.UpdateLapText(lapInfo);
            }
        }
        else if (checkpointNumber == lastCheckpointIndex + 1)
        {
            carLastCheckpointIndex[carIdentity]++;
        }
    }

    IEnumerator DelayedFinishEvent()
    {
        yield return new WaitForSeconds(5f); // Attendre 5 secondes
        onPlayerFinished.Invoke(); // Appeler l'événement onPlayerFinished
    }
}
