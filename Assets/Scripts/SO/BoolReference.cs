using UnityEngine;
using System;

[Serializable]
public class BoolReference
{
    public bool UseConstant = true;
    public bool ConstantValue;
    public BoolVariable Variable;

    public bool Value{
        get{
            return UseConstant ? ConstantValue : Variable.Value;
        }set{ // make this have some security
            if (UseConstant) ConstantValue = value; // Directly modify the constant value
            else Variable.Value = value; // Modify the ScriptableObject variable
        }
    }
}
