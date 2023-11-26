import { Assignment } from "./assignment";
import { Route } from "./route";

export interface Schedule {
  id: string;
  date: Date;
  route: Route;
}

export interface ScheduleDetails {
  id: string;
  date: Date;
  route: Route;
  assignments: Assignment[];
}