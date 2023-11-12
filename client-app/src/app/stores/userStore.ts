import { makeAutoObservable, runInAction, values } from "mobx";
import { SignInFormValues, SignUpFormValues, User } from "../models/user";
import agent from "../api/agent";
import { store } from "./store";
import { router } from "../router/Routes";
import { getLocalStorageWithExpiry, removeLocalStorage, setLocalStorageWithExpiry } from "./localStorageHandler";

export default class UserStore {
  user: User | null = null;

  constructor() {
    makeAutoObservable(this);

    this.loadUserDetails();  
  }

  get isLoggedIn() {
    return !!this.user;
  }

  get isAdmin() {
    return this.user?.roleName === 'Supervisor';
  }

  loadUserDetails() {
    const userJson = getLocalStorageWithExpiry('user');
    if (userJson) {
      try {
        const userData = JSON.parse(userJson);
        userData.dateOfBirth = new Date(userData.dateOfBirth);
        runInAction(() => {
          this.user = userData;
        });
      } catch (error) {
        console.log(error);
      }
    }
  }

  saveUserDetails(user: User) {
    try {
      const userJson = JSON.stringify(user);
      setLocalStorageWithExpiry('user', userJson, 1);
    } catch (error) {
      console.log(error);
    }
  }

  signIn = async (values: SignInFormValues) => {
    try {
      const token = await agent.Users.signin(values);
      store.tokenStore.setToken(token.data.accessToken);
      const user = await agent.Users.accountdetails();
      runInAction(() => {
        this.user = user.data;
        this.saveUserDetails(user.data);
      });
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
    removeLocalStorage('user');
    router.navigate('/');
  }
}