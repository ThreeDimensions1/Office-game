using System;
using UnityEngine;

[Serializable]
public class IntReference
{
    public bool UseConstant = true;
    public int ConstantValue;
    public IntVariable Variable;

    public int Value{
        get{
            return UseConstant ? ConstantValue : Variable.Value;
        }set{ // make this have some security
            if (UseConstant) ConstantValue = value; // Directly modify the constant value
            else Variable.Value = value; // Modify the ScriptableObject variable
        }
    }
    public static implicit operator int(IntReference reference) => reference.Value;
    //public static implicit operator int(IntReference reference) => (int)reference.Value;

    public static IntReference operator ++(IntReference reference){
        reference.Value += 1;
        return reference;
    }
    public static IntReference operator --(IntReference reference){
        reference.Value -= 1;
        return reference;
    }
    public static IntReference operator -(IntReference reference, int value){
        reference.Value -= value;
        return reference;
    }
    public static IntReference operator +(IntReference reference, int value){
        reference.Value += value;
        return reference;
    }
    public static IntReference operator *(IntReference reference, int value){
        reference.Value *= value;
        return reference;
    }
    public static IntReference operator /(IntReference reference, int value){
        reference.Value /= value;
        return reference;
    }
}
