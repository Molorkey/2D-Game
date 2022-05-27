using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroy : MonoBehaviour
{
    public void DestroyGameObject()
    {
        Destroy(this.gameObject);

    }
}
