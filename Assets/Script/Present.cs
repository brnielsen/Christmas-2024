using System;
using UnityEngine;

public class Present : MonoBehaviour
{
    public void Dunk()
    {
        Debug.Log("Present Dunked");
        // Add any additional logic here if needed
        Destroy(gameObject);
    }


}
