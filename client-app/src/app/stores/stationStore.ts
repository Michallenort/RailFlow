import { makeAutoObservable, runInAction } from "mobx";
import { Station, StationFormValues } from "../models/station";
import agent from "../api/agent";

export default class StationStore {
  stations = new Map<string, Station>();
  isLoading = false;

  constructor() {
    makeAutoObservable(this);
  }

  private setStation = (station: Station) => {
    this.stations.set(station.id, station);
  }

  loadStations = async () => {
    this.isLoading = true;
    
    try {
      const result = await agent.Stations.list();
      result.data.forEach((station: Station) => {
        this.setStation(station);
      });
      this.isLoading = false;
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

  createStation = async (station: StationFormValues) => {
    try {
      await agent.Stations.create(station);
      const newStation = new Station(station);
      this.setStation(newStation);
    } catch(error) {
      console.log(error);
    }
  }

  deleteStation = async (id: string) => {
    this.isLoading = true;
    try {
      await agent.Stations.delete(id);
      runInAction(() => {
        this.stations.delete(id);
        this.isLoading = false;
      })
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }
}