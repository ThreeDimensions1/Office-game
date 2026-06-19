using System;
using UnityEngine;
// ✅ 
[Serializable]
public class FloatReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;

    public float Value{
        get{
            return UseConstant ? ConstantValue : Variable.Value;
        }set{ // make this have some security
            if (UseConstant) ConstantValue = value; // Directly modify the constant value
            else Variable.Value = value; // Modify the ScriptableObject variable
        }
    }
    // reading without calling .Value like a slave, but its mf useless because I cant write into it cleanly.
    // overloading is page 168 in the book 😏
    public static implicit operator float(FloatReference reference) => reference.Value;
    //public static implicit operator int(FloatReference reference) => (int)reference.Value;

    public static FloatReference operator ++(FloatReference reference){
        reference.Value += 1f;
        return reference;
    }
    public static FloatReference operator --(FloatReference reference){
        reference.Value -= 1f;
        return reference;
    }
    public static FloatReference operator -(FloatReference reference, float value){
        reference.Value -= value;
        return reference;
    }
    public static FloatReference operator +(FloatReference reference, float value){
        reference.Value += value;
        return reference;
    }
    public static FloatReference operator *(FloatReference reference, float value){
        reference.Value *= value;
        return reference;
    }
    public static FloatReference operator /(FloatReference reference, float value){
        reference.Value /= value;
        return reference;
    }
}