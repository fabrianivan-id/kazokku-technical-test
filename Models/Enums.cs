namespace DmsCreditScoring.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum MaritalStatus
    {
        Single,
        Married
    }

    public enum EducationLevel
    {
        SmaOrBelow,
        Diploma,
        S1,
        S2OrAbove
    }

    public enum ResidenceOwnership
    {
        Others,
        Rent,
        OwnedWithMortgage,
        Owned
    }

    public enum CompanyCategory
    {
        GovernmentInstitution,     // Lembaga Departemen
        RegionalOwnedEnterprise,   // BUMD
        PrivateNoRating,           // Swasta tidak punya rating
        PrivateWithRating,         // Swasta dengan rating
        PrivateCategoryI,
        PrivateCategoryII,
        PrivateCategoryIII
    }

    public enum JobPosition
    {
        Staff,
        Director,
        Commissioner
    }

    public enum InstallmentHistory
    {
        NewBorrower,
        LateButCurrent,
        OnTime
    }

    public enum SlikStatus
    {
        Collectibility3To5,
        ArrearsLessThan3Months,
        NoFacility,
        Current
    }

    public enum CreditCardOwnership
    {
        None,
        Basic,
        Gold,
        PlatinumOrAbove
    }

    public enum BankAccountType
    {
        None,
        Savings,
        Giro,
        SavingsGiroAndDeposit
    }

    public enum AppraisalResult
    {
        NotRecommended,
        Marketable
    }

    public enum FinancingPurpose
    {
        Others,
        RentalInvestment,
        Renovation,
        FirstHomeSelfOccupied
    }
}
