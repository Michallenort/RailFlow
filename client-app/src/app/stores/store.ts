import { useContext, createContext } from "react";
import UserStore from "./userStore";
import TokenStore from "./tokenStore";
import StationStore from "./stationStore";

interface Store {
  userStore: UserStore;
  tokenStore: TokenStore,
  stationStore: StationStore
}

export const store: Store = {
  userStore: new UserStore(),
  tokenStore: new TokenStore(),
  stationStore: new StationStore()
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext);
}