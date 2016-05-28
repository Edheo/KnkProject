using System.Linq;
using KnkInterfaces.Utilities;
using KnkInterfaces.Interfaces;
using System.Reflection;
using System;
using KnkInterfaces.Classes;
using KnkInterfaces.Enumerations;

namespace KnkCore
{
    public abstract class KnkItemBase : KnkItemItf
    {
        private readonly KnkTableEntity _entity;
        private readonly string _primarykey;
        private UpdateStatusEnu _status = UpdateStatusEnu.NoChanges;
       

        public KnkItemBase(KnkTableEntity aEntity)
        {
            _entity = aEntity;
            _primarykey = KnkInterfacesUtils.GetPrimaryKey(this).Name;
        }

        public KnkListItf Parent { get; set; }
        private PropertyInfo KnkPrimaryKey
        {
            get
            {
                return KnkInterfacesUtils.GetProperties<KnkItemItf>(this).Where(p => p.Name == this.PrimaryKey()).FirstOrDefault();
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

        public string PrimaryKey()
        {
            return _primarykey;
        }

        private PropertyInfo PropertyMatch(string aProperty)
        {
            return KnkInterfacesUtils.GetProperties<KnkItemItf>(this).Where(p => p.Name.ToLower().Equals(aProperty.ToLower())).FirstOrDefault();
        }

        public object PropertyGet(string aProperty)
        {
            return PropertyMatch(aProperty)?.GetValue(this);
        }

        public void PropertySet(string aProperty, object aValue)
        {
            var lPrp = PropertyMatch(aProperty);
            if(lPrp!=null)
            {
                if(lPrp.PropertyType.Equals(typeof(KnkEntityIdentifier)))
                {
                    KnkEntityIdentifier lVal = null;
                    if (aValue != null)
                        lVal = (int)Convert.ChangeType(aValue, typeof(int));
                    lPrp.SetValue(this, lVal);
                }
                else
                { 
                    lPrp.SetValue(this, aValue);
                }
            }
        }

        public int? UserCreationId { get; set; }
        public int? UserModifiedId { get; set; }
        public int? UserDeletedId { get; set; }

        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public abstract override string ToString();

        public T Clone<T>() where T : KnkItemItf, new()
        {
            T lNew = new T();
            var lProperties = KnkInterfacesUtils.GetProperties<KnkItemItf>(this).Where(p => p.Name != this.PrimaryKey());
            foreach(var lProperty in lProperties)
            {
                lNew.PropertySet(lProperty.Name, this.PropertyGet(lProperty.Name));
            }
            return lNew;
        }

        public void Update()
        {
            KnkEntityIdentifier lIdp = PropertyGet(PrimaryKey()) as KnkEntityIdentifier;
            if (lIdp!=null)
            {
                if(lIdp.GetInnerValue()!=null)
                    _status = UpdateStatusEnu.Update;
                else
                    _status = UpdateStatusEnu.New;
            }
        }

        public void Delete()
        {
            _status = UpdateStatusEnu.Delete;
        }

        public UpdateStatusEnu Status()
        {
            return _status;
        }

    }
}
