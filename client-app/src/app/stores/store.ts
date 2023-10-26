import { useContext, createContext } from "react";
import UserStore from "./userStore";
import TokenStore from "./tokenStore";

interface Store {
  userStore: UserStore;
  tokenStore: TokenStore
}

export const store: Store = {
  userStore: new UserStore(),
  tokenStore: new TokenStore()
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext);
}