import { useContext, createContext } from "react";
import UserStore from "./userStore";
import TokenStore from "./tokenStore";
import StationStore from "./stationStore";
import TrainStore from "./trainStore";

interface Store {
  userStore: UserStore;
  tokenStore: TokenStore;
  stationStore: StationStore;
  trainStore: TrainStore;
}

export const store: Store = {
  userStore: new UserStore(),
  tokenStore: new TokenStore(),
  stationStore: new StationStore(),
  trainStore: new TrainStore()
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext);
}