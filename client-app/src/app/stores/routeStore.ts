import { makeAutoObservable } from "mobx";
import { Route, RouteFormValues } from "../models/route";
import { Pagination, PagingParams } from "../models/pagination";
import agent from "../api/agent";
import { Station } from "../models/station";

export default class RouteStore {
  routes = new Map<string, Route>();
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

  clearRoutes = () => {
    this.routes.clear();
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

  private setRoute = (route: Route) => {
    this.routes.set(route.id, route);
  }

  loadRoutes = async () => {
    this.isLoading = true;
    this.clearRoutes();

    try {
      const result = await agent.Routes.list(this.axiosParams);
      result.data.items.forEach((route: Route) => {
        this.setRoute(route);
      });
      this.setPagination(result.data.pagination);
      this.isLoading = false;
    } catch(error) {
      console.log(error);
      this.isLoading = false;
    }
  }

  createStation = async (route: RouteFormValues) => {
    try {
      const response = await agent.Routes.create(route);
      const newRoute = new Route(route);
      this.setRoute(newRoute);

      return response;
    } catch(error) {
      console.log(error);
    }
  }

  // deleteStation = async (id: string) => {
  //   this.isLoading = true;
  //   try {
  //     await agent.Stations.delete(id);
  //     runInAction(() => {
  //       this.stations.delete(id);
  //       this.isLoading = false;
  //     })
  //   } catch(error) {
  //     console.log(error);
  //     this.isLoading = false;
  //   }
  // }
}