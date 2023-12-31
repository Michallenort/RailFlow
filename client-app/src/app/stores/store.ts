import { useContext, createContext } from "react";
import UserStore from "./userStore";
import TokenStore from "./tokenStore";
import StationStore from "./stationStore";
import TrainStore from "./trainStore";
import RouteStore from "./routeStore";
import StopStore from "./stopStore";
import ScheduleStore from "./scheduleStore";
import AssignmentStore from "./assignmentsStore";
import ConnectionStore from "./connectionStore";
import CheckoutStore from "./checkoutStore";
import ReservationStore from "./reservationStore";

interface Store {
  userStore: UserStore;
  tokenStore: TokenStore;
  stationStore: StationStore;
  trainStore: TrainStore;
  routeStore: RouteStore;
  stopStore: StopStore;
  scheduleStore: ScheduleStore;
  assignmentStore: AssignmentStore;
  connectionStore: ConnectionStore;
  checkoutStore: CheckoutStore;
  reservationStore: ReservationStore;
}

export const store: Store = {
  userStore: new UserStore(),
  tokenStore: new TokenStore(),
  stationStore: new StationStore(),
  trainStore: new TrainStore(),
  routeStore: new RouteStore(),
  stopStore: new StopStore(),
  scheduleStore: new ScheduleStore(),
  assignmentStore: new AssignmentStore(),
  connectionStore: new ConnectionStore(),
  checkoutStore: new CheckoutStore(),
  reservationStore: new ReservationStore()
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext);
}