﻿using System.Linq;
using KnkInterfaces.Utilities;
using KnkInterfaces.Interfaces;
using System.Reflection;
using System;
using KnkInterfaces.Classes;

namespace KnkCore
{
    public class KnkItemBase : KnkItemItf
    {
        private KnkTableEntity _entity = new KnkTableEntity();
        public KnkListItf Parent { get; set; }
        private PropertyInfo KnkPrimaryKey
        {
            get
            {
                return KnkUtility.GetProperties<KnkItemItf>(this).Where(p => p.Name == this.SourceEntity.PrimaryKey).FirstOrDefault();
            }
        }

        public int? KnkEntityId
        {
            get
            {
                return (KnkPrimaryKey?.GetValue(this) as int?);
            }

            set
            {
                KnkPrimaryKey?.SetValue(this, value);
            }
        }

        public KnkConnectionItf Connection
        {
            get
            {
                return this.Parent?.Connection;
            }

        }

        public KnkTableEntityItf SourceEntity
        {
            get
            {
                return _entity;
            }
        }

        public KnkItemItf Load<T>(int aId) where T:KnkItemItf, new()
        {
            return this.Parent.Connection.GetItem<T>(aId);
        }

        private PropertyInfo PropertyMatch(string aProperty)
        {
            return KnkUtility.GetProperties<KnkItemItf>(this).Where(p => p.Name.ToLower().Equals(aProperty.ToLower())).FirstOrDefault();
        }

        public object PropertyGet(string aProperty)
        {
            return PropertyMatch(aProperty)?.GetValue(this);
        }

        public void PropertySet(string aProperty, object aValue)
        {
            PropertyMatch(aProperty)?.SetValue(this, aValue);
        }

        public int? UserCreationId { get; set; }
        public int? UserModifiedId { get; set; }
        public int? UserDeletedId { get; set; }

        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
