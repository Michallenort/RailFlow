import { makeAutoObservable, values } from "mobx";
import { SignInFormValues, User } from "../models/user";
import agent from "../api/agent";

export default class UserStore {
  user: User | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  signIn = async (values: SignInFormValues) => {
    try {
      await agent.Users.signin(values);
      
    } catch (error) {
      throw error;
    }
  }
}