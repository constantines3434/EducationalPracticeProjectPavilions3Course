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
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.StatePavilions = new HashSet<StatePavilion>();
        }
    
        public int IdEmployee { get; set; }
        public string Surname { get; set; }
        public string NameEmployee { get; set; }
        public string Patronimic { get; set; }
        public string LoginEmployee { get; set; }
        public string PasswordEmployee { get; set; }
        public Nullable<int> IdStatusEmployee { get; set; }
        public Nullable<int> IdRole { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public byte[] Photo { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual StatusEmployee StatusEmployee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StatePavilion> StatePavilions { get; set; }
    }
}