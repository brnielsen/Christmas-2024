using System;
using UnityEngine;

public class PresentManager : MonoBehaviour
{
    public static PresentManager Instance { get; private set; }

    public GameObject presentPrefab;

    public Transform presentSpawnPoint;

    private Present activePresent;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Present.OnPresentDunked += Present_OnPresentDunked;
        Present.OnPresentMissed += Present_OnPresentMissed;
    }

    private void Present_OnPresentMissed(object sender, EventArgs e)
    {
        SpawnPresent();
    }

    private void Present_OnPresentDunked(object sender, EventArgs e)
    {
        SpawnPresent();
    }

    public void SpawnPresent()
    {
        GameObject present = Instantiate(presentPrefab, presentSpawnPoint.position, Quaternion.identity);
        activePresent = present.GetComponent<Present>();        
    }

    public void LaunchPresent()
    {
        if (activePresent != null)
        {
            activePresent.GetComponent<ArcMover>().LaunchToTarget();
        }
    }


}
