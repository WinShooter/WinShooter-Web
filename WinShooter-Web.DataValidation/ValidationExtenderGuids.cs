using System;

namespace WinShooter.Web.DataValidation
{
    using System.Diagnostics;

    public static class ValidationExtenderGuids
    {
        [DebuggerHidden]
        public static Validation<Guid> NotEmpty(this Validation<Guid> item)
        {
            if (Guid.Empty.Equals(item.Value))
            {
                ThrowHelper.ThrowArgumentNullException(item.ArgName);

                // The return statement is only done to fool ReSharper
                return item;
            }

            return item;
        }
    }
}
