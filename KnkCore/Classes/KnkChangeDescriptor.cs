﻿using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnkInterfaces.Enumerations;
using System.ComponentModel;

namespace KnkCore
{
    public class KnkChangeDescriptor : KnkChangeDescriptorItf
    {
        public KnkChangeDescriptor(KnkItemItf aItm)
        {
            Item = aItm;
            IdValue = Item.PropertyGet(aItm.PrimaryKey()) as KnkEntityIdentifier;
            CreationDate = aItm.CreationDate;
            DeletedDate = aItm.DeletedDate;
            ModifiedDate = aItm.ModifiedDate;
            Action = aItm.Status().ToString();
            Text = aItm.ToString();
            Message = aItm.UpdateMessage();
        }

        public KnkChangeDescriptor(string aAction, string aText)
        : this(aAction, aText, string.Empty)
        {

        }

        public KnkChangeDescriptor(string aAction, string aText, string aMessage)
        {
            CreationDate = DateTime.Now;
            Action = aAction;
            Message = aMessage;
            Text = aText;
        }

        [Browsable(false)]
        public KnkItemItf Item { get; }
        [Browsable(false)]
        public KnkEntityIdentifierItf IdValue { get; }
        public string Object
        {
            get
            {
                return Item?.GetType().Name;
            }
        }
        public string Action { get; private set; }
        public string Text { get; private set; }
        public string Message { get; private set; }

        public DateTime? CreationDate { get; }
        public DateTime? ModifiedDate { get; private set; }

        [Browsable(false)]
        public DateTime? DeletedDate { get; }

        public void UpdateMessage(string aAction, string aMessage)
        {
            Action = aAction;
            Message = aMessage;
            ModifiedDate = DateTime.Now;
        }
    }
}
