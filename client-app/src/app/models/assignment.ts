export interface Assignment {
  id: string;
  userEmail: string;
  scheduleId: string;
  startHour: string;
  endHour: string;
}

export interface AssignmentFormValues {
  userEmail: string;
  scheduleId: string;
  startHour: string;
  endHour: string;
}

export class Assignment implements Assignment {
  constructor(init?: AssignmentFormValues) {
    Object.assign(this, init);
  }
}