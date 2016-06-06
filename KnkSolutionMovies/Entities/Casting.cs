using KnkCore;
using KnkInterfaces.Interfaces;
using KnkInterfaces.PropertyAtributes;
using System;

namespace KnkSolutionMovies.Entities
{
    public class Casting : KnkItem
    {
        KnkEntityRelation<Casting, MediaLink> _Pictures;
        KnkEntityRelation<Casting, CastingBiography> _Biography;
        KnkEntityRelation<Casting, CastingName> _Names;

        #region Interface/Implementation
        public Casting():base(new KnkTableEntity("vieCasting", "Casting"))
        {
        }
        #endregion Interface/Implementation

        #region Class Properties
        [AtributePrimaryKey]
        public KnkEntityIdentifier IdCasting { get; set; }
        public string ArtistName { get; set; }
        public string BirthDay { get; set; }
        public string DeathDay { get; set; }
        public string HomePage { get; set; }
        public string BirthPlace { get; set; }
        public string ImdbId { get; set; }
        public string TmdbId { get; set; }
        public string TvdbId { get; set; }

        #endregion Class Properties

        public KnkEntityRelationItf<Casting, MediaLink> Pictures()
        {
            if (_Pictures == null) _Pictures = new KnkEntityRelation<Casting, MediaLink>(this, "vieCastingLinks");
            return _Pictures;
        }

        public KnkEntityRelationItf<Casting, CastingBiography> Biography()
        {
            if (_Biography == null) _Biography = new KnkEntityRelation<Casting, CastingBiography>(this, "vieCastingBiographies");
            return _Biography;
        }

        public KnkEntityRelationItf<Casting, CastingName> Names()
        {
            if (_Names == null) _Names = new KnkEntityRelation<Casting, CastingName>(this, "vieCastingNames");
            return _Names;
        }

        public override string ToString()
        {
            return ArtistName;
        }
    }
}
