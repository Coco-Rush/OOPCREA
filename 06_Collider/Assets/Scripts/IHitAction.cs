using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitAction
{
    void Impact();
    void Impact(int collisionSpeed);
    void Impact(float collisionSpeed);
}
