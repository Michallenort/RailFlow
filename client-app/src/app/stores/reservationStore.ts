import { makeAutoObservable, runInAction } from "mobx";
import { Reservation, ReservationFormValues } from "../models/reservation";
import { store } from "./store";
import agent from "../api/agent";

export default class ReservationStore {
  reservations = new Map<string, Reservation>();
  isLoading = false;

  constructor() {
    makeAutoObservable(this);
  }

  clearReservations = () => {
    this.reservations.clear();
  }

  private setReservation = (reservation: Reservation) => {
    this.reservations.set(reservation.id, reservation);
  }

  loadReservations = async () => {
    this.isLoading = true;
    this.clearReservations();

    try {
      const userId = store.userStore.loggedUser?.id;
      const result = await agent.Reservations.list(userId!);
      result.data.forEach((reservation: Reservation) => {
        this.setReservation(reservation);
      });
      this.isLoading = false;
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

  createReservation = async (reservation: ReservationFormValues) => {
    try {
      const response = await agent.Reservations.create(reservation);
      return response;
    } catch(error) {
      console.log(error);
    }
  }

  cancelReservation = async (id: string) => {
    this.isLoading = true;
    try {
      await agent.Reservations.cancel(id);
      runInAction(() => {
        this.reservations.delete(id);
        this.isLoading = false;
      })
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

  generateTicket = async (id: string) => {
    this.isLoading = true;
    try {
      let response = await agent.Reservations.generateTicket(id);
      this.isLoading = false;
      return response.data;
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

}