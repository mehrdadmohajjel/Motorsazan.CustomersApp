using System;

namespace Motorsazan.CustomersApp.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreInStoredProcedureOutputAttribute : Attribute
    {
    }
}