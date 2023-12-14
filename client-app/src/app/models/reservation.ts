export interface Reservation {
  id: string;
  date: Date;
  startStopName: string;
  startHour: string;
  endStopName: string;
  endHour: string;
  price: number;
}

export interface ReservationFormValues {
  date: Date;
  userId: string;
  firstScheduleId: string;
  secondScheduleId?: string;
  startStopId: string;
  startHour: string
  endStopId: string;
  endHour: string;
  price: number;
}