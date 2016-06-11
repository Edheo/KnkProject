using System.Linq;
using KnkInterfaces.Utilities;
using KnkInterfaces.Interfaces;
using System.Reflection;
using System;
using KnkInterfaces.Enumerations;
using KnkInterfaces.PropertyAtributes;
using KnkCore.Utilities;

namespace KnkCore
{
    public abstract class KnkItem : KnkItemItf
    {
        private readonly KnkTableEntity _entity;
        private readonly string _primarykey;
        private string _UpdateMessage;
        private UpdateStatusEnu _status = UpdateStatusEnu.NoChanges;
        private KnkListItf _parent;

        public KnkItem(KnkTableEntity aEntity)
        {
            _entity = aEntity;
            _primarykey = KnkCoreUtils.GetPrimaryKey(this).Name;
        }

        public KnkListItf GetParent()
        {
            return _parent;
        }

        public void SetParent(KnkListItf aParent)
        {
            //_status = UpdateStatusEnu.NoChanges;
            _parent = aParent;
        }

        private PropertyInfo KnkPrimaryKey
        {
            get
            {
                return KnkInterfacesUtils.GetProperties<KnkItemItf>(this).Where(p => p.Name == this.PrimaryKey()).FirstOrDefault();
            }
        }

        public KnkConnectionItf Connection()
        {
            if (this.GetParent() != null)
                return this.GetParent()?.Connection;
            else
                return Utilities.KnkCoreUtils.GlobalConn;
        }

        public KnkTableEntityItf SourceEntity()
        {
            return _entity;
        }

        public KnkItemItf Load<T>(int aId) where T:KnkItemItf, new()
        {
            return this.GetParent().Connection.GetItem<T>(aId);
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

        public bool Deleted { get; set; }

        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public KnkEntityIdentifierItf UserCreationId { get; set; }
        public KnkEntityIdentifierItf UserModifiedId { get; set; }
        public KnkEntityIdentifierItf UserDeletedId { get; set; }

        public string CreationText { get; set; }
        public string ModifiedText { get; set; }
        public string DeletedText { get; set; }

        public abstract override string ToString();

        public T Clone<T>() where T : KnkItemItf, new()
        {
            T lNew = new T();
            lNew.SetParent(this.GetParent());
            var lProperties = KnkInterfacesUtils.GetProperties<KnkItemItf>(this).Where(p => p.Name != this.PrimaryKey() 
                && !KnkInterfacesUtils.CreatedFields().Contains(p.Name.ToLower())
                && !KnkInterfacesUtils.DeletedFields().Contains(p.Name.ToLower())
                && !KnkInterfacesUtils.ModifiedFields().Contains(p.Name.ToLower())
                );
            foreach(var lProperty in lProperties)
            {
                lNew.PropertySet(lProperty.Name, this.PropertyGet(lProperty.Name));
            }
            return lNew;
        }

        public virtual void Update(string aMessage)
        {
            var lPrp = PropertyGet(PrimaryKey());
            _UpdateMessage = aMessage;
            Deleted = false;
            if (lPrp == null)
            {
                CreationDate = DateTime.Now;
                UserCreationId = Connection().CurrentUser().PrimaryKeyValue();
                CreationText = _UpdateMessage;
                _status = UpdateStatusEnu.New;
            }
            else
            {
                ModifiedDate = DateTime.Now;
                UserModifiedId = Connection().CurrentUser().PrimaryKeyValue();
                ModifiedText = _UpdateMessage;
                _status = UpdateStatusEnu.Update;
            }
            GetParent()?.AddMessage(new KnkChangeDescriptor(this));
        }

        public void Delete(string aMessage)
        {
            _status = UpdateStatusEnu.Delete;
            _UpdateMessage = aMessage;
            Deleted = true;
            DeletedDate = DateTime.Now;
            UserDeletedId = Connection().CurrentUser().PrimaryKeyValue();
            DeletedText = _UpdateMessage;
            GetParent()?.Messages?.Add(new KnkChangeDescriptor(this));
        }

        public string UpdateMessage()
        {
            return _UpdateMessage;
        }

        public string UpdateMessage(string aNewMessage)
        {
            _UpdateMessage = aNewMessage;
            return _UpdateMessage;
        }

        public UpdateStatusEnu Status()
        {
            return _status;
        }

        public bool PrimaryKeyAutoGenerated()
        {
            return this.PropertyMatch(PrimaryKey()).GetCustomAttributes().OfType<AtributePrimaryKey>().FirstOrDefault()?.AutoGenerated??false;
        }

        public KnkEntityIdentifierItf PrimaryKeyValue()
        {
            return (PropertyGet(PrimaryKey()) as KnkEntityIdentifier);
        }

        public bool IsNew()
        {
            return PrimaryKeyValue() == null;
        }

        public bool IsChanged()
        {
            return Status() != UpdateStatusEnu.NoChanges;
        }

        public KnkListItf<Tdad, Tlst> GetParent<Tdad, Tlst>()
            where Tdad : KnkItemItf, new()
            where Tlst : KnkItemItf, new()
        {
            return _parent as KnkListItf<Tdad,Tlst>;
        }
    }
}
