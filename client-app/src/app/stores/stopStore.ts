import { makeAutoObservable, runInAction } from "mobx";
import { Pagination, PagingParams } from "../models/pagination";
import { Stop, StopFormValues } from "../models/stop";
import agent from "../api/agent";

export default class StopStore {
  selectedStops = new Map<string, Stop>();
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

  clearStops = () => {
    this.selectedStops.clear();
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

  setStop = (stop: Stop) => {
    this.selectedStops.set(stop.id, stop);
  }

  createStop = async (stop: StopFormValues) => {
    try {
      const response = await agent.Stops.create(stop);
      const newStop = new Stop(stop);
      this.setStop(newStop);

      return response;
    } catch(error) {
      console.log(error);
    }
  }

  deleteStop = async (id: string) => {
    this.isLoading = true;
    try {
      await agent.Stops.delete(id);
      runInAction(() => {
        this.selectedStops.delete(id);
        this.isLoading = false;
      });
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

  // loadStops = async () => {
  //   this.isLoading = true;
  //   this.clearStops();

  //   try {
  //     const result = await agent.Stops.list(this.axiosParams);
  //     result.data.items.forEach((stop: Stop) => {
  //       this.setStop(stop);
  //     });
  //     this.setPagination(result.data.pagination);
  //     this.isLoading = false;
  //   } catch(error) {
  //     console.log(error);
  //     this.isLoading = false;
  //   }
  // }
}