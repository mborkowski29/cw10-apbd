namespace cw10.DTOs;

public class PrescriptionPatientDTOs
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<PrescriptionMedicamentDTOs> Medicaments { get; set; }
}