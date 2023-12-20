using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public static event Action BulletHittedEvent;

    public static void RunBulletHittedEvent()
    {
        if(BulletHittedEvent != null)
        {
            BulletHittedEvent();
        }
    }
}
