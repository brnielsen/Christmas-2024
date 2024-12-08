using System.Collections.Generic;
using UnityEngine;

public class ChimneyManager : MonoBehaviour
{
    public static ChimneyManager Instance;

    public List<Chimney> chimneys = new List<Chimney>();

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

    public Chimney ClosestChimney(Vector3 position)
    {
        float closestDistance = Mathf.Infinity;
        Chimney closestChimney = null;
        foreach (Chimney chimney in chimneys)
        {
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
