using System;
using DmsCreditScoring.Models;

namespace DmsCreditScoring.Services
{
    public class ScoringService : IScoringService
    {
        public ScoringResult Calculate(ApplicationForm form)
        {
            // Kalau Age kosong, hitung dari BirthDate
            if (form.Age <= 0 && form.BirthDate != default)
            {
                form.Age = GetAgeFromBirthDate(form.BirthDate);
            }

            var result = new ScoringResult();

            var g1 = CalculateInfo1(form);
            var g2 = CalculateInfo2(form);
            var g3 = CalculateInfo3(form);
            var g4 = CalculateInfo4(form);
            var g5 = CalculateInfo5(form);
            var g6 = CalculateInfo6(form);

            result.GroupScores["INFORMASI 1 - Data Demografis"] = g1;
            result.GroupScores["INFORMASI 2 - Tempat Tinggal"] = g2;
            result.GroupScores["INFORMASI 3 - Pekerjaan & THP"] = g3;
            result.GroupScores["INFORMASI 4 - Rekening & Histori"] = g4;
            result.GroupScores["INFORMASI 5 - Tenor & DSR"] = g5;
            result.GroupScores["INFORMASI 6 - Jaminan"] = g6;

            result.TotalScore = g1 + g2 + g3 + g4 + g5 + g6;
            result.RiskLevel = GetRiskLevel(result.TotalScore);

            return result;
        }

        // ================= HELPER =================

