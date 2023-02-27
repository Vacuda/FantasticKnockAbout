using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ot_OBJECTTYPE;

public class Spike : MonoBehaviour, ICanCollideWith
{
    public ot_OBJECTTYPE object_type { get; set; } = ot_SPIKE;
}
