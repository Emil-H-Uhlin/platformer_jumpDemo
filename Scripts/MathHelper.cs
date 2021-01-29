using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Help-class with useful mathematical formulas
/// </summary>
public static class MathHelper {
    /// <summary>
    /// Gets a scalar of a value given a certain range.
    /// A value of 0.5 with lower and upper bounds 0 and 1 respectively, returns 0.5
    /// </summary>
    /// <param name="_value">The value</param>
    /// <param name="_begin">Lower bound</param>
    /// <param name="_end">Upper bound</param>
    /// <returns>A scalar fraction of the value within the given range.</returns>
    public static float Fraction(float _value, float _begin, float _end) {
        return (_value - _begin) / (_end - _begin);
    }

    /// <summary>
    /// Gets a value based on a certain range given a scalar/fraction.
    /// A fraction of 0.5 with lower and upper bounds 0 and 1 respectively, returns 0.5
    /// </summary>
    /// <param name="_fraction">Where in the range</param>
    /// <param name="_begin">Lower bound</param>
    /// <param name="_end">Upper bound</param>
    /// <returns>A value within the given range and fraction.</returns>
    public static float Fractal(float _fraction, float _begin, float _end) {
        return _fraction * (_end - _begin) + _begin;
    }
}
