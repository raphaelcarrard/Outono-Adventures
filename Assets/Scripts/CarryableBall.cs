using UnityEngine;

public abstract class CarryableBall : MonoBehaviour
{
    public abstract bool CanBePickedUp { get;  }
    public abstract bool IsBeingCarried { get;  }
    public abstract void PickUp(Transform holdPoint);
    public abstract void Drop();
    public abstract void Kick(float force);
}
