namespace cw10.DTOs;

public class PrescriptionDTOs
{
    public PatientDTOs Patient { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDTOs Doctor { get; set; }
    public List<PrescriptionMedicamentDTOs> Medicaments { get; set; }
}