using System.Runtime.Serialization;

namespace identityproduct_app.Domain.Enum
{
    public enum RoleEnum
    {
        [EnumMember(Value = "Admin")]
        Admin,
        [EnumMember(Value = "User")]
        User
    }
}
