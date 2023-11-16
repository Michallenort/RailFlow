import { useContext, createContext } from "react";
import UserStore from "./userStore";
import TokenStore from "./tokenStore";
import StationStore from "./stationStore";
import TrainStore from "./trainStore";
import RouteStore from "./routeStore";

interface Store {
  userStore: UserStore;
  tokenStore: TokenStore;
  stationStore: StationStore;
  trainStore: TrainStore;
  routeStore: RouteStore;
}

export const store: Store = {
  userStore: new UserStore(),
  tokenStore: new TokenStore(),
  stationStore: new StationStore(),
  trainStore: new TrainStore(),
  routeStore: new RouteStore()
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext);
}