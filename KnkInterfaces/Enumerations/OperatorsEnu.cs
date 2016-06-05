using System.ComponentModel;

namespace KnkInterfaces.Enumerations
{
    public enum OperatorsEnu
    {
        [Description("@Field=@Value|@Field is @Value")]   Equal,
        [Description("@Field<>@Value")]                     Distinct,
        [Description("@Field>@Value")]                      GreatThan,
        [Description("@Field<@Value")]                      LowerThan,
        [Description("@Field Like @Value")]                 Like,
        [Description("@Field In (@List[Field])")]           In
    }
}
