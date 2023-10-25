export interface User {
  firstName: string;
  lastName: string;
  email: string;
  dateOfBirth: Date;
  nationality: string;
  roleName: string;
}

export interface SignInFormValues {
  email: string;
  password: string;
}

export interface SignUpFormValues {
  firstName: string;
  lastName: string;
  email: string;
  dateOfBirth: Date;
  password: string;
  nationality: string;
}