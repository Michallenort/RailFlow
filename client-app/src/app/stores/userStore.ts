import { makeAutoObservable, values } from "mobx";
import { SignInFormValues, User } from "../models/user";

export default class UserStore {
  user: User | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  signIn = async (values: SignInFormValues) => {
    try {
      
    } catch (error) {
      throw error;
    }
  }
}