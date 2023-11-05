import { makeAutoObservable, runInAction, values } from "mobx";
import { SignInFormValues, SignUpFormValues, User } from "../models/user";
import agent from "../api/agent";
import { store } from "./store";
import { router } from "../router/Routes";

export default class UserStore {
  user: User | null = null;

  constructor() {
    makeAutoObservable(this);

    agent.Users.accountdetails().then(user => {
        runInAction(() => this.user = user);
      }).catch(error => {
        console.log(error);
      });;
    }

  get isLoggedIn() {
    return !!this.user;
  }

  get isAdmin() {
    return this.user?.roleName === 'Supervisor';
  }

  signIn = async (values: SignInFormValues) => {
    try {
      const token = await agent.Users.signin(values);
      store.tokenStore.setToken(token.accessToken);
      const user = await agent.Users.accountdetails();
      runInAction(() => this.user = user);
    } catch (error) {
      throw error;
    }
  }

  signUp = async (values: SignUpFormValues) => {
    try {
      await agent.Users.signup(values);
    } catch (error) {
      throw error;
    }
  }

  logout = () => {
    store.tokenStore.setToken(null);
    this.user = null;
    router.navigate('/');
  }
}