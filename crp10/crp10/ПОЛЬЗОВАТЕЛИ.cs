//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace crp10
{
    using System;
    using System.Collections.Generic;
    
    public partial class ПОЛЬЗОВАТЕЛИ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ПОЛЬЗОВАТЕЛИ()
        {
            this.АРЕНДА = new HashSet<АРЕНДА>();
            this.ЗАЯВКИ = new HashSet<ЗАЯВКИ>();
            this.КЛИЕНТЫ = new HashSet<КЛИЕНТЫ>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ID_sotrudnik { get; set; }
        public Nullable<int> ID_role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<АРЕНДА> АРЕНДА { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ЗАЯВКИ> ЗАЯВКИ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<КЛИЕНТЫ> КЛИЕНТЫ { get; set; }
        public virtual РОЛЬ РОЛЬ { get; set; }
        public virtual СОТРУДНИКИ СОТРУДНИКИ { get; set; }
    }
}
