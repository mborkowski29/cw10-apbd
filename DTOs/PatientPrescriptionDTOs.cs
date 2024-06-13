namespace cw10.DTOs;

public class PatientPrescriptionDTOs
{
    public PatientDTOs PatientDto { get; set; }
    public List<PrescriptionPatientDTOs> PrescriptionForPatientDtos { get; set; }
    public DoctorDTOs DoctorDto { get; set; }
}