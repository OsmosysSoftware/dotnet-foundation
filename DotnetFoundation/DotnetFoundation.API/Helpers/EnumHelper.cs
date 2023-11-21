namespace DotnetFoundation.API.Helpers
{
    public static class EnumHelper
    {
        public static string ConvertToString(Enum enumElement)
        {
            return Enum.GetName(enumElement.GetType(), enumElement);
        }

        public static TEnumType ConvertToEnum<TEnumType>(string enumValue)
        {
            return (TEnumType)Enum.Parse(typeof(TEnumType), enumValue);
        }
    }
}
