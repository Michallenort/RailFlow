import { makeAutoObservable, runInAction } from "mobx";
import { Pagination, PagingParams } from "../models/pagination";
import { Train, TrainFormValues } from "../models/train";
import agent from "../api/agent";

export default class TrainStore {
  trains = new Map<string, Train>();
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

  clearTrains = () => {
    this.trains.clear();
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

  private setTrain = (train: Train) => {
    this.trains.set(train.id, train);
  }

  loadTrains = async () => {
    this.isLoading = true;
    this.clearTrains();

    try {
      const result = await agent.Trains.list(this.axiosParams);
      result.data.items.forEach((train: Train) => {
        this.setTrain(train);
      });
      this.setPagination(result.data.pagination);
      this.isLoading = false;
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

  createTrain = async (train: TrainFormValues) => {
    try {
      const response = await agent.Trains.create(train);
      const newTrain = new Train(train);
      this.setTrain(newTrain);

      return response;
    } catch(error) {
      console.log(error);
    }
  }

  deleteTrain = async (id: string) => {
    this.isLoading = true;
    try {
      await agent.Trains.delete(id);
      runInAction(() => {
        this.trains.delete(id);
        this.isLoading = false;
      })
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }  
}