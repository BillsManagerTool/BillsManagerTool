namespace BillsManagement.Utility
{
    using System;

    public class SecurityHelper
    {
        public Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}
