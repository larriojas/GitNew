using Alemana.Nucleo.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Alemana.Nucleo.Common.Utility
{
    /// <summary>
    /// Clase internal de ayuda para operaciones con Reflection.
    /// </summary>
    internal static class ReflectionHelper
    {
        #region Public Methods

        public static bool IsSerializable(this object obj)
        {
            if (obj is System.Runtime.Serialization.ISerializable)
                return true;
            return Attribute.IsDefined(obj.GetType(), typeof(SerializableAttribute));
        }
        internal static Type GetType(string type)
        {
            try
            {
                return Type.GetType(type, true);
            }
            catch (Exception ex)
            {
                throw new NucleoCommonException(ex, Messages.ReflectionHelper_TypeNotFound, type);
            }
        }

        internal static object CreateObject(string type, params object[] args)
        {
            return CreateObject(GetType(type), args);
        }

        internal static object TryCreateObject(Type type, params object[] args)
        {
            object result;

            try
            {
                result = Activator.CreateInstance(type, args);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        internal static object CreateObject(Type type, params object[] args)
        {

            object result;

            try
            {
                result = Activator.CreateInstance(type, args);
            }
            catch (Exception ex)
            {
                throw new NucleoCommonException(ex, Messages.ReflectionHelper_CantCreateInstance, type.AssemblyQualifiedName);
            }

            return result;
        }

        internal static TCast CreateObject<TCast>(string type, params object[] args)
        {
            object instance = CreateObject(type, args);

            if (instance is TCast)
                return (TCast)instance;
            else
                throw new NucleoCommonException(Messages.ReflectionHelper_CantCastToType, type, typeof(TCast).AssemblyQualifiedName);
        }

        internal static TCast CreateObject<TCast>(Type type, params object[] args)
        {
            object instance = CreateObject(type, args);

            if (instance is TCast)
                return (TCast)instance;
            else
                throw new NucleoCommonException(Messages.ReflectionHelper_CantCastToType, type.AssemblyQualifiedName, typeof(TCast).AssemblyQualifiedName);
        }

        internal static PropertyInfo GetProperty(LambdaExpression exp)
        {
            MemberExpression memberExp;
            
            if (exp.Body is UnaryExpression) //Puede venir como Convert(entity.propertyName)
                memberExp = ((UnaryExpression)exp.Body).Operand as MemberExpression;
            else
                memberExp = exp.Body as MemberExpression;

            if (memberExp == null)
                throw new NucleoCommonException(Messages.ReflectionHelper_BadLambdaProperty);

            if (memberExp.Member.MemberType != System.Reflection.MemberTypes.Property)
                throw new NucleoCommonException(Messages.ReflectionHelper_BadLambdaProperty);

            return memberExp.Member as PropertyInfo;
        }

        internal static void SetPropertyValue(object dto, LambdaExpression exp, object value)
        {
            SetPropertyValue(dto, GetProperty(exp), value);
        }

        internal static void SetPropertyValue(object dto, PropertyInfo property, object value)
        {
            property.SetValue(dto, value, null);
        }

        internal static object GetPropertyValue(object obj, PropertyInfo property)
        {
            return property.GetValue(obj, null);
        }

        internal static PropertyInfo[] GetProperties(object obj, params LambdaExpression[] bindings)
        {
            return obj.GetType().GetProperties();
        }

        internal static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        internal static Type GetNonNullableType(Type type)
        {
            return IsNullableType(type) ? type.GetGenericArguments()[0] : type;
        }

        internal static object GenericCreate(Type genericType, params Type[] specificTypes)
        {
            var specificType = genericType.MakeGenericType(specificTypes);
            return Activator.CreateInstance(specificType);
        }

        internal static object GenericCreate(Type genericType, string specificTypeName)
        {
            var specificType = genericType.MakeGenericType(Type.GetType(specificTypeName));
            return Activator.CreateInstance(specificType);
        }

        internal static object GenericCreate(Type genericType, Type specificType, params object[] args)
        {
            var newType = genericType.MakeGenericType(specificType);
            return Activator.CreateInstance(newType, args);
        }

        internal static object GenericInvoke(object instance, Type genericType1, Type genericType2, string methodName, params object[] args)
        {
            var method = GetExactMethod(instance, methodName, args);
            method = method.MakeGenericMethod(genericType1, genericType2);
            return method.Invoke(instance, args);
        }

        internal static object GenericInvoke(object instance, Type genericType, string methodName, params object[] args)
        {
            var method = GetExactMethod(instance, methodName, args);
            method = method.MakeGenericMethod(genericType);
            return method.Invoke(instance, args);
        }

        internal static object GenericInvoke(object instance, Type genericType, MethodInfo method, params object[] args)
        {
            method = method.MakeGenericMethod(genericType);
            return method.Invoke(instance, args);
        }

        internal static object GenericInvoke(object instance, Type genericType, int methodNumber, params object[] args)
        {
            var method = GetExactMethod(instance, methodNumber, args);
            method = method.MakeGenericMethod(genericType);
            return method.Invoke(instance, args);
        }

        internal static MethodInfo GetExactMethod(object instance, int methodNumber, params object[] args)
        {
            if (instance.GetType().GetMethods()[methodNumber] != null)
                return instance.GetType().GetMethods()[methodNumber];
            else
                return null;
        }

        internal static MethodInfo GetExactMethod(object instance, BindingFlags flags, string methodName, params object[] args)
        {
            MethodInfo method;
            Type[] inputParams = GetArgumentTypes(args);


            if (inputParams.Length > 0)
                method = instance.GetType().GetMethod(methodName, flags, null, inputParams.ToArray(), null);
            else
                method = instance.GetType().GetMethod(methodName, flags);


            return method;
        }

        internal static MethodInfo GetExactMethod(object instance, string methodName, params object[] args)
        {
            MethodInfo method;
            Type[] inputParams = GetArgumentTypes(args);


            if (inputParams.Length > 0)
                method = instance.GetType().GetMethod(methodName, inputParams.ToArray());
            else
                method = instance.GetType().GetMethod(methodName);


            return method;
        }

        internal static Type[] GetArgumentTypes(object[] args)
        {
            List<Type> inputParamTypes = new List<Type>();

            foreach (var arg in args)
            {
                Type argType = arg.GetType();

                if (argType.BaseType == null
                    || argType.BaseType.Name != "LambdaExpression")
                    inputParamTypes.Add(argType);
            }

            return inputParamTypes.ToArray();
        }

        #endregion
    }

}