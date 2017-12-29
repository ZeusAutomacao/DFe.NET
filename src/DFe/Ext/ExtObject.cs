namespace DFe.Ext
{
    public static class ExtObject
    {
        public static bool IsNull(this object objeto)
        {
            return objeto == null;
        }

        public static bool IsNotNull(this object objeto)
        {
            return !objeto.IsNull();
        }
    }
}