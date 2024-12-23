using System;
using UnityEngine;

public class Present : MonoBehaviour
{

    public static event EventHandler OnPresentDunked;
    public static event EventHandler OnPresentMissed;
    public void Dunk()
    {
        Debug.Log("Present Dunked");
        // Add any additional logic here if needed
        OnPresentDunked?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    public void Miss()
    {
        Debug.Log("Present Missed");
        OnPresentMissed?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
