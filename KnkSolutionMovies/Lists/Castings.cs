using KnkCore;
using KnkInterfaces.Interfaces;
using KnkSolutionMovies.Entities;

namespace KnkSolutionMovies.Lists
{
    public class Castings : KnkList<Casting>
    {
        public Castings():base(new KnkConnection())
        {
            Connection.FillList(this);
        }

        public Castings(KnkConnectionItf aConnection) : base(aConnection)
        {
            Connection.FillList(this);
        }
    }
}
