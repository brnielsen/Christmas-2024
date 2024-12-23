using System.Collections.Generic;
using UnityEngine;

public class ChimneyManager : MonoBehaviour
{
    public static ChimneyManager Instance;

    public List<Chimney> chimneys = new List<Chimney>();

    public PlayerController PlayerController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerController == null)
        {
            PlayerController = FindAnyObjectByType<PlayerController>();

        }
    }

    public Chimney ClosestChimney(Vector3 position)
    {
        float closestDistance = Mathf.Infinity;
        Chimney closestChimney = null;
        foreach (Chimney chimney in chimneys)
        {

            if (chimney.GetComponentInChildren<SpriteRenderer>().isVisible == false || (chimney.transform.position.x + chimney.DunkRadius) < PlayerController.transform.position.x)
            {
                continue;
            }

            float distance = Vector3.Distance(position, chimney.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestChimney = chimney;
            }
        }
        return closestChimney;
    }
}
