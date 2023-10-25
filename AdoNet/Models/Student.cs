using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AdoNet.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Student ID is Required")]
        [RegularExpression(@"^STDN\d{5}$", ErrorMessage = "Student ID should start with 'STDN' then 5 digits.")]

        public string StudentID { get; set; }

        [CustomGenderValidation(ErrorMessage = "Gender should be 'M', 'F', or null.")]

        public string ? Gender { get; set; }

        [CustomNationalityValidation(ErrorMessage = "Gender must be 'M' or 'F'.")]

        public string?  Nationality { get; set; }

        [CustomPlaceOfBirthValidation(ErrorMessage = "PlaceOfBirth must only contain Alphabets and White Space.")]

        public string? PlaceOfBirth { get; set; }

        [CustomStageIDValidation(ErrorMessage = "StageID must be 'loverlevel' or 'MiddleSchool' or 'HighSchool'.")]

        public string? StageID { get; set; }

        [CustomGradeIDValidation(ErrorMessage = "GradeID must be G-00 format.")]

        public string? GradeID { get; set; }

        [CustomSectionIDValidation(ErrorMessage = "SectionID must be 'A' or 'C'.")]

        public string? SectionID { get; set; }

        [CustomTopicValidation(ErrorMessage = "Topic must only contain Alphabets.")]

        public string? Topic { get; set; }

        [CustomSemesterValidation(ErrorMessage = "Semester must be 'F' or 'S'.")]

        public string? Semester { get; set; }

        [CustomRelationValidation(ErrorMessage = "Relation must be 'Father' or 'Mum'.")]

        public string? Relation { get; set; }

        [CustomRaisedHandsValidation(ErrorMessage = "RaisedHands must be a number between 0 - 999.")]

        public int? RaisedHands { get; set; }

        [CustomVisitedResourcesValidation(ErrorMessage = "VisitedResources must be a number between 0 - 999.")]

        public int? VisitedResources { get; set; }

        [CustomAnnouncementsViewValidation(ErrorMessage = "AnnouncementsView must be a number between 0 - 999.")]

        public int? AnnouncementsView { get; set; }

        [CustomDiscussionValidation(ErrorMessage = "Discussion must be a number between 0 - 999.")]

        public int? Discussion { get; set; }

        [CustomParentAnsweringSurveyValidation(ErrorMessage = "ParentAnsweringSurvey must be 'Yes' or 'No'.")]

        public string? ParentAnsweringSurvey { get; set; }

        [CustomParentSchoolSatisfactionValidation(ErrorMessage = "ParentSchoolSatisfaction must be 'Good' or 'Bad'.")]

        public string? ParentSchoolSatisfaction { get; set; }

        [CustomStudentAbsenceDaysValidation(ErrorMessage = "StudentAbsenceDays must be 'Under-7' or 'Above-7'.")]

        public string? StudentAbsenceDays { get; set; }

        [CustomStudentMarksValidation(ErrorMessage = "StudentMarks must be a number between 0 - 100.")]

        public int? StudentMarks { get; set; }

        [CustomClassValidation(ErrorMessage = "Class must be 'M' or 'L' or 'H'.")]

        public string? Class { get; set; }
    }

    public class CustomGenderValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            string gender = value as string;
            if (gender == "M" || gender == "F")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }

    public class CustomNationalityValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string nationality = value as string;

            if (Regex.IsMatch(nationality, "^[a-zA-Z ]+$"))
            {
                return ValidationResult.Success; // Matches the pattern
            }

            return new ValidationResult("Nationality must only contain Alphabets and White Space.");
        }
    }

    public class CustomPlaceOfBirthValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string placeOfBirth = value as string;

            if (Regex.IsMatch(placeOfBirth, "^[a-zA-Z ]+$"))
            {
                return ValidationResult.Success; // Matches the pattern
            }

            return new ValidationResult("PlaceOfBirth must only contain Alphabets and White Space.");
        }
    }

    public class CustomStageIDValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string stageID = value as string;

            if (Regex.IsMatch(stageID, "^(lowerlevel|MiddleSchool|HighSchool)$"))
            {
                return ValidationResult.Success; // Matches the pattern
            }

            return new ValidationResult("StageID must be 'loverlevel' or 'MiddleSchool' or 'HighSchool'.");
        }
    }

    public class CustomGradeIDValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string gradeID = value as string;

            if (Regex.IsMatch(gradeID, "^G-\\d{2}$"))
            {
                return ValidationResult.Success; // Matches the pattern
            }

            return new ValidationResult("GradeID must be G-00 format.");
        }
    }

    public class CustomSectionIDValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string sectionID = value as string;

            if (Regex.IsMatch(sectionID, "^[A-C]$"))
            {
                return ValidationResult.Success; // Matches the pattern
            }

            return new ValidationResult("SectionID must be 'A' or 'C'.");
        }
    }

    public class CustomTopicValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string topic = value as string;

            if (Regex.IsMatch(topic, "^[a-zA-Z]+$"))
            {
                return ValidationResult.Success; // Matches the pattern
            }

            return new ValidationResult("Topic must only contain Alphabets.");
        }

    }

    public class CustomSemesterValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string semester = value as string;

            if (Regex.IsMatch(semester, "^[FS]$"))
            {
                return ValidationResult.Success; // Matches the pattern
            }

            return new ValidationResult("Semester must be 'F' or 'S'.");
        }
    }

    public class CustomRelationValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string relation = value as string;

            if (Regex.IsMatch(relation, "^(Father|Mum)$"))
            {
                return ValidationResult.Success; // Matches the pattern
            }

            return new ValidationResult("Relation must be 'Father' or 'Mum'.");
        }
    }

    public class CustomRaisedHandsValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            int? raisedHands = value as int?;

            if (raisedHands >= 0 && raisedHands <= 999)
            {
                return ValidationResult.Success; // Falls within the valid range
            }

            return new ValidationResult("RaisedHands must be a number between 0 - 999.");
        }
    }

    public class CustomVisitedResourcesValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            int? visitedResources = value as int?;

            if (visitedResources >= 0 && visitedResources <= 999)
            {
                return ValidationResult.Success; // Falls within the valid range
            }

            return new ValidationResult("VisitedResources must be a number between 0 - 999.");
        }
    }

    public class CustomAnnouncementsViewValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            int? announcementsView = value as int?;

            if (announcementsView >= 0 && announcementsView <= 999)
            {
                return ValidationResult.Success; // Falls within the valid range
            }

            return new ValidationResult("AnnouncementsView must be a number between 0 - 999.");
        }
    }

    public class CustomDiscussionValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            int? discussion = value as int?;

            if (discussion >= 0 && discussion <= 999)
            {
                return ValidationResult.Success; // Falls within the valid range
            }

            return new ValidationResult("Discussion must be a number between 0 - 999.");
        }
    }

    public class CustomParentAnsweringSurveyValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string parentAnsweringSurvey = value as string;

            if (Regex.IsMatch(parentAnsweringSurvey, "^(Yes|No)$", RegexOptions.IgnoreCase))
            {
                return ValidationResult.Success; // Matches the pattern (case-insensitive)
            }

            return new ValidationResult("ParentAnsweringSurvey must be 'Yes' or 'No'.");
        }
    }

    public class CustomParentSchoolSatisfactionValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string parentSchoolSatisfaction = value as string;

            if (Regex.IsMatch(parentSchoolSatisfaction, "^(Good|Bad)$", RegexOptions.IgnoreCase))
            {
                return ValidationResult.Success; // Matches the pattern (case-insensitive)
            }

            return new ValidationResult("ParentSchoolSatisfaction must be 'Good' or 'Bad'.");
        }
    }

    public class CustomStudentAbsenceDaysValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string studentAbsenceDays = value as string;

            if (Regex.IsMatch(studentAbsenceDays, "^(Under-7|Above-7)$", RegexOptions.IgnoreCase))
            {
                return ValidationResult.Success; // Matches the pattern (case-insensitive)
            }

            return new ValidationResult("StudentAbsenceDays must be 'Under-7' or 'Above-7'.");
        }

    }

    public class CustomStudentMarksValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            int? studentMarks = value as int?;

            if (studentMarks >= 0 && studentMarks <= 100)
            {
                return ValidationResult.Success; // Falls within the valid range
            }

            return new ValidationResult("StudentMarks must be a number between 0 - 100.");
        }

    }

    public class CustomClassValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success; // Null values are allowed
            }

            string classValue = value as string;

            if (Regex.IsMatch(classValue, "^[MLH]$", RegexOptions.IgnoreCase))
            {
                return ValidationResult.Success; // Matches the pattern (case-insensitive)
            }

            return new ValidationResult("Class must be 'M', 'L', or 'H'.");
        }
    }



}
