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
    
    public partial class fu_function
    {
        public fu_function()
        {
            this.rf_role_function_rel = new HashSet<rf_role_function_rel>();
        }
    
        public int fu_function_id { get; set; }
        public Nullable<int> fu_function_parent_id { get; set; }
        public string fu_function_name { get; set; }
        public string fu_function_dec { get; set; }
        public string fu_controller { get; set; }
        public string fu_action { get; set; }
        public string fu_status { get; set; }
    
        public virtual ICollection<rf_role_function_rel> rf_role_function_rel { get; set; }
    }
}
