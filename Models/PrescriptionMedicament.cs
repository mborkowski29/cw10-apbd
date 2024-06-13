using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace cw10.Models;

[PrimaryKey(nameof(IdMedicament),nameof(IdPrescription))] 
public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }

    [ForeignKey(nameof(IdMedicament))]
    public virtual Medicament Medicament { get; set; }

    public int IdPrescription { get; set; }

    [ForeignKey(nameof(IdPrescription))]
    public virtual Prescription Prescription { get; set; }

    [Required]
    public int Dose { get; set; }

    [MaxLength(100)]
    public string Details { get; set; }
}