        private static int GetAgeFromBirthDate(DateTime birthDate)
        {
            if (birthDate == default) return 0;
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

        private static string GetRiskLevel(double totalScore)
        {
            if (totalScore <= 55) return "HIGH RISK";
            if (totalScore <= 70) return "MEDIUM RISK";
            return "LOW RISK";
        }

        // ================= INFORMASI 1 =================
        // BOBOT B = 0.05

        private double CalculateInfo1(ApplicationForm f)
        {
            const double B = 0.05;

            double sumHD = 0;

            // Umur Pemohon (D = 0.3)
            const double DAge = 0.3;
            var fAge = GetAgeScore(f.Age);
            sumHD += fAge * DAge;

            // Umur + Tenor (D = 0.1) batas ketentuan 65 tahun
            const double DAgeTenor = 0.1;
            var agePlusTenor = f.Age + f.TenorYears;
            var fAgeTenor = agePlusTenor > 65 ? 25.0 : 100.0;
            sumHD += fAgeTenor * DAgeTenor;

            // Status Perkawinan (D = 0.4)
            const double DMarital = 0.4;
            var fMarital = GetMaritalScore(f.MaritalStatus, f.NumberOfDependants);
            sumHD += fMarital * DMarital;

            // Pendidikan (D = 0.2)
            const double DEdu = 0.2;
            var fEdu = GetEducationScore(f.Education);
            sumHD += fEdu * DEdu;

            return B * sumHD;
        }

        private static double GetAgeScore(int age)
        {
            if (age >= 31 && age <= 45) return 100;
            if (age >= 46 && age <= 55) return 75;
            if (age >= 21 && age <= 30) return 50;
            if (age >= 56 && age <= 65) return 25;
            return 25; // default worst
        }

        private static double GetMaritalScore(MaritalStatus status, int dependants)
        {
            if (status == MaritalStatus.Single)
            {
                if (dependants > 2) return 25;
                if (dependants > 0) return 45;
                return 65; // 0 tanggungan
            }

            // Married
            if (dependants > 2) return 85;
            return 100;
        }

        private static double GetEducationScore(EducationLevel education)
        {
            return education switch
            {
                EducationLevel.SmaOrBelow => 25,
                EducationLevel.Diploma => 50,
                EducationLevel.S1 => 75,
                EducationLevel.S2OrAbove => 100,
                _ => 25
            };
        }

        // ================= INFORMASI 2 =================
        // BOBOT B = 0.05

        private double CalculateInfo2(ApplicationForm f)
        {
            const double B = 0.05;
            double sumHD = 0;

            // Alamat tempat tinggal (D = 0.4)
            const double DAddress = 0.4;
            var fAddress = f.AddressMatchesBank ? 100.0 : 25.0;
            sumHD += fAddress * DAddress;

            // Kepemilikan tempat tinggal (D = 0.3)
            const double DOwnership = 0.3;
            var fOwnership = f.ResidenceOwnership switch
            {
                ResidenceOwnership.Others => 25,
                ResidenceOwnership.Rent => 50,
                ResidenceOwnership.OwnedWithMortgage => 75,
                ResidenceOwnership.Owned => 100,
                _ => 25
            };
            sumHD += fOwnership * DOwnership;

            // Lama menempati (D = 0.3)
            const double DYears = 0.3;
            var years = Math.Max(0, f.YearsAtCurrentAddress);
            double fYears;
            if (years <= 2) fYears = 25;
            else if (years <= 5) fYears = 50;
            else if (years <= 8) fYears = 75;
            else fYears = 100;

            sumHD += fYears * DYears;

            return B * sumHD;
        }

        // ================= INFORMASI 3 =================
        // BOBOT B = 0.2

        private double CalculateInfo3(ApplicationForm f)
        {
            const double B = 0.2;
            double sumHD = 0;

            // Kategori Perusahaan (D = 0.2)
            const double DCompany = 0.2;
            var fCompany = f.CompanyCategory switch
            {
                CompanyCategory.GovernmentInstitution => 100,
                CompanyCategory.RegionalOwnedEnterprise => 25,
                CompanyCategory.PrivateNoRating => 100,
                CompanyCategory.PrivateWithRating => 25,
                CompanyCategory.PrivateCategoryI => 75,
                CompanyCategory.PrivateCategoryII => 50,
                CompanyCategory.PrivateCategoryIII => 0,
                _ => 25
            };
            sumHD += fCompany * DCompany;

            // Jabatan (D = 0.2)
            const double DJob = 0.2;
            var fJob = f.JobPosition switch
            {
                JobPosition.Staff => 25,
                JobPosition.Director => 75,
                JobPosition.Commissioner => 100,
                _ => 25
            };
            sumHD += fJob * DJob;

            // Lama bekerja (D = 0.2)
            const double DWorkYears = 0.2;
            var yrs = Math.Max(0, f.YearsWorking);
            double fWorkYears;
            if (yrs <= 2) fWorkYears = 0;
            else if (yrs <= 5) fWorkYears = 25;
            else if (yrs <= 10) fWorkYears = 75;
            else fWorkYears = 100;
            sumHD += fWorkYears * DWorkYears;

            // Pendapatan THP (D = 0.4)
            const double DIncome = 0.4;
            var thp = Math.Max(0m, f.TakeHomePay);
            double fIncome;
            if (thp <= 10_000_000m) fIncome = 25;
            else if (thp <= 25_000_000m) fIncome = 50;
            else if (thp <= 50_000_000m) fIncome = 75;
            else fIncome = 100;
            sumHD += fIncome * DIncome;

            return B * sumHD;
        }

        // ================= INFORMASI 4 =================
        // BOBOT B = 0.15

        private double CalculateInfo4(ApplicationForm f)
        {
            const double B = 0.15;
            double sumHD = 0;

            // Rekening Bank (D = 0.1)
            const double DAccount = 0.1;
            var fAccount = f.BankAccountType switch
            {
                BankAccountType.None => 25,
                BankAccountType.Savings => 50,
                BankAccountType.Giro => 75,
                BankAccountType.SavingsGiroAndDeposit => 100,
                _ => 25
            };
            sumHD += fAccount * DAccount;

            // Rata-rata saldo (D = 0.15)
            const double DBalance = 0.15;
            var bal = Math.Max(0m, f.AverageMonthlyBalance);
            double fBalance;
            if (bal <= 10_000_000m) fBalance = 25;
            else if (bal <= 25_000_000m) fBalance = 50;
            else if (bal <= 50_000_000m) fBalance = 75;
            else fBalance = 100;
            sumHD += fBalance * DBalance;

            // Track record pembayaran angsuran (D = 0.15)
            const double DInstallment = 0.15;
            var fInstallment = f.InstallmentHistory switch
            {
                InstallmentHistory.NewBorrower => 25,
                InstallmentHistory.LateButCurrent => 50,
                InstallmentHistory.OnTime => 100,
                _ => 25
            };
            sumHD += fInstallment * DInstallment;

            // Track data SLIK (D = 0.4)
            const double DSlik = 0.4;
            var fSlik = f.SlikStatus switch
            {
                SlikStatus.Collectibility3To5 => 0,
                SlikStatus.ArrearsLessThan3Months => 50,
                SlikStatus.NoFacility => 75,
                SlikStatus.Current => 100,
                _ => 0
            };
            sumHD += fSlik * DSlik;

            // Kepemilikan kartu kredit (D = 0.2)
            const double DCc = 0.2;
            var fCc = f.CreditCardOwnership switch
            {
                CreditCardOwnership.None => 25,
                CreditCardOwnership.Basic => 50,
                CreditCardOwnership.Gold => 75,
                CreditCardOwnership.PlatinumOrAbove => 100,
                _ => 25
            };
            sumHD += fCc * DCc;

            return B * sumHD;
        }

        // ================= INFORMASI 5 =================
        // BOBOT B = 0.3

        private double CalculateInfo5(ApplicationForm f)
        {
            const double B = 0.3;
            double sumHD = 0;

            // Tenor (D = 0.25)
            const double DTenor = 0.25;
            var tenor = Math.Max(0, f.TenorYears);
            double fTenor;
            if (tenor > 15) fTenor = 25;
            else if (tenor > 10) fTenor = 50;
            else if (tenor > 5) fTenor = 75;
            else fTenor = 100;
            sumHD += fTenor * DTenor;

            // Debt Service Ratio (D = 0.75)
            const double DDSR = 0.75;
            var dsr = Math.Max(0.0, f.DebtServiceRatio);
            double fDsr;
            if (dsr > 50) fDsr = 0;
            else if (dsr > 40) fDsr = 50;
            else if (dsr > 30) fDsr = 75;
            else fDsr = 100;
            sumHD += fDsr * DDSR;

            return B * sumHD;
        }

        // ================= INFORMASI 6 =================
        // BOBOT B = 0.25

        private double CalculateInfo6(ApplicationForm f)
        {
            const double B = 0.25;
            double sumHD = 0;

            // Hasil Appraisal (D = 0.1)
            const double DAppraisal = 0.1;
            var fAppraisal = f.AppraisalResult switch
            {
                AppraisalResult.NotRecommended => 0,
                AppraisalResult.Marketable => 100,
                _ => 0
            };
            sumHD += fAppraisal * DAppraisal;

            // Luas Bangunan (D = 0.2)
            const double DArea = 0.2;
            var area = Math.Max(0.0, f.BuildingArea);
            double fArea;
            if (area > 200) fArea = 25;
            else if (area > 100) fArea = 50;
            else if (area > 45) fArea = 75;
            else fArea = 100;
            sumHD += fArea * DArea;

            // Tujuan Pembiayaan (D = 0.1)
            const double DPurpose = 0.1;
            var fPurpose = f.FinancingPurpose switch
            {
                FinancingPurpose.Others => 25,
                FinancingPurpose.RentalInvestment => 50,
                FinancingPurpose.Renovation => 75,
                FinancingPurpose.FirstHomeSelfOccupied => 100,
                _ => 25
            };
            sumHD += fPurpose * DPurpose;

            // LTV (D = 0.6)
            const double DLtv = 0.6;
            var ltv = Math.Max(0.0, f.LoanToValue);
            double fLtv;
            if (ltv > 100) fLtv = 0;
            else if (ltv > 90) fLtv = 25;
            else if (ltv > 80) fLtv = 50;
            else if (ltv > 70) fLtv = 75;
            else fLtv = 100;
            sumHD += fLtv * DLtv;

            return B * sumHD;
        }
    }
}
