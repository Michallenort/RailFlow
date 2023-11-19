export interface Stop {
  id: string;
  arrivalTime: string;
  departureTime: string;
  stationName: string;
}

export interface StopFormValues {
  arrivalTime: string;
  departureTime: string;
  stationName: string;
  routeId: string;
}

export class Stop implements Stop {
  constructor(init?: StopFormValues) {
    Object.assign(this, init);
  }
}