export interface Route {
  id: string;
  name: string;
  startStationName: string;
  endStationName: string;
  trainNumber: number;
}

export interface RouteFormValues {
  name: string;
  startStationName: string;
  endStationName: string;
  trainNumber: number;
}

export class Route implements Route {
  constructor(init?: RouteFormValues) {
    Object.assign(this, init);
  }
}