using Core.Repository;
using Microsoft.CodeAnalysis;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Domain
{
    public class SpecializationInitializer
    {
        private List<Specialization> Specializations = new List<Specialization>
        {
            new Specialization
            {
                Name="Psychiatry(Mental, Emotional or Behavioral Disorders)"
            },
            new Specialization
            {
                Name="Dentistry(Teeth)"
            },
            new Specialization
            {
                Name="Pediatrics and New Born(Child)"
            },
           new Specialization
            {
                Name="Orthopedics(Bones)"
            },
            new Specialization
            {
                Name="Genecology and Infertility"
            },
            new Specialization
            {
                Name="Ear, Nose and Throat"
            },
           new Specialization
            {
                Name="Andrology and Male Infertility"
            },
            new Specialization
            {
                Name="Allergy and Immunology(Sensitivity and Immunity)"
            },
            new Specialization
            {
                Name="Cardiology and Vascular Disease(Heart)"
            },
           new Specialization
            {
                Name="Audiology"
            },
            new Specialization
            {
                Name="Cardiology and Thoracic Surgery(Heart & Chest)"
            },
            new Specialization
            {
                Name="Chest and Respiratory"
            },
            new Specialization
            {
                Name="Dietitian and Nutrition"
            },
            new Specialization
            {
                Name="Diagnostic Radiology(Scan Centers)"
            },
            new Specialization
            {
                Name="Diabetes and Endocrinology"
            }
        };
        private IUnitOfWork _unitOfWork;

        public SpecializationInitializer(IUnitOfWork UnitOfWork) { 
            _unitOfWork = UnitOfWork;
        }
        public void Initialize(){
            if (_unitOfWork.Specializations.GetAnyAsync().Result)
            {
                return;   // DB has been seeded
            }
            _unitOfWork.Specializations.AddRangeAsync(Specializations);
            _unitOfWork.Complete();

        }
    }
}
