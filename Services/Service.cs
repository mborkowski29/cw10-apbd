using cw10.Context;
using cw10.DTOs;
using cw10.Models;
using Microsoft.EntityFrameworkCore;

namespace cw10.Services;

public class Service
{
    private readonly c10Context _context;

    public Service(c10Context context)
    {
        _context = context;
    }

    public void AddPrescription(PrescriptionDTOs prescriptionDto)
    {
        Patient patient = _context.Patients
            .FirstOrDefault(p => p.IdPatient == prescriptionDto.Patient.IdPatient);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescriptionDto.Patient.FirstName,
                LastName = prescriptionDto.Patient.LastName,
                Birthdate = prescriptionDto.Patient.Birthdate
            };

            _context.Patients.Add(patient);
        }
        
        foreach (var medDto in prescriptionDto.Medicaments)
        {
            if (!_context.Medicaments.Any(m => m.IdMedicament == medDto.IdMedicament))
            {
                throw new Exception($"Medicament with ID {medDto.IdMedicament} not found.");
            }
        }
        
        var prescription = new Prescription
        {
            Date = prescriptionDto.Date,
            DueDate = prescriptionDto.DueDate,
            IdDoctor = prescriptionDto.Doctor.IdDoctor,
            IdPatient = patient.IdPatient
        };
        
        _context.Prescriptions.Add(prescription);
        
        foreach (var medDto in prescriptionDto.Medicaments)
        {
            _context.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = medDto.IdMedicament,
                IdPrescription = prescription.IdPrescription,
                Dose = medDto.Dose,
                Details = medDto.Description
            });
        }

        _context.SaveChanges();
    }

    public PatientPrescriptionDTOs GetPatientDetails(int id)
    {
        var patient = _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.Doctor)
            .FirstOrDefault(p => p.IdPatient == id);

        if (patient == null)
        {
            return null;
        }

        var prescriptionsDto = patient.Prescriptions.Select(p => new PrescriptionPatientDTOs
        {
            IdPrescription = p.IdPrescription,
            Date = p.Date,
            DueDate = p.DueDate,
            Medicaments = p.PrescrptionMedicaments.Select(pm => new PrescriptionMedicamentDTOs
            {
                IdMedicament = pm.Medicament.IdMedicament,
                Name = pm.Medicament.Name,
                Dose = pm.Dose,
                Description = pm.Details
            }).ToList()
        }).OrderBy(p => p.DueDate).ToList();

        var patientDto = new PatientDTOs
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate
        };

        var doctorDto = prescriptionsDto.FirstOrDefault()?.Medicaments.FirstOrDefault() != null
            ? new DoctorDTOs()
            {
                IdDoctor = patient.Prescriptions.FirstOrDefault().Doctor.IdDoctor,
                FirstName = patient.Prescriptions.FirstOrDefault().Doctor.FirstName,
                LastName = patient.Prescriptions.FirstOrDefault().Doctor.LastName,
                Email = patient.Prescriptions.FirstOrDefault().Doctor.Email
            }
            : null;
        var result = new PatientPrescriptionDTOs
        {
            PatientDto = patientDto,
            PrescriptionForPatientDtos = prescriptionsDto,
            DoctorDto = new DoctorDTOs
            {
                IdDoctor = doctorDto.IdDoctor,
                FirstName = doctorDto.FirstName,
                LastName = doctorDto.LastName,
                Email = doctorDto.Email
            }
        };

        return result;
    }
}
