using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    private void OnDestroy()
    {
        EventMessenger.TriggerEvent("PlayerProjectileDestroyed");
    }
}
