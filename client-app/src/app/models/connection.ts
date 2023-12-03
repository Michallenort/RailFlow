import { Schedule } from "./schedule";
import { Stop } from "./stop";

export interface SubConnection {
  schedule: Schedule;
  stops: Stop[];
}

export interface Connection {
  subConnections: SubConnection[];
  price: number
}