namespace PawMates.Data
{
    public class DataConstants
    {
        //Pets constants
        public const int PetNameMaxLenght = 40;
        public const int PetNameMinLenght = 2;

        public const int PetAgeMaxLenght = 100;
        public const int PetAgeMinLenght = 0;

        public const string DateOfBirthFormat = "DD/MM/YY";

        public const int PetBreedMaxLenght = 60;
        public const int PetBreedMinLenght = 2;

        public const int PetColorMaxLenght = 30;
        public const int PetColorMinLenght = 3;

        public const int PetWeightMaxLenght = 100;
        public const int PetWeightMinLenght = 0;

        //Owner constants

        public const int OwnerNamesMaxLenght = 50;
        public const int OwnerNamesMinLenght = 2;
                         
        public const int OwnerCountryNameMaxLenght = 56;
        public const int OwnerCountryNameMinLenght = 4;
                         
        public const int OwnerAdressNameMaxLenght = 100;
        public const int OwnerAdressNameMinLenght = 15;

        //Events constants

        public const int EventNameMaxLenght = 70;
        public const int EventNameMinLenght = 8;

        public const int EventDescriptionMaxLenght = 250;
        public const int EventDescriptionMinLenght = 20;

        public const int EventLocationMaxLenght = 100;
        public const int EventLocationMinLenght = 10;

        public const string EventStartDateFormat = "yyyy-MM-dd H:mm";

        //PetType constants

        public const int TypeNameMaxLenght = 70;
        public const int TypeNameMinLenght = 8;

        public const int TypeDescriptionMaxLenght = 250;
        public const int TypeDescriptionMinLenght = 20;

        //Error message

        public const string RequireErrorMessage = "The field {0} is required";
        public const string StringLengthErrorMessage = "The field {0} must be between {2} and {1} characters long";
        public const string RangeIntErrorMessage = "The field {0} must be between {1} and {2} characters long";

    }
}
