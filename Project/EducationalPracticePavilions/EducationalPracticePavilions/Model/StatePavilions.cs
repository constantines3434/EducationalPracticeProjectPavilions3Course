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
    
    public partial class StatePavilions
    {
        public int IdPavilions { get; set; }
        public Nullable<int> IdEmployee { get; set; }
    
        public virtual Employees Employees { get; set; }
        public virtual Pavilions Pavilions { get; set; }
    }
}
