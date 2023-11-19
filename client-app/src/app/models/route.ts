import { Stop } from "./stop";

export interface Route {
  id: string;
  name: string;
  startStationName: string;
  endStationName: string;
  trainNumber: number;
  isActive: boolean;
}

export interface RouteFormValues {
  name: string;
  startStationName: string;
  endStationName: string;
  trainNumber: number;
}

export interface RouteDetails {
  id: string;
  name: string;
  startStationName: string;
  endStationName: string;
  trainNumber: number;
  stops: Stop[];
}

export class Route implements Route {
  constructor(init?: RouteFormValues) {
    Object.assign(this, init);
    this.isActive = false;
  }
}