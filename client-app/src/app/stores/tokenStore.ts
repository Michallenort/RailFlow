import { makeAutoObservable, reaction } from "mobx";
import { getLocalStorageWithExpiry, removeLocalStorage, setLocalStorageWithExpiry } from "./localStorageHandler";

export default class TokenStore {
  token: string | null = getLocalStorageWithExpiry('jwt');
  
  constructor() {
    makeAutoObservable(this);

    reaction(
      () => this.token,
      token => {
        if (token) {
          setLocalStorageWithExpiry('jwt', token, 1);
        } else {
          removeLocalStorage('jwt');
        }
      }
    )
  }

  setToken = (token: string | null) => {
    this.token = token;
  }
}