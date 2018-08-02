namespace EmployeeManagement
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("tblDepartment")]
    public partial class tblDepartment
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        [Remote("IsAvailable", "Departments", ErrorMessage = "This Department is already exist", AdditionalFields = "Id")]
        public string DepartmentName { get; set; }
    }
}
