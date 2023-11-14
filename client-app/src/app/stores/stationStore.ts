import { makeAutoObservable, runInAction } from "mobx";
import { Station, StationFormValues } from "../models/station";
import agent from "../api/agent";
import { Pagination, PagingParams } from "../models/pagination";

export default class StationStore {
  stations = new Map<string, Station>();
  isLoading = false;
  pagination: Pagination | null = null;
  pagingParams = new PagingParams();
  searchTerm: string | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  setPagingParams = (pagingParams: PagingParams) => {
    this.pagingParams = pagingParams;
  }

  setSearchTerm = (searchTerm: string | null) => {
    this.searchTerm = searchTerm;
  }

  setPagination = (pagination: Pagination) => {
    this.pagination = pagination;
  }

  clearStations = () => {
    this.stations.clear();
  }

  get axiosParams() {
    const params = new URLSearchParams();

    if (this.searchTerm) {
      params.append('searchTerm', this.searchTerm);
    }

    params.append('page', this.pagingParams.page.toString());
    params.append('pageSize', this.pagingParams.pageSize.toString());
    return params;
  }

  private setStation = (station: Station) => {
    this.stations.set(station.id, station);
  }

  loadStations = async () => {
    this.isLoading = true;
    this.clearStations();

    try {
      const result = await agent.Stations.list(this.axiosParams);
      result.data.items.forEach((station: Station) => {
        this.setStation(station);
      });
      this.setPagination(result.data.pagination);
      this.isLoading = false;
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

  createStation = async (station: StationFormValues) => {
    try {
      const response = await agent.Stations.create(station);
      const newStation = new Station(station);
      this.setStation(newStation);

      return response;
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