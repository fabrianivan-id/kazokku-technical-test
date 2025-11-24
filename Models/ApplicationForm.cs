using System;
using System.ComponentModel.DataAnnotations;

namespace DmsCreditScoring.Models
{
    public class ApplicationForm
    {
        private const string RequiredMessage = "\"{0}\" field is required";

        // ===== INFORMASI APLIKASI UTAMA =====

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "No Aplikasi")]
        public string ApplicationNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Nama Pemohon")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Tempat Lahir")]
        public string BirthPlace { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Lahir")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Jenis Kelamin")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Alamat")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Kode Pos")]
        public string PostalCode { get; set; } = string.Empty;

        // Untuk demo, umur bisa diisi manual, tapi juga akan dihitung dari BirthDate
        [Display(Name = "Umur (tahun)")]
        public int Age { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Tenor (tahun)")]
        public int TenorYears { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Status Perkawinan")]
        public MaritalStatus MaritalStatus { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Jumlah Tanggungan")]
        public int NumberOfDependants { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Pendidikan Terakhir")]
        public EducationLevel Education { get; set; }

        // ===== INFORMASI 2 – TEMPAT TINGGAL =====

        [Display(Name = "Alamat sesuai data Bank?")]
        public bool AddressMatchesBank { get; set; }

        [Display(Name = "Kepemilikan Tempat Tinggal")]
        public ResidenceOwnership ResidenceOwnership { get; set; }

        [Display(Name = "Lama Menempati (tahun)")]
        public int YearsAtCurrentAddress { get; set; }

        // ===== INFORMASI 3 – PEKERJAAN & PENGHASILAN =====

        [Display(Name = "Kategori Perusahaan")]
        public CompanyCategory CompanyCategory { get; set; }

        [Display(Name = "Jabatan")]
        public JobPosition JobPosition { get; set; }

        [Display(Name = "Lama Bekerja (tahun)")]
        public int YearsWorking { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Pendapatan THP per bulan (Rp)")]
        public decimal TakeHomePay { get; set; }

        // ===== INFORMASI 4 – REKENING & HISTORI =====

        [Display(Name = "Jenis Rekening Bank")]
        public BankAccountType BankAccountType { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Rata-rata Saldo per bulan (Rp)")]
        public decimal AverageMonthlyBalance { get; set; }

        [Display(Name = "Track Record Pembayaran Angsuran")]
        public InstallmentHistory InstallmentHistory { get; set; }

        [Display(Name = "Track Data SLIK")]
        public SlikStatus SlikStatus { get; set; }

        [Display(Name = "Kepemilikan Kartu Kredit")]
        public CreditCardOwnership CreditCardOwnership { get; set; }

        // ===== INFORMASI 5 – TENOR & DSR =====

        /// <summary>
        /// Debt Service Ratio dalam persen, contoh: 35 berarti 35%
        /// </summary>
        [Display(Name = "Debt Service Ratio (%)")]
        public double DebtServiceRatio { get; set; }

        // TenorYears dipakai juga di INFORMASI 5

        // ===== INFORMASI 6 – JAMINAN =====

        [Display(Name = "Hasil Appraisal")]
        public AppraisalResult AppraisalResult { get; set; }

        [Display(Name = "Luas Bangunan (m2)")]
        public double BuildingArea { get; set; }

        [Display(Name = "Tujuan Pembiayaan")]
        public FinancingPurpose FinancingPurpose { get; set; }

        /// <summary>
        /// Loan to Value dalam persen, contoh: 80 berarti 80%
        /// </summary>
        [Display(Name = "Loan to Value (LTV) (%)")]
        public double LoanToValue { get; set; }
    }
}
