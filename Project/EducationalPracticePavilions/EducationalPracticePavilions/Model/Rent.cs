//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EducationalPracticePavilions.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rent
    {
        public int IdRent { get; set; }
        public Nullable<int> IdTenant { get; set; }
        public Nullable<int> IdStatePavilion { get; set; }
        public Nullable<int> IdStatusRent { get; set; }
        public Nullable<System.DateTime> StartOfLease { get; set; }
        public Nullable<System.DateTime> EndOfLease { get; set; }
    
        public virtual StatePavilion StatePavilion { get; set; }
        public virtual StatusRent StatusRent { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
