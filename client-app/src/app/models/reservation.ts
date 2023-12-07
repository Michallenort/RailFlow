export interface Reservation {
  id: string;
  date: Date;
  userId: string;
  firstScheduleId: string;
  secondScheduleId?: string;
  startStopName: string;
  endStopName: string;
  transferStopName?: string;
  price: number;
}

export interface ReservationFormValues {
  date: Date;
  userId: string;
  firstScheduleId: string;
  secondScheduleId?: string;
  startStopName: string;
  endStopName: string;
  transferStopName?: string;
  price: number;
}
