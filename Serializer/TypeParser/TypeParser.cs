﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Serializer.TypeParser
{
    
    public class TypeParser
    {
        private readonly Type _type;
        private readonly SerializationTree _tree;
        private readonly ISet<Type> _atomicTypes;

        private TypeParser(Type type, ISet<Type> atomicTypes)
        {
            this._type = type;
            this._atomicTypes = atomicTypes;

            this._tree = new SerializationTree();
        }

        public static SerializationTree ParseType(Type type, ISet<Type> atomicTypes)
        {
            TypeParser typeParser = new TypeParser(type, atomicTypes);
            return typeParser.GenerateTree();
        }

        private SerializationTree GenerateTree()
        {
            CheckForErrors();
            ParseFields();
            ParseProperties();
            AddTreeAttributes();
            return _tree;
        }

        private void ParseProperties()
        {
            foreach (var property in _type.GetProperties())
            {
                if (!HasNonSerializableAttribute(Attribute.GetCustomAttributes(property)))
                {
                    ParseProperty(property);
                }
            }
        }


        private void CheckForErrors()
        {
            if (HasNonSerializableAttribute(Attribute.GetCustomAttributes(_type)))
            {
                throw new Exception(string.Format("The Type {0} has been marked as NonSerializable", _type.ToString()));
            }

            if (!HasSerializableAttribute(Attribute.GetCustomAttributes(_type)))
            {
                throw new Exception(string.Format("The Type {0} is not marked as Serializable", _type.ToString()));
            }
        }

// ReSharper disable once ParameterTypeCanBeEnumerable.Local
        private bool HasNonSerializableAttribute(Attribute[] attributes)
        {
            return (from att in attributes
                where att is Attributes.NonSerializableAttribute
                select att).Any();
        }

// ReSharper disable once ParameterTypeCanBeEnumerable.Local
        private bool HasSerializableAttribute(Attribute[] attributes)
        {
            return (from att in attributes
                where att is Attributes.SerializableAttribute
                select att).Any();
        }

        private void ParseFields()
        {
            foreach(FieldInfo field in _type.GetFields())
            {
                if (!HasNonSerializableAttribute(Attribute.GetCustomAttributes(field)))
                {
                    ParseField(field);
                }
            }
        }

        private FieldInfo GetPropertyBackingField(PropertyInfo property)
        {
            return _type.GetField(GetBackingFieldName(property.Name), BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private static string GetBackingFieldName(string propertyName)
        {
            return string.Format("<{0}>k__BackingField", propertyName);
        }

        private void ParseField(FieldInfo field)
        {
            if (_atomicTypes.Contains(field.FieldType))
            {
                ParseAtomicType(field);
            }
        }

        private void ParseProperty(PropertyInfo property)
        {
            if (_atomicTypes.Contains(property.PropertyType))
            {
                ParseAtomicType(property);
            }
        }

        private void ParseAtomicType(FieldInfo field)
        {
            var attributes = GetFilteredAttributes(field);

            AtomicType type = new AtomicType(new FieldInfoWrapper(field)) {Attributes = attributes};
            _tree.AddAtomicType(type);
        }

        private void ParseAtomicType(PropertyInfo property)
        {
            var attributes = GetFilteredAttributes(property);

            AtomicType type = new AtomicType(new PropertyInfoWrapper(property)) { Attributes = attributes };
            _tree.AddAtomicType(type);
        }

        private IEnumerable<SerializerAttribute> GetFilteredAttributes(FieldInfo field)
        {
            Attribute[] fieldAttributes = Attribute.GetCustomAttributes(field);
            return FilterAttributes(fieldAttributes);
        }

        private IEnumerable<SerializerAttribute> GetFilteredAttributes(PropertyInfo property)
        {
            Attribute[] fieldAttributes = Attribute.GetCustomAttributes(property);
            return FilterAttributes(fieldAttributes);
        } 

        private IEnumerable<SerializerAttribute> FilterAttributes(Attribute[] attributes)
        {
            //Filter the Serializable and NonSerializable attributes
            //The cast is possible cause it's checked before
            return from att in attributes
                             where (!(att is Attributes.SerializableAttribute)
                                   && !(att is Attributes.NonSerializableAttribute)
                                   && att is SerializerAttribute)
                             select (SerializerAttribute) att;
            //TO-DO may remove the unnecessary checks, as Serializable and NonSerializable attributes are not
            //derived by SerializerAttribute
        }
        
        private void AddTreeAttributes()
        {
            Attribute[] typeAttributes = Attribute.GetCustomAttributes(_type);
            var attrs = FilterAttributes(typeAttributes);
            _tree.Attributes = attrs;
        }

    }
}
