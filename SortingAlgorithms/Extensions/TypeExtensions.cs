using System;
using System.Collections.Generic;

namespace SortingAlgorithms.Extensions
{
    internal static class TypeExtensions
    {
        private static readonly HashSet<Type> IntegerTypes = new HashSet<Type>
        {
            typeof(byte), typeof(sbyte),
            typeof(short), typeof(ushort),
            typeof(int), typeof(uint),
            typeof(long), typeof(ulong)
        };

        private static readonly HashSet<Type> FloatingPointTypes = new HashSet<Type>
        {
            typeof(decimal), typeof(float), typeof(double)
        };
        
        internal static bool IsNumeric(this Type t)
        {
            return IsInteger(t) || IsFloatingPoint(t);
        }

        internal static bool IsInteger(this Type t)
        {
            return IntegerTypes.Contains(t);
        }

        internal static bool IsFloatingPoint(this Type t)
        {
            return FloatingPointTypes.Contains(t);
        }
    }
}