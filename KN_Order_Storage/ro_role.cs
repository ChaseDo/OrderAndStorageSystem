//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KN_Order_Storage
{
    using System;
    using System.Collections.Generic;
    
    public partial class ro_role
    {
        public ro_role()
        {
            this.rf_role_function_rel = new HashSet<rf_role_function_rel>();
            this.us_user = new HashSet<us_user>();
        }
    
        public int ro_role_id { get; set; }
        public string ro_role_name { get; set; }
        public string ro_function_dec { get; set; }
        public string ro_status { get; set; }
    
        public virtual ICollection<rf_role_function_rel> rf_role_function_rel { get; set; }
        public virtual ICollection<us_user> us_user { get; set; }
    }
}
