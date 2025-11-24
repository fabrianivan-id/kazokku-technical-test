using System.ComponentModel.DataAnnotations;

namespace DmsCreditScoring.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum MaritalStatus
    {
        [Display(Name = "Belum Kawin")]
        Single,
        [Display(Name = "Kawin")]
        Married
    }

    public enum EducationLevel
    {
        [Display(Name = "SMA atau dibawahnya")]
        SmaOrBelow,
        [Display(Name = "D1, D2, D3, D4")]
        Diploma,
        [Display(Name = "S1")]
        S1,
        [Display(Name = "Master atau diatasnya (S2, S3)")]
        S2OrAbove
    }

    public enum ResidenceOwnership
    {
        [Display(Name = "Lain-lain")]
        Others,
        [Display(Name = "Sewa / Kontrak")]
        Rent,
        [Display(Name = "Milik sendiri masih diangsur")]
        OwnedWithMortgage,
        [Display(Name = "Milik sendiri")]
        Owned
    }

    public enum CompanyCategory
    {
        [Display(Name = "Lembaga Departemen")]
        GovernmentInstitution,
        [Display(Name = "BUMD")]
        RegionalOwnedEnterprise,
        [Display(Name = "SWASTA tidak punya rating")]
        PrivateNoRating,
        [Display(Name = "SWASTA dengan rating")]
        PrivateWithRating,
        [Display(Name = "SWASTA Kategori I")]
        PrivateCategoryI,
        [Display(Name = "SWASTA Kategori II")]
        PrivateCategoryII,
        [Display(Name = "SWASTA Kategori III")]
        PrivateCategoryIII
    }

    public enum JobPosition
    {
        [Display(Name = "Staff")]
        Staff,
        [Display(Name = "Direktur")]
        Director,
        [Display(Name = "Komisaris")]
        Commissioner
    }

    public enum InstallmentHistory
    {
        [Display(Name = "Peminjam baru")]
        NewBorrower,
        [Display(Name = "Angsuran terlambat tapi lancar")]
        LateButCurrent,
        [Display(Name = "Angsuran tepat waktu")]
        OnTime
    }

    public enum SlikStatus
    {
        [Display(Name = "Kolektibilitas 3 sd 5")]
        Collectibility3To5,
        [Display(Name = "Ada tunggakan < 3 bulan")]
        ArrearsLessThan3Months,
        [Display(Name = "Tidak ada fasilitas")]
        NoFacility,
        [Display(Name = "Lancar")]
        Current
    }

    public enum CreditCardOwnership
    {
        [Display(Name = "Tidak Ada")]
        None,
        [Display(Name = "Basic")]
        Basic,
        [Display(Name = "Gold")]
        Gold,
        [Display(Name = "Platinum atau diatasnya")]
        PlatinumOrAbove
    }

    public enum BankAccountType
    {
        [Display(Name = "Tidak ada")]
        None,
        [Display(Name = "Tabungan")]
        Savings,
        [Display(Name = "Giro")]
        Giro,
        [Display(Name = "Tabungan/Giro + Deposito")]
        SavingsGiroAndDeposit
    }

    public enum AppraisalResult
    {
        [Display(Name = "Tidak Direkomendasikan")]
        NotRecommended,
        [Display(Name = "Marketable")]
        Marketable
    }

    public enum FinancingPurpose
    {
        [Display(Name = "Lain-Lain")]
        Others,
        [Display(Name = "Disewakan/Investasi")]
        RentalInvestment,
        [Display(Name = "Renovasi")]
        Renovation,
        [Display(Name = "Pertama & Ditempati Sendiri")]
        FirstHomeSelfOccupied
    }
}
