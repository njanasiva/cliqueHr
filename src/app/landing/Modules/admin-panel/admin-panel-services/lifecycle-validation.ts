import { ValidationType } from 'src/Application/Types/Constants';


export class ValidationMessage {
  public static data = {
    ProbationDetailForm: {
      ProbationName: {
        [ValidationType.required]: "Probation Name Required."
      },
      Period: {
        [ValidationType.required]: "Period Required."
      },
      IncrementEachExtensionby: {
        [ValidationType.required]:"Increment each extension by."
      },
      MaxnoofDays: {
        [ValidationType.required]: "Max. Number of days to extend probation."
      },
      ConfirmationTrigger: {
        [ValidationType.required]: "Confirmation trigger."
      }
    }

  }
}
