using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw10.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    public int IdDoctor { get; set; }

    [ForeignKey(nameof(IdDoctor))]
    public Doctor Doctor { get; set; }

    public int IdPatient { get; set; }

    [ForeignKey(nameof(IdPatient))]
    public Patient Patient { get; set; }
    
    public virtual ICollection<PrescriptionMedicament> PrescrptionMedicaments { get; set; }

}