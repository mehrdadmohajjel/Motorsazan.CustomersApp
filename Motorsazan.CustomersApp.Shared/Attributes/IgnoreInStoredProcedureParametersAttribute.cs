using System;

namespace Motorsazan.CustomersApp.Shared.Attributes
{
    [AttributeUsage(System.AttributeTargets.Property)]
    public class IgnoreInStoredProcedureParametersAttribute : Attribute
    {
    }
}