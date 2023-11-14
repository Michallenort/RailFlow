export interface User {
  id: string;
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
  confirmPassword: string;
  nationality: string;
}

export interface CreateUserFormValues {
  firstName: string;
  lastName: string;
  email: string;
  dateOfBirth: Date;
  password: string;
  confirmPassword: string;
  nationality: string;
  roleId: number;
}

export class User implements User {
  constructor(init?: SignInFormValues) {
    Object.assign(this, init);
  }
}