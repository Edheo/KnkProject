using KnkInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnkCore
{
    public class KnkEntityRelation<Tdad, Titm> : KnkList<Tdad, Titm>, KnkEntityRelationItf<Tdad, Titm>
        where Tdad : KnkItemItf, new()
        where Titm : KnkItemItf, new()
    {
        public KnkEntityRelation(Tdad aItem)
        :this(aItem, (new Titm()).SourceEntity().SourceTable)
        {
        }

        public KnkEntityRelation(Tdad aItem, string aRelatedView)
        : base(aItem.Connection())
        {
            this.Criteria = KnkCore.Utilities.KnkCoreUtils.BuildRelationCriteria(this, aItem, aRelatedView);
        }
    }
}